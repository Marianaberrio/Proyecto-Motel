using System.Net.Http;
using System.Net.Http.Json;
using System.Collections.Generic;
using System.Threading.Tasks;
using Motel.Integracion.Habitacion;

namespace Motel.Integracion.Habitaciones
{
    public class HabitacionService
    {
        private readonly HttpClient _httpClient;

        public HabitacionService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<bool> CrearHabitacionAsync(HabitacionRequest request)
        {
            var response = await _httpClient.PostAsJsonAsync("Habitaciones", request); // sin "api/"
            return response.IsSuccessStatusCode;
        }

        public async Task<List<HabitacionResponse>> ObtenerTodosAsync()
        {
            var response = await _httpClient.GetAsync("Habitaciones");
            return await response.Content.ReadFromJsonAsync<List<HabitacionResponse>>() ?? new();
        }

        public async Task<HabitacionResponse?> ObtenerPorNumeroAsync(string numHabitacion)
        {
            var response = await _httpClient.GetAsync($"Habitaciones/{numHabitacion}");
            if (!response.IsSuccessStatusCode) return null;
            return await response.Content.ReadFromJsonAsync<HabitacionResponse>();
        }

        public async Task<HabitacionResponse?> ObtenerPorIdAsync(int idHabitacion)
        {
            var response = await _httpClient.GetAsync($"Habitaciones/byid/{idHabitacion}");
            if (!response.IsSuccessStatusCode) return null;
            return await response.Content.ReadFromJsonAsync<HabitacionResponse>();
        }

        public async Task<bool> ActualizarHabitacionAsync(string numHabitacion, HabitacionResponse habitacion)
        {
            var response = await _httpClient.PutAsJsonAsync($"Habitaciones/{numHabitacion}", habitacion);
            return response.IsSuccessStatusCode;
        }

        public async Task<bool> EliminarHabitacionAsync(string numHabitacion)
        {
            var response = await _httpClient.DeleteAsync($"Habitaciones/{numHabitacion}");
            return response.IsSuccessStatusCode;
        }

        public async Task<bool> CambiarEstadoHabitacionAsync(int idHabitacion, string nuevoEstado)
        {
            var content = JsonContent.Create(nuevoEstado);
            var response = await _httpClient.PutAsync($"Habitaciones/cambiarEstado/{idHabitacion}", content);
            return response.IsSuccessStatusCode;
        }

        public async Task<List<HabitacionResponse>> ObtenerHabitacionesDisponiblesAsync()
        {
            var response = await _httpClient.GetAsync("Habitaciones/habitacionesDisponibles");
            return await response.Content.ReadFromJsonAsync<List<HabitacionResponse>>() ?? new();
        }

        public async Task<List<HabitacionResponse>> ObtenerDisponiblesPorTipoCantidadAsync(string tipo, int cantidad)
        {
            var response = await _httpClient.GetAsync($"Habitaciones/disponibles/{tipo}/{cantidad}");

            if (!response.IsSuccessStatusCode)
            {
                // Lee el contenido en texto por si es un mensaje de error o JSON parcial
                var error = await response.Content.ReadAsStringAsync();
                throw new Exception($"Error desde Core: {error}");
            }

            return await response.Content.ReadFromJsonAsync<List<HabitacionResponse>>() ?? new List<HabitacionResponse>();
        }



        public async Task<bool> MarcarComoOcupadasAsync(List<int> ids)
        {
            var response = await _httpClient.PutAsJsonAsync("Habitaciones/ocupar", ids);
            return response.IsSuccessStatusCode;
        }

        public async Task<bool> AsignarHabitacionesAsync(AsignarHabitacionesRequest request)
        {
            var response = await _httpClient.PostAsJsonAsync("Habitaciones/asignar", request);
            return response.IsSuccessStatusCode;
        }
    }
}
