using Microsoft.AspNetCore.Mvc;
using Motel.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace Motel.Web.Controllers
{
    public class ReservasController : Controller
    {
        private readonly HttpClient _api;

        public ReservasController(IHttpClientFactory factory)
        {
            _api = factory.CreateClient("MotelApi");
        }

        // 1) Lista todas las reservas del cliente
        public async Task<IActionResult> Index()
        {
            var cid = HttpContext.Session.GetInt32("ClienteId");
            if (!cid.HasValue) return RedirectToAction("Login", "Auth");

            List<Reserva> reservas = new();
            try
            {
                var resp = await _api.GetAsync($"reservas/cliente/{cid}");
                if (resp.IsSuccessStatusCode)
                    reservas = await resp.Content.ReadFromJsonAsync<List<Reserva>>();
                else
                    TempData["Error"] = $"API error: {resp.StatusCode}";
            }
            catch (Exception ex)
            {
                TempData["Error"] = $"Error de conexión: {ex.Message}";
            }

            return View(reservas);
        }

        // 2) Mostrar formulario de búsqueda por tipo y cantidad
        [HttpGet]
        public IActionResult BuscarDisponibilidad()
        {
            // Si no hay usuario logueado, voy a Login
            if (!HttpContext.Session.GetInt32("ClienteId").HasValue)
                return RedirectToAction("Login", "Auth");

            return View();
        }

        // 3) Llamada a GET api/habitaciones/disponibles/{tipo}/{cantidad}
        [HttpPost]
        public async Task<IActionResult> HabitacionesDisponibles(BuscarHabitacionesViewModel vm)
        {
            // 1) No permitir fechas pasadas
            if (vm.FechaEntrada < DateTime.Now)
            {
                TempData["Error"] = "La fecha de entrada no puede ser anterior a hoy.";
                return RedirectToAction("BuscarDisponibilidad");
            }

            // 2) Salida posterior a entrada
            if (vm.FechaSalida <= vm.FechaEntrada)
            {
                TempData["Error"] = "La fecha de salida debe ser posterior a la de entrada.";
                return RedirectToAction("BuscarDisponibilidad");
            }

            // 3) Mínimo 1 hora de estadía
            if ((vm.FechaSalida - vm.FechaEntrada).TotalHours < 1)
            {
                TempData["Error"] = "La estadía mínima es de 1 hora.";
                return RedirectToAction("BuscarDisponibilidad");
            }

            // 4) Validación de tipo y cantidad existente
            if (string.IsNullOrWhiteSpace(vm.TipoHabitacion) || vm.Cantidad < 1)
            {
                TempData["Error"] = "Debes indicar tipo de habitación y cantidad válida.";
                return RedirectToAction("BuscarDisponibilidad");
            }

            // 5) Guardar fechas para pasos siguientes
            TempData["FechaEntrada"] = vm.FechaEntrada.ToString("o");
            TempData["FechaSalida"] = vm.FechaSalida.ToString("o");
            TempData.Keep("FechaEntrada");
            TempData.Keep("FechaSalida");

            // 6) Llamada a tu API Core
            var url = $"habitaciones/disponibles/{Uri.EscapeDataString(vm.TipoHabitacion)}/{vm.Cantidad}";
            var resp = await _api.GetAsync(url);
            if (!resp.IsSuccessStatusCode)
            {
                var err = await resp.Content.ReadAsStringAsync();
                TempData["Error"] = $"Error disponibilidad: {err}";
                return RedirectToAction("BuscarDisponibilidad");
            }

            var list = await resp.Content.ReadFromJsonAsync<List<Habitacion>>() ?? new();
            return View("HabitacionesDisponibles", list);
        }

        // 4) Elegir servicios tras seleccionar habitaciones
        [HttpPost]
        public async Task<IActionResult> ElegirServicios(string habitacionIds)
        {
            if (string.IsNullOrEmpty(habitacionIds))
            {
                TempData["Error"] = "Selecciona al menos una habitación.";
                return RedirectToAction("BuscarDisponibilidad");
            }

            // Guardamos habitaciones y mantenemos fechas
            TempData["HabitacionIds"] = habitacionIds;
            TempData.Keep("HabitacionIds");
            TempData.Keep("FechaEntrada");
            TempData.Keep("FechaSalida");

            // Traer lista de servicios (ID, Nombre, Precio)
            List<Servicios> servicios = new();
            try
            {
                var resp = await _api.GetAsync("ReservaServicios/nombres-precios-ids");
                if (resp.IsSuccessStatusCode)
                    servicios = await resp.Content.ReadFromJsonAsync<List<Servicios>>();
                else
                    TempData["Error"] = $"API error: {resp.StatusCode}";
            }
            catch (Exception ex)
            {
                TempData["Error"] = $"Error de conexión: {ex.Message}";
            }

            return View("ElegirServicios", servicios);
        }

        // 5) Mostrar datos del cliente y pasar habitaciones+servicios
        [HttpPost]
        public IActionResult DatosCliente(string habitacionIds, string servicioIds)
        {
            if (string.IsNullOrEmpty(habitacionIds))
                return RedirectToAction("BuscarDisponibilidad");

            TempData["HabitacionIds"] = habitacionIds;
            TempData["ServicioIds"] = servicioIds ?? "";
            TempData.Keep("HabitacionIds");
            TempData.Keep("ServicioIds");
            TempData.Keep("FechaEntrada");
            TempData.Keep("FechaSalida");

            var email = HttpContext.Session.GetString("ClienteEmail") ?? "";
            return View(new Cliente { CorreoCliente = email });
        }

        // 6) Confirmar reserva: recibe Cliente, fechas y servicios
        [HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> ConfirmarReserva(
            Cliente cliente,
            DateTime fechaEntrada,
            DateTime fechaSalida,
            string servicioIds)
        {   
            if (!ModelState.IsValid)
                return View("DatosCliente", cliente);

            // 6.1) Crear o actualizar cliente
            var cid = HttpContext.Session.GetInt32("ClienteId");
            if (!cid.HasValue)
            {
                var respCli = await _api.PostAsJsonAsync("clientes", cliente);
                var creado = await respCli.Content.ReadFromJsonAsync<Cliente>();
                cid = creado!.NumCliente;
                HttpContext.Session.SetInt32("ClienteId", cid.Value);
            }

            // 6.2) Calcular total y crear reserva
            var horas = (decimal)(fechaSalida - fechaEntrada).TotalHours;
            var total = 0m;

            var habIds = TempData["HabitacionIds"]!
                           .ToString()!
                           .Split(',', StringSplitOptions.RemoveEmptyEntries);
            var srvIds = (servicioIds ?? "")
                           .Split(',', StringSplitOptions.RemoveEmptyEntries);

            // Obtener info y precios de las habitaciones
            var todasHab = new List<Habitacion>();
            foreach (var h in habIds)
            {
                var r = await _api.GetAsync($"habitaciones/byid/{h}");
                if (r.IsSuccessStatusCode)
                {
                    var hInfo = await r.Content.ReadFromJsonAsync<Habitacion>();
                    if (hInfo != null)
                    {
                        todasHab.Add(hInfo);
                        total += hInfo.PrecioHabitacion * horas;
                    }
                }
            }

            // Obtener info y precios de los servicios
            var todosSrv = new List<Servicios>();
            foreach (var s in srvIds)
            {
                var r = await _api.GetAsync($"servicios/{s}");
                if (r.IsSuccessStatusCode)
                {
                    var sInfo = await r.Content.ReadFromJsonAsync<Servicios>();
                    if (sInfo != null)
                    {
                        todosSrv.Add(sInfo);
                        total += sInfo.PrecioServicio;
                    }
                }
            }

            var nuevaReserva = new
            {
                NumCliente = cid.Value,
                FechaEntrada = fechaEntrada,
                FechaSalida = fechaSalida,
                EstadoReserva = "Confirmada",
                TotalReserva = total,
                FechaReserva = DateTime.Now,
                ComentarioReserva = $"Estadía: {horas:0.##}h"
            };

            var respRes = await _api.PostAsJsonAsync("reservas", nuevaReserva);
            var creada = await respRes.Content.ReadFromJsonAsync<Reserva>();

            // 6.3) Asignar habitaciones con precio real
            foreach (var hInfo in todasHab)
            {
                var detalle = new ReservaHabitacion
                {
                    NumReserva = creada!.NumReserva,
                    IdHabitacion = hInfo.IdHabitacion,
                    PrecioHabitacion = hInfo.PrecioHabitacion
                };
                await _api.PostAsJsonAsync("ReservaHabitacion", detalle);
            }

            // 6.4) Asignar servicios con precio real
            foreach (var sInfo in todosSrv)
            {
                var detalle = new ReservaServicio
                {
                    NumReserva = creada!.NumReserva,
                    NumServicio = sInfo.NumServicio,
                    PrecioServicio = sInfo.PrecioServicio
                };
                await _api.PostAsJsonAsync("ReservaServicios", detalle);
            }

            return RedirectToAction(
    actionName: "Pagar",
    controllerName: "Pagos",
    routeValues: new { reservaId = creada.NumReserva, monto = total }
);

        }

        // 7) Mostrar confirmación con detalles
        public async Task<IActionResult> Confirmacion(int id)
        {
            // 1. Reserva básica
            var reserva = await _api.GetFromJsonAsync<Reserva>($"reservas/{id}");
            if (reserva == null) return NotFound();

            // 2. Detalle de habitaciones
            reserva.DetalleHabitaciones = await _api
                .GetFromJsonAsync<List<ReservaHabitacion>>(
                    $"ReservaHabitacion/habitacionesPorReserva/{id}")
                ?? new();

            // 3. Detalle de servicios (mapping)
            var mapaServicios = await _api
                .GetFromJsonAsync<List<ReservaServicio>>(
                    $"ReservaServicios/serviciosPorReserva/{id}")
                ?? new();
            reserva.DetalleServicios = mapaServicios;

            // 4. Traer TODAS las habitaciones y servicios para resolver nombres
            reserva.Habitaciones = await _api
                .GetFromJsonAsync<List<Habitacion>>("habitaciones")
                ?? new();

            reserva.Servicios = await _api
                .GetFromJsonAsync<List<Servicios>>("servicios")
                ?? new();

            return View(reserva);
        }

        // 8) Cancelar reserva
        [HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> Cancelar(int id)
        {
            var resp = await _api.PutAsync($"reservas/cancelar/{id}", null);
            TempData[resp.IsSuccessStatusCode ? "Success" : "Error"] =
                resp.IsSuccessStatusCode ? "Reserva cancelada" : "No se pudo cancelar";
            return RedirectToAction(nameof(Index));
        }
    }
}