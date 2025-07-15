using Microsoft.AspNetCore.Mvc;
using Motel.Web.Models;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace Motel.Web.Controllers
{
    public class ReservasController : Controller
    {
        private readonly HttpClient _api;

        public ReservasController(IHttpClientFactory factory)
        {
            _api = factory.CreateClient("MotelIntegracion");
        }

        // 1) Lista todas las reservas del cliente
        public async Task<IActionResult> Index()
        {
            var cid = HttpContext.Session.GetInt32("ClienteId");
            if (!cid.HasValue)
                return RedirectToAction("Login", "Auth");

            var reservas = new List<Reserva>();
            try
            {
                var resp = await _api.GetAsync($"Reservas/cliente/{cid.Value}");
                if (resp.IsSuccessStatusCode)
                    reservas = await resp.Content.ReadFromJsonAsync<List<Reserva>>() ?? new();
                else
                    TempData["Error"] = $"API error: {resp.StatusCode}";
            }
            catch (Exception ex)
            {
                TempData["Error"] = $"Error de conexión: {ex.Message}";
            }

            return View(reservas);
        }

        // 2) Formulario para buscar disponibilidad
        // GET: /Reservas/BuscarDisponibilidad
        [HttpGet]
        public async Task<IActionResult> BuscarDisponibilidad()
        {
            // 1) Traer todas las habitaciones
            var allHabs = await _api
                .GetFromJsonAsync<List<Habitacion>>("Habitaciones")
                     ?? new List<Habitacion>();

            // 2) Extraer tipos únicos
            var tipos = allHabs
                .Select(h => h.TipoHabitacion)
                .Where(t => !string.IsNullOrWhiteSpace(t))
                .Distinct()
                .ToList();

            // 3) Armar el VM con una fila inicial
            var vm = new BuscarHabitacionesViewModel
            {
                TodosLosTipos = tipos,
                TiposCantidad = new List<TipoCantidad>
                {
                    new TipoCantidad
                    {
                        Tipo     = tipos.FirstOrDefault() ?? "",
                        Cantidad = 1
                    }
                },
                FechaEntrada = DateTime.Now,
                FechaSalida = DateTime.Now.AddHours(1)
            };

            return View(vm);
        }

        // POST: /Reservas/HabitacionesDisponibles
        [HttpPost]
        public async Task<IActionResult> HabitacionesDisponibles(BuscarHabitacionesViewModel vm)
        {
            if (!ModelState.IsValid)
                return View("BuscarDisponibilidad", vm);

            // Validar fechas aquí si lo necesitas...

            var listaTotal = new List<Habitacion>();
            foreach (var tc in vm.TiposCantidad)
            {
                var resp = await _api.GetAsync(
                    $"Habitaciones/disponibles/{Uri.EscapeDataString(tc.Tipo)}/{tc.Cantidad}"
                );

                if (!resp.IsSuccessStatusCode)
                {
                    ModelState.AddModelError("",
                        $"No hay {tc.Cantidad} habitaciones disponibles de tipo “{tc.Tipo}”.");
                    continue;
                }

                var subset = await resp.Content
                    .ReadFromJsonAsync<List<Habitacion>>()
                    ?? new List<Habitacion>();

                listaTotal.AddRange(subset);
            }

            if (!listaTotal.Any())
            {
                // recargar tipos desde la misma fuente
                var allHabs = await _api
                    .GetFromJsonAsync<List<Habitacion>>("Habitaciones")
                         ?? new List<Habitacion>();

                vm.TodosLosTipos = allHabs
                    .Select(h => h.TipoHabitacion)
                    .Where(t => !string.IsNullOrWhiteSpace(t))
                    .Distinct()
                    .ToList();

                return View("BuscarDisponibilidad", vm);
            }

            // Guardar fechas y mostrar resultados
            TempData["FechaEntrada"] = vm.FechaEntrada.ToString("o");
            TempData["FechaSalida"] = vm.FechaSalida.ToString("o");
            return View("HabitacionesDisponibles", listaTotal);
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

            TempData["HabitacionIds"] = habitacionIds;
            TempData.Keep("HabitacionIds");
            TempData.Keep("FechaEntrada");
            TempData.Keep("FechaSalida");

            var servicios = new List<Servicios>();
            try
            {
                var resp = await _api.GetAsync("Servicios");
                if (resp.IsSuccessStatusCode)
                    servicios = await resp.Content.ReadFromJsonAsync<List<Servicios>>() ?? new();
                else
                    TempData["Error"] = $"API error: {resp.StatusCode}";
            }
            catch (Exception ex)
            {
                TempData["Error"] = $"Error de conexión: {ex.Message}";
            }

            return View("ElegirServicios", servicios);
        }

        // 5) Mostrar datos del cliente y pasar habitaciones + servicios
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

        // 6) Confirmar reserva → crea cliente, reserva y redirige a pago
        [HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> ConfirmarReserva(
    Cliente cliente,
    DateTime fechaEntrada,
    DateTime fechaSalida,
    string servicioIds)
        {
            if (!ModelState.IsValid)
                return View("DatosCliente", cliente);

            // 1) Registrar o recuperar cliente
            var cid = HttpContext.Session.GetInt32("ClienteId");
            if (!cid.HasValue)
            {
                var respCli = await _api.PostAsJsonAsync("Clientes", cliente);
                if (!respCli.IsSuccessStatusCode)
                {
                    TempData["Error"] = "No se pudo registrar el cliente.";
                    return View("DatosCliente", cliente);
                }
                var creadoCli = await respCli.Content.ReadFromJsonAsync<Cliente>();
                cid = creadoCli!.NumCliente;
                HttpContext.Session.SetInt32("ClienteId", cid.Value);
            }

            // 2) Calcular horas y precio total
            decimal horas = (decimal)(fechaSalida - fechaEntrada).TotalHours;
            decimal total = 0m;

            var habIds = (TempData["HabitacionIds"]?.ToString() ?? "")
                .Split(',', StringSplitOptions.RemoveEmptyEntries);
            var srvIds = (servicioIds ?? "")
                .Split(',', StringSplitOptions.RemoveEmptyEntries);

            // 3) Sumar precio de cada habitación
            foreach (var h in habIds)
            {
                var respH = await _api.GetAsync($"Habitaciones/byid/{h}");
                if (!respH.IsSuccessStatusCode) continue;

                var hInfo = await respH.Content.ReadFromJsonAsync<Habitacion>();
                if (hInfo == null) continue;

                total += hInfo.PrecioHabitacion * horas;
            }

            // 4) Sumar precio de cada servicio
            foreach (var s in srvIds)
            {
                var respS = await _api.GetAsync($"Servicios/{s}");
                if (!respS.IsSuccessStatusCode) continue;

                var sInfo = await respS.Content.ReadFromJsonAsync<Servicios>();
                if (sInfo == null) continue;

                total += sInfo.PrecioServicio;
            }

            // 5) Crear la reserva
            var nueva = new
            {
                NumCliente = cid.Value,
                FechaEntrada = fechaEntrada,
                FechaSalida = fechaSalida,
                EstadoReserva = "Confirmada",
                TotalReserva = total,
                FechaReserva = DateTime.Now,
                ComentarioReserva = $"Estadía: {horas:0.##}h"
            };

            var respRes = await _api.PostAsJsonAsync("Reservas", nueva);
            if (!respRes.IsSuccessStatusCode)
            {
                TempData["Error"] = "No se pudo crear la reserva.";
                return View("DatosCliente", cliente);
            }

            // 6) Leer reserva creada (case-insensitive)
            var creada = await respRes.Content.ReadFromJsonAsync<Reserva>(new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            });
            if (creada == null)
            {
                TempData["Error"] = "No se recibió la reserva creada.";
                return View("DatosCliente", cliente);
            }

            // 7) Guardar cada detalle de habitación con su precio real
            foreach (var h in habIds)
            {
                var respH = await _api.GetAsync($"Habitaciones/byid/{h}");
                if (!respH.IsSuccessStatusCode) continue;

                var hInfo = await respH.Content.ReadFromJsonAsync<Habitacion>();
                if (hInfo == null) continue;

                var detalleHab = new ReservaHabitacion
                {
                    NumReserva = creada.NumReserva,
                    IdHabitacion = hInfo.IdHabitacion,
                    PrecioHabitacion = hInfo.PrecioHabitacion
                };
                await _api.PostAsJsonAsync("ReservaHabitacion", detalleHab);
            }

            // 8) Guardar cada detalle de servicio con su precio real
            foreach (var s in srvIds)
            {
                var respS = await _api.GetAsync($"Servicios/{s}");
                if (!respS.IsSuccessStatusCode) continue;

                var sInfo = await respS.Content.ReadFromJsonAsync<Servicios>();
                if (sInfo == null) continue;

                var detalleSrv = new ReservaServicio
                {
                    NumReserva = creada.NumReserva,
                    NumServicio = sInfo.NumServicio,
                    PrecioServicio = sInfo.PrecioServicio
                };
                await _api.PostAsJsonAsync("ReservaServicios", detalleSrv);
            }

            // 9) Redirigir al pago con ID y monto
            return RedirectToAction("Pagar", "Pagos", new
            {
                reservaId = creada.NumReserva,
                monto = total
            });
        }

        // 7) Confirmación final
        [HttpGet]
        public async Task<IActionResult> Confirmacion(int id)
        {
            // 1) Traer la reserva
            var reserva = await _api.GetFromJsonAsync<Reserva>($"Reservas/{id}");
            if (reserva == null)
                return NotFound();

            // 2) Traer los detalles de habitación (ruta correcta)
            var detallesHab = await _api
                .GetFromJsonAsync<List<ReservaHabitacion>>(
                    $"ReservaHabitacion/habitacionesPorReserva/{id}"
                ) ?? new List<ReservaHabitacion>();

            // 3) Traer el catálogo completo de habitaciones
            var catalogoHab = await _api
                .GetFromJsonAsync<List<Habitacion>>("Habitaciones")
                ?? new List<Habitacion>();

            // 4) Traer los detalles de servicio (ruta correcta)
            var detallesSrv = await _api
                .GetFromJsonAsync<List<ReservaServicio>>(
                    $"ReservaServicios/serviciosPorReserva/{id}"
                ) ?? new List<ReservaServicio>();

            // 5) Traer el catálogo completo de servicios
            var catalogoSrv = await _api
                .GetFromJsonAsync<List<Servicios>>("Servicios")
                ?? new List<Servicios>();

            // 6) Empaquetar en el ViewModel y pasar a la vista
            var vm = new ConfirmacionReservaViewModel
            {
                Reserva = reserva,
                DetalleHabitaciones = detallesHab,
                Habitaciones = catalogoHab,
                DetalleServicios = detallesSrv,
                Servicios = catalogoSrv
            };

            return View(vm);
        }

        // 8) Cancelar reserva
        [HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> Cancelar(int id)
        {
            var resp = await _api.PutAsync($"Reservas/cancelar/{id}", null);
            TempData[resp.IsSuccessStatusCode ? "Success" : "Error"] =
                resp.IsSuccessStatusCode ? "Reserva cancelada" : "No se pudo cancelar";
            return RedirectToAction(nameof(Index));
        }
    }
}