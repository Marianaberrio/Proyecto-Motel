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
        [HttpGet]
        public IActionResult BuscarDisponibilidad()
        {
            if (!HttpContext.Session.GetInt32("ClienteId").HasValue)
                return RedirectToAction("Login", "Auth");

            return View();
        }

        // 3) Mostrar habitaciones disponibles
        [HttpPost]
        public async Task<IActionResult> HabitacionesDisponibles(BuscarHabitacionesViewModel vm)
        {
            if (!HttpContext.Session.GetInt32("ClienteId").HasValue)
                return RedirectToAction("Login", "Auth");

            if (vm.FechaEntrada < DateTime.Now)
            {
                TempData["Error"] = "La fecha de entrada no puede ser anterior a hoy.";
                return RedirectToAction("BuscarDisponibilidad");
            }
            if (vm.FechaSalida <= vm.FechaEntrada)
            {
                TempData["Error"] = "La fecha de salida debe ser posterior a la de entrada.";
                return RedirectToAction("BuscarDisponibilidad");
            }
            if ((vm.FechaSalida - vm.FechaEntrada).TotalHours < 1)
            {
                TempData["Error"] = "La estadía mínima es de 1 hora.";
                return RedirectToAction("BuscarDisponibilidad");
            }
            if (string.IsNullOrWhiteSpace(vm.TipoHabitacion) || vm.Cantidad < 1)
            {
                TempData["Error"] = "Debes indicar tipo de habitación y cantidad válida.";
                return RedirectToAction("BuscarDisponibilidad");
            }

            TempData["FechaEntrada"] = vm.FechaEntrada.ToString("o");
            TempData["FechaSalida"] = vm.FechaSalida.ToString("o");
            TempData.Keep("FechaEntrada");
            TempData.Keep("FechaSalida");

            var url = $"Habitaciones/disponibles/{Uri.EscapeDataString(vm.TipoHabitacion)}/{vm.Cantidad}";
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

            var cid = HttpContext.Session.GetInt32("ClienteId");
            if (!cid.HasValue)
            {
                var respCli = await _api.PostAsJsonAsync("Clientes", cliente);
                var creado = await respCli.Content.ReadFromJsonAsync<Cliente>();
                cid = creado!.NumCliente;
                HttpContext.Session.SetInt32("ClienteId", cid.Value);
            }

            decimal horas = (decimal)(fechaSalida - fechaEntrada).TotalHours;
            decimal total = 0m;
            var habIds = TempData["HabitacionIds"]!.ToString()!
                            .Split(',', StringSplitOptions.RemoveEmptyEntries);
            var srvIds = (servicioIds ?? "")
                            .Split(',', StringSplitOptions.RemoveEmptyEntries);

            foreach (var h in habIds)
            {
                var r = await _api.GetAsync($"Habitaciones/byid/{h}");
                if (r.IsSuccessStatusCode)
                {
                    var hInfo = await r.Content.ReadFromJsonAsync<Habitacion>();
                    if (hInfo != null)
                        total += hInfo.PrecioHabitacion * horas;
                }
            }

            foreach (var s in srvIds)
            {
                var r = await _api.GetAsync($"Servicios/{s}");
                if (r.IsSuccessStatusCode)
                {
                    var sInfo = await r.Content.ReadFromJsonAsync<Servicios>();
                    if (sInfo != null)
                        total += sInfo.PrecioServicio;
                }
            }

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

            // 👉 Aquí deserializas correctamente con case-insensitive
            var creada = await respRes.Content.ReadFromJsonAsync<Reserva>(
                new JsonSerializerOptions { PropertyNameCaseInsensitive = true }
            );
            if (creada == null)
            {
                TempData["Error"] = "No se recibió la reserva creada.";
                return View("DatosCliente", cliente);
            }

            // Asignar habitaciones
            foreach (var h in habIds)
            {
                var detalle = new ReservaHabitacion
                {
                    NumReserva = creada.NumReserva,
                    IdHabitacion = int.Parse(h),
                    PrecioHabitacion = 0
                };
                await _api.PostAsJsonAsync("ReservaHabitacion", detalle);
            }

            // Asignar servicios
            foreach (var s in srvIds)
            {
                var detalle = new ReservaServicio
                {
                    NumReserva = creada.NumReserva,
                    NumServicio = int.Parse(s),
                    PrecioServicio = 0
                };
                await _api.PostAsJsonAsync("ReservaServicios", detalle);
            }

            // Redirigir a pago
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
            var reserva = await _api.GetFromJsonAsync<Reserva>($"Reservas/{id}");
            if (reserva == null) return NotFound();
            return View(reserva);
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