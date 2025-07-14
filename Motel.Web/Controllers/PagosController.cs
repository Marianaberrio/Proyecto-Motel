using Microsoft.AspNetCore.Mvc;
using Motel.Web.Models;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using System;
using System.Collections.Generic;

namespace Motel.Web.Controllers
{
    public class PagosController : Controller
    {
        private readonly HttpClient _api;
        public PagosController(IHttpClientFactory factory)
            => _api = factory.CreateClient("MotelApi");

        public async Task<IActionResult> Index()
        {
            var list = new List<Pagos>();
            try
            {
                var resp = await _api.GetAsync("pagos");
                if (resp.IsSuccessStatusCode)
                    list = await resp.Content.ReadFromJsonAsync<List<Pagos>>();
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