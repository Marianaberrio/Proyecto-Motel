using System.Net.Http;
using System.Net.Http.Json;

namespace Motel.Integracion.Clientes
{
    public class ClienteService
    {
        private readonly HttpClient _http;

        public ClienteService(HttpClient httpClient)
        {
            _http = httpClient;
        }
        public async Task<bool> CrearClienteAsync(ClienteRequest cliente)
        {
            var response = await _http.PostAsJsonAsync("Clientes", cliente);
            return response.IsSuccessStatusCode;
        }

        public async Task<List<ClienteResponse>> ObtenerTodosAsync()
        {
            var response = await _http.GetAsync("Clientes");
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<List<ClienteResponse>>() ?? new();
        }

        public async Task<ClienteResponse?> ObtenerPorIdAsync(int id)
        {
            var response = await _http.GetAsync($"Clientes/{id}");
            if (!response.IsSuccessStatusCode)
                return null;

            return await response.Content.ReadFromJsonAsync<ClienteResponse>();
        }

        public async Task<bool> ActualizarClienteAsync(int id, ClienteRequest cliente)
        {
            var response = await _http.PutAsJsonAsync($"Clientes/{id}", cliente);
            return response.IsSuccessStatusCode;
        }

        public async Task<bool> EliminarClienteAsync(int id)
        {
            var response = await _http.DeleteAsync($"Clientes/{id}");
            return response.IsSuccessStatusCode;
        }

        public async Task<ClienteResponse?> BuscarPorCorreoAsync(string correo)
        {
            var response = await _http.GetAsync($"Clientes/buscar?email={correo}");
            if (!response.IsSuccessStatusCode)
                return null;

            return await response.Content.ReadFromJsonAsync<ClienteResponse>();
        }

        public async Task<List<int>> ObtenerIdsAsync()
        {
            var response = await _http.GetAsync("Clientes/ids");
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<List<int>>() ?? new();
        }

        public async Task<List<string>> ObtenerCorreosAsync()
        {
            var response = await _http.GetAsync("Clientes/correos");
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<List<string>>() ?? new();
        }

        public async Task<int?> ObtenerIdPorCorreoAsync(string correo)
        {
            var response = await _http.GetAsync($"Clientes/obtenerIdPorCorreo/{correo}");
            if (!response.IsSuccessStatusCode)
                return null;

            return await response.Content.ReadFromJsonAsync<int>();
        }
    }
}