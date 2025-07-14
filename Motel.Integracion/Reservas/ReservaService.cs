using System.Net.Http;
using System.Net.Http.Json;
using System.Text.Json;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace Motel.Integracion.Reservas
{
    public class ReservaService
    {
        private readonly HttpClient _httpClient;

        public ReservaService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        /// <summary>
        /// Llama al endpoint POST /api/Reservas y devuelve la reserva creada.
        /// </summary>
        public async Task<ReservaResponse?> CrearAsync(ReservaRequest request)
        {
            var resp = await _httpClient.PostAsJsonAsync("Reservas", request);

            if (!resp.IsSuccessStatusCode)
                return null;

            // Deserializamos el JSON que devuelve CreatedAtAction en el controller
            return await resp.Content.ReadFromJsonAsync<ReservaResponse>();
        }

        public async Task<List<ReservaResponse>> ObtenerTodasAsync()
            => await _httpClient.GetFromJsonAsync<List<ReservaResponse>>("Reservas")
               ?? new List<ReservaResponse>();

        public async Task<List<ReservaResponse>> ObtenerPagasAsync()
            => await _httpClient.GetFromJsonAsync<List<ReservaResponse>>("Reservas/pagas")
               ?? new List<ReservaResponse>();

        public async Task<List<int>> ObtenerIdsAsync()
            => await _httpClient.GetFromJsonAsync<List<int>>("Reservas/ids")
               ?? new List<int>();

        public async Task<ReservaResponse?> ObtenerPorIdAsync(int numReserva)
            => await _httpClient.GetFromJsonAsync<ReservaResponse>($"Reservas/{numReserva}");

        public async Task<List<ReservaResponse>> ObtenerPorClienteAsync(int clienteId)
            => await _httpClient.GetFromJsonAsync<List<ReservaResponse>>($"Reservas/cliente/{clienteId}")
               ?? new List<ReservaResponse>();

        public async Task<List<ReservaResponse>> ObtenerPendientesAsync()
            => await _httpClient.GetFromJsonAsync<List<ReservaResponse>>("Reservas/pendientes")
               ?? new List<ReservaResponse>();

        public async Task<bool> CambiarEstadoAsync(int numReserva, string nuevoEstado)
        {
            var resp = await _httpClient.PutAsJsonAsync($"Reservas/cambiarEstado/{numReserva}", nuevoEstado);
            return resp.IsSuccessStatusCode;
        }

        public async Task<bool> CancelarAsync(int numReserva)
        {
            var resp = await _httpClient.PutAsync($"Reservas/cancelar/{numReserva}", null);
            return resp.IsSuccessStatusCode;
        }

        public async Task<bool> ActualizarAsync(int numReserva, ReservaRequest request)
        {
            var resp = await _httpClient.PutAsJsonAsync($"Reservas/{numReserva}", request);
            return resp.IsSuccessStatusCode;
        }

        public async Task<List<ReservaResponse>> ObtenerActivasAsync()
            => await _httpClient.GetFromJsonAsync<List<ReservaResponse>>("Reservas/reservasActivas")
               ?? new List<ReservaResponse>();
    }
}