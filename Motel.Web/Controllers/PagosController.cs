using Microsoft.AspNetCore.Mvc;
using Motel.Web.Models;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace Motel.Web.Controllers
{
    public class PagosController : Controller
    {
        private readonly HttpClient _api;
        public PagosController(IHttpClientFactory factory)
            => _api = factory.CreateClient("MotelApi");

        // GET /Pagos
        public async Task<IActionResult> Index()
        {
            var list = new List<Pagos>();
            try
            {
                var resp = await _api.GetAsync("pagos");
                if (resp.IsSuccessStatusCode)
                    list = await resp.Content.ReadFromJsonAsync<List<Pagos>>();
                else
                    TempData["Error"] = $"API error: {resp.StatusCode}";
            }
            catch (Exception ex)
            {
                TempData["Error"] = $"Error de conexión: {ex.Message}";
            }
            return View(list);
        }

        // GET /Pagos/Pagar?reservaId=123&monto=456.78
        [HttpGet]
        public async Task<IActionResult> Pagar(int reservaId, decimal monto)
        {
            // Comprobar si ya existe un pago para esta reserva
            var respCheck = await _api.GetAsync($"pagos/reserva/{reservaId}");
            if (respCheck.IsSuccessStatusCode)
            {
                // Ya hay un pago registrado, redirijo a confirmación de la reserva
                TempData["Info"] = "Esta reserva ya tiene un pago registrado.";
                return RedirectToAction("Confirmacion", "Reservas", new { id = reservaId });
            }

            // Preparar ViewModel para realizar el pago
            var vm = new Pagos
            {
                NumReserva = reservaId,
                MontoPago = monto,
                FechaPago = DateTime.Now,
                MetodoPago = "Tarjeta",
                EstadoPago = "Pendiente"
            };
            return View(vm);
        }

        // POST /Pagos/Pagar
        [HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> Pagar(Pagos model)
        {
            if (!ModelState.IsValid)
                return View(model);

            try
            {
                var resp = await _api.PostAsJsonAsync("pagos", model);
                if (!resp.IsSuccessStatusCode)
                {
                    TempData["Error"] = "No se pudo procesar el pago.";
                    return View(model);
                }

                TempData["Success"] = "Pago registrado con éxito.";
                return RedirectToAction("Confirmacion", "Reservas", new { id = model.NumReserva });
            }
            catch (Exception ex)
            {
                TempData["Error"] = $"Error de conexión: {ex.Message}";
                return View(model);
            }
        }

        // POST /Pagos/Cancelar/{numPago}
        [HttpPost]
        public async Task<IActionResult> Cancelar(int numPago)
        {
            try
            {
                var resp = await _api.PutAsync($"pagos/cancelar/{numPago}", null);
                if (!resp.IsSuccessStatusCode)
                    TempData["Error"] = "No se pudo cancelar el pago.";
                else
                    TempData["Success"] = "Pago cancelado.";
            }
            catch (Exception ex)
            {
                TempData["Error"] = $"Error de conexión: {ex.Message}";
            }

            return RedirectToAction(nameof(Index));
        }
    }
}