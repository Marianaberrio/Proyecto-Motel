using System.Net.Http;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using Motel.Integracion.ReservaHabitacion;

namespace Motel.Integracion.ReservaHabitacion
{
    public class ReservaHabitacionService
    {
        private readonly HttpClient _httpClient;

        public ReservaHabitacionService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<List<ReservaHabitacionResponse>> ObtenerPorReservaAsync(int numReserva)
        {
            return await _httpClient.GetFromJsonAsync<List<ReservaHabitacionResponse>>(
                $"ReservaHabitacion/habitacionesPorReserva/{numReserva}") ?? new();
        }

        public async Task<List<ReservaHabitacionResponse>> ObtenerTodasAsync()
        {
            return await _httpClient.GetFromJsonAsync<List<ReservaHabitacionResponse>>(
                "ReservaHabitacion/todasReservaHabitacion") ?? new();
        }

        public async Task<bool> CrearAsync(ReservaHabitacionRequest request)
        {
            var response = await _httpClient.PostAsJsonAsync("ReservaHabitacion", request);
            return response.IsSuccessStatusCode;
        }

        public async Task<bool> EliminarAsync(int numReserva, int idHabitacion)
        {
            var response = await _httpClient.DeleteAsync($"ReservaHabitacion/{numReserva}/{idHabitacion}");
            return response.IsSuccessStatusCode;
        }

        public async Task<List<int>> ObtenerIdsHabitacionesAsync()
        {
            return await _httpClient.GetFromJsonAsync<List<int>>("ReservaHabitacion/habitaciones") ?? new();
        }

        public async Task<List<int>> ObtenerHabitacionesDisponiblesAsync()
        {
            return await _httpClient.GetFromJsonAsync<List<int>>("ReservaHabitacion/habitacionesDisponibles") ?? new();
        }
    }
}
