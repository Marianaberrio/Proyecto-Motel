using Microsoft.AspNetCore.Mvc;
using Motel.Web.Models;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace Motel.Web.Controllers
{
    public class ServiciosController : Controller
    {
        private readonly HttpClient _api;

        public ServiciosController(IHttpClientFactory factory)
        {
            // Usamos el cliente configurado para Integración
            _api = factory.CreateClient("MotelIntegracion");
        }

        // GET: /Servicios
        public async Task<IActionResult> Index()
        {
            var servicios = new List<Servicios>();

            try
            {
                // Como BaseAddress ya incluye /api/, solo usamos "Servicios"
                var response = await _api.GetAsync("Servicios");

                if (!response.IsSuccessStatusCode)
                {
                    var errorMsg = await response.Content.ReadAsStringAsync();
                    TempData["Error"] = $"Error al cargar servicios: {errorMsg}";
                    return View(servicios);
                }

                servicios = await response.Content.ReadFromJsonAsync<List<Servicios>>() ?? new();
            }
            catch (Exception ex)
            {
                TempData["Error"] = $"Error de conexión: {ex.Message}";
            }

            return View(servicios);
        }
    }
}