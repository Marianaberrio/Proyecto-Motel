using Microsoft.AspNetCore.Mvc;
using Motel.Web.Models;
using Newtonsoft.Json;
using System.Net.Http.Headers;

namespace Motel.Web.Controllers
{
    public class PagosController : Controller
    {
        private readonly HttpClient _httpClient;

        public PagosController()
        {
            _httpClient = new HttpClient
            {
                BaseAddress = new Uri("http://localhost:5264/") // Ajusta si tu puerto es diferente
            };
            _httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }

        public async Task<IActionResult> Index()
        {
            List<Pagos> listaPagos = new List<Pagos>();

            HttpResponseMessage respuesta = await _httpClient.GetAsync("api/Pagos"); // Ruta exacta de tu API

            if (respuesta.IsSuccessStatusCode)
            {
                string jsonRespuesta = await respuesta.Content.ReadAsStringAsync();
                listaPagos = JsonConvert.DeserializeObject<List<Pagos>>(jsonRespuesta);
            }

            return View(listaPagos);
        }
    }
}