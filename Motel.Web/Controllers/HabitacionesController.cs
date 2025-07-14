using Microsoft.AspNetCore.Mvc;
using Motel.Web.Models;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using System;
using System.Collections.Generic;

namespace Motel.Web.Controllers
{
    public class HabitacionesController : Controller
    {
        private readonly HttpClient _api;
        public HabitacionesController(IHttpClientFactory factory)
            => _api = factory.CreateClient("MotelApi");

        public async Task<IActionResult> Index()
        {
            var list = new List<Habitacion>();
            try
            {
                var resp = await _api.GetAsync("habitaciones");
                if (resp.IsSuccessStatusCode)
                    list = await resp.Content.ReadFromJsonAsync<List<Habitacion>>();
                else
                    TempData["Error"] = $"API error {resp.StatusCode}";
            }
            catch (Exception ex)
            {
                TempData["Error"] = $"Error de conexión: {ex.Message}";
            }
            return View(list);
        }
    }
}