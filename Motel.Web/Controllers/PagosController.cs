using Microsoft.AspNetCore.Mvc;
using Motel.Web.Models;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using System.Text.Json;

namespace Motel.Web.Controllers
{
    public class PagosController : Controller
    {
        private readonly HttpClient _api;

        public PagosController(IHttpClientFactory factory)
        {
            // Usa el cliente configurado para Integración
            _api = factory.CreateClient("MotelIntegracion");
        }

        // GET: /Pagos
        public async Task<IActionResult> Index()
        {
            var list = new List<Pagos>();

            try
            {
                var resp = await _api.GetAsync("Pagos");
                if (resp.IsSuccessStatusCode)
                {
                    list = await resp.Content.ReadFromJsonAsync<List<Pagos>>() ?? new();
                }
                else
                {
                    TempData["Error"] = $"API error: {resp.StatusCode}";
                }
            }
            catch (Exception ex)
            {
                TempData["Error"] = $"Conexión fallida: {ex.Message}";
            }

            return View(list);
        }

        // GET: /Pagos/Pagar?reservaId=123&monto=456.78
        [HttpGet]
        public async Task<IActionResult> Pagar(int reservaId, decimal monto)
        {
            var existe = await _api.GetAsync($"Pagos/reserva/{reservaId}");
            if (existe.IsSuccessStatusCode)
            {
                TempData["Info"] = "Esta reserva ya tiene un pago registrado.";
                return RedirectToAction("Confirmacion", "Reservas", new { id = reservaId });
            }

            var vm = new Pagos
            {
                NumReserva = reservaId,
                MontoPago = monto,
                MetodoPago = "Tarjeta",
                EstadoPago = "Pendiente",
                FechaPago = DateTime.Now
            };

            return View(vm);
        }

        // POST: /Pagos/Pagar
        [HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> Pagar(Pagos model)
        {
            model.FechaPago = DateTime.Now;
            model.EstadoPago = "Pendiente";

            ModelState.Remove(nameof(model.FechaPago));
            ModelState.Remove(nameof(model.EstadoPago));

            if (!ModelState.IsValid)
                return View(model);

            var payload = new
            {
                NumReserva = model.NumReserva,
                MontoPago = model.MontoPago,
                MetodoPago = model.MetodoPago,
                EstadoPago = model.EstadoPago,
                FechaPago = model.FechaPago,
                ComentarioPago = model.ComentarioPago
            };

            try
            {
                var resp = await _api.PostAsJsonAsync("Pagos", payload);

                if (resp.StatusCode == System.Net.HttpStatusCode.Created)
                {
                    TempData["Success"] = "Pago registrado con éxito.";
                    return RedirectToAction("Confirmacion", "Reservas", new { id = model.NumReserva });
                }
                else
                {
                    var errorMsg = await resp.Content.ReadAsStringAsync();
                    TempData["Error"] = $"Error al pagar: {errorMsg}";
                    return View(model);
                }
            }
            catch (Exception ex)
            {
                TempData["Error"] = $"Conexión fallida: {ex.Message}";
                return View(model);
            }
        }

        // POST: /Pagos/Cancelar/5
        [HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> Cancelar(int numPago)
        {
            try
            {
                var resp = await _api.PutAsync($"Pagos/cancelar/{numPago}", null);

                if (resp.IsSuccessStatusCode)
                    TempData["Success"] = "Pago cancelado correctamente.";
                else
                    TempData["Error"] = "No se pudo cancelar el pago.";
            }
            catch (Exception ex)
            {
                TempData["Error"] = $"Conexión fallida: {ex.Message}";
            }

            return RedirectToAction(nameof(Index));
        }
    }
}