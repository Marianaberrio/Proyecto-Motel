using System.Net.Http;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using Motel.Integracion.Servicios;


namespace Motel.Integracion.ReservaServicios
{
    public class ReservaServiciosService
    {
        private readonly HttpClient _httpClient;

        public ReservaServiciosService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<List<ReservaServicioResponse>> ObtenerTodosAsync()
        {
            return await _httpClient.GetFromJsonAsync<List<ReservaServicioResponse>>("ReservaServicios/todasReservaServicios") ?? new();
        }

        public async Task<object?> ObtenerServicioPorNombreAsync(string nombre)
        {
            return await _httpClient.GetFromJsonAsync<object>($"ReservaServicios/servicio/{nombre}");
        }

        public async Task<List<ReservaServicioResponse>> ObtenerServiciosPorReservaAsync(int numReserva)
        {
            return await _httpClient.GetFromJsonAsync<List<ReservaServicioResponse>>(
                $"ReservaServicios/serviciosPorReserva/{numReserva}") ?? new();
        }

        public async Task<bool> EliminarAsync(int numReserva, int numServicio)
        {
            var response = await _httpClient.DeleteAsync($"ReservaServicios/{numReserva}/{numServicio}");
            return response.IsSuccessStatusCode;
        }

        public async Task<bool> CrearAsync(ReservaServicioRequest request)
        {
            var response = await _httpClient.PostAsJsonAsync("ReservaServicios", request);
            return response.IsSuccessStatusCode;
        }

        public async Task<List<int>> ObtenerIdsReservasAsync()
        {
            return await _httpClient.GetFromJsonAsync<List<int>>("ReservaServicios/ids/reservas") ?? new();
        }

        public async Task<List<int>> ObtenerIdsServiciosAsync()
        {
            return await _httpClient.GetFromJsonAsync<List<int>>("ReservaServicios/ids/servicios") ?? new();
        }

        public async Task<List<ServicioLiteResponse>> ObtenerNombresPreciosIdsAsync()
        {
            return await _httpClient.GetFromJsonAsync<List<ServicioLiteResponse>>("ReservaServicios/nombres-precios-ids") ?? new();
        }

        public async Task<bool> ActualizarServicioPorNombreAsync(string nombreServicio, ServicioRequest request)
        {
            var content = new StringContent(JsonSerializer.Serialize(request), Encoding.UTF8, "application/json");
            var response = await _httpClient.PutAsync($"ReservaServicios/servicio/{nombreServicio}", content);
            return response.IsSuccessStatusCode;
        }
    }
}
