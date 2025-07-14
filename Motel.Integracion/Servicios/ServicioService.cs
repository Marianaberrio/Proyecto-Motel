using System.Net.Http;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;

namespace Motel.Integracion.Servicios
{
    public class ServicioService
    {
        private readonly HttpClient _httpClient;

        public ServicioService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<bool> CrearAsync(ServicioRequest request)
        {
            var response = await _httpClient.PostAsJsonAsync("Servicios", request);
            return response.IsSuccessStatusCode;
        }

        public async Task<List<ServicioResponse>> ObtenerTodosAsync()
        {
            return await _httpClient.GetFromJsonAsync<List<ServicioResponse>>("Servicios") ?? new();
        }

        public async Task<ServicioResponse?> ObtenerPorIdAsync(int numServicio)
        {
            return await _httpClient.GetFromJsonAsync<ServicioResponse>($"Servicios/{numServicio}");
        }

        public async Task<bool> ActualizarAsync(int numServicio, ServicioRequest request)
        {
            var content = new StringContent(JsonSerializer.Serialize(request), Encoding.UTF8, "application/json");
            var response = await _httpClient.PutAsync($"Servicios/{numServicio}", content);
            return response.IsSuccessStatusCode;
        }

        public async Task<bool> EliminarAsync(int numServicio)
        {
            var response = await _httpClient.DeleteAsync($"Servicios/{numServicio}");
            return response.IsSuccessStatusCode;
        }
    }
}
