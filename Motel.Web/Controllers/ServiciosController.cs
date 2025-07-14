using Microsoft.AspNetCore.Mvc;
using Motel.Web.Models;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using System;
using System.Collections.Generic;

namespace Motel.Web.Controllers
{
    public class ServiciosController : Controller
    {
        private readonly HttpClient _api;
        public ServiciosController(IHttpClientFactory factory)
            => _api = factory.CreateClient("MotelApi");

        // GET: /Servicios
        public async Task<IActionResult> Index()
        {
            var servicios = new List<Servicios>();

            try
            {
                var response = await _api.GetAsync("servicios");

                if (!response.IsSuccessStatusCode)
                {
                    var errorMsg = await response.Content.ReadAsStringAsync();
                    TempData["Error"] = $"Error al cargar servicios: {errorMsg}";
                    return View(servicios);
                }

                servicios = await response.Content.ReadFromJsonAsync<List<Servicios>>();
            }
            catch (Exception ex)
            {
                TempData["Error"] = $"Excepción: {ex.Message}";
            }

            return View(servicios);
        }
    }
}