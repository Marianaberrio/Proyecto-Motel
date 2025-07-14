using Microsoft.AspNetCore.Mvc;
using Motel.Web.Models;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace Motel.Web.Controllers
{
    public class HabitacionesController : Controller
    {
        private readonly HttpClient _api;

        public HabitacionesController(IHttpClientFactory factory)
        {
            _api = factory.CreateClient("MotelIntegracion");
        }

        // GET: /Habitaciones
        public async Task<IActionResult> Index()
        {
            var list = new List<Habitacion>();
            try
            {
                // BaseAddress = https://localhost:5268/api/
                // Solo ponemos el controlador: "Habitaciones"
                var resp = await _api.GetAsync("Habitaciones");
                if (resp.IsSuccessStatusCode)
                {
                    list = await resp.Content.ReadFromJsonAsync<List<Habitacion>>();
                }
                else
                {
                    TempData["Error"] = $"API error: {resp.StatusCode}";
                }
            }
            catch (Exception ex)
            {
                TempData["Error"] = $"Error de conexión: {ex.Message}";
            }

            return View(list);
        }
    }
}