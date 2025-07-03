using Microsoft.AspNetCore.Mvc;
using Motel.Web.Models;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace Motel.Web.Controllers
{
    public class HabitacionesController : Controller
    {
        private readonly HttpClient _httpClient;

        public HabitacionesController(IHttpClientFactory factory)
        {
            _httpClient = factory.CreateClient("MotelApi");
        }

        // GET: /Habitaciones
        public async Task<IActionResult> Index()
        {
            List<Habitacion> habitaciones;
            try
            {
                // Ahora existirá este endpoint en tu API Core:
                habitaciones = await _httpClient.GetFromJsonAsync<List<Habitacion>>("Habitaciones");
            }
            catch (Exception ex)
            {
                TempData["Error"] = $"No se pudieron cargar las habitaciones: {ex.Message}";
                habitaciones = new List<Habitacion>();
            }
            return View(habitaciones);
        }
    }
}