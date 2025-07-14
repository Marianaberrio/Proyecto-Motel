using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Motel.Integracion.Pagos
{
    public class PagoService
    {
        private readonly HttpClient _httpClient;

        public PagoService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        /// <summary>
        /// Llama POST /api/Pagos y devuelve true si se creó correctamente.
        /// </summary>
        public async Task<bool> CrearPagoAsync(PagoRequest request)
        {
            // Si tu API requiere que FechaPago y EstadoPago vengan aquí:
            // request.FechaPago  = DateTime.Now;
            // request.EstadoPago = "Pendiente";

            var resp = await _httpClient.PostAsJsonAsync("Pagos", request);
            return resp.IsSuccessStatusCode;
        }

        /// <summary>
        /// Llama GET /api/Pagos y devuelve la lista, o lista vacía si falla.
        /// </summary>
        public async Task<List<PagoResponse>> ObtenerPagosAsync()
        {
            try
            {
                return await _httpClient
                    .GetFromJsonAsync<List<PagoResponse>>("Pagos")
                       ?? new List<PagoResponse>();
            }
            catch
            {
                return new List<PagoResponse>();
            }
        }

        /// <summary>
        /// Llama GET /api/Pagos/{numPago}, devuelve null si 404, o el PagoResponse.
        /// </summary>
        public async Task<PagoResponse?> ObtenerPorNumPagoAsync(int numPago)
        {
            var resp = await _httpClient.GetAsync($"Pagos/{numPago}");
            if (resp.StatusCode == HttpStatusCode.NotFound)
                return null;

            resp.EnsureSuccessStatusCode();
            return await resp.Content.ReadFromJsonAsync<PagoResponse>();
        }

        /// <summary>
        /// Llama GET /api/Pagos/reserva/{numReserva}, devuelve null si 404, o el PagoResponse.
        /// </summary>
        public async Task<PagoResponse?> ObtenerPorReservaAsync(int numReserva)
        {
            var resp = await _httpClient.GetAsync($"Pagos/reserva/{numReserva}");
            if (resp.StatusCode == HttpStatusCode.NotFound)
                return null;

            resp.EnsureSuccessStatusCode();
            return await resp.Content.ReadFromJsonAsync<PagoResponse>();
        }

        /// <summary>
        /// Llama PUT /api/Pagos/cancelar/{numPago}, devuelve true si fue exitoso.
        /// </summary>
        public async Task<bool> CancelarPagoAsync(int numPago)
        {
            var resp = await _httpClient.PutAsync($"Pagos/cancelar/{numPago}", null);
            return resp.IsSuccessStatusCode;
        }

        /// <summary>
        /// Llama PUT /api/Pagos/{numPago} con el request para actualizar estado.
        /// </summary>
        public async Task<bool> ActualizarEstadoPagoAsync(int numPago, PagoRequest request)
        {
            var resp = await _httpClient.PutAsJsonAsync($"Pagos/{numPago}", request);
            return resp.IsSuccessStatusCode;
        }

        /// <summary>
        /// Llama PUT /api/Pagos/modificar/{numPago} con el request para modificar pago.
        /// </summary>
        public async Task<bool> ModificarPagoAsync(int numPago, PagoRequest request)
        {
            var resp = await _httpClient.PutAsJsonAsync($"Pagos/modificar/{numPago}", request);
            return resp.IsSuccessStatusCode;
        }
    }
}