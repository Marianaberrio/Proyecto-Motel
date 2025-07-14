using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using Motel.Integracion.Requests;
using Motel.Integracion.Responses;

namespace Motel.Integracion.Usuarios
{
    public class UsuarioService
    {
        private readonly HttpClient _httpClient;

        public UsuarioService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<bool> CrearAsync(UsuarioRequest request)
        {
            var response = await _httpClient.PostAsJsonAsync("Usuarios/agregar", request);
            return response.IsSuccessStatusCode;
        }

        public async Task<List<UsuarioResponse>> ObtenerTodosAsync()
        {
            return await _httpClient.GetFromJsonAsync<List<UsuarioResponse>>("Usuarios") ?? new();
        }

        public async Task<UsuarioResponse?> BuscarPorIdAsync(int id)
        {
            return await _httpClient.GetFromJsonAsync<UsuarioResponse>($"Usuarios/buscarPorId/{id}");
        }

        public async Task<UsuarioResponse?> BuscarPorNombreAsync(string usuario)
        {
            return await _httpClient.GetFromJsonAsync<UsuarioResponse>($"Usuarios/buscarPorNombre/{usuario}");
        }

        public async Task<bool> ModificarPorIdAsync(int id, UsuarioRequest request)
        {
            var content = new StringContent(JsonSerializer.Serialize(request), Encoding.UTF8, "application/json");
            var response = await _httpClient.PutAsync($"Usuarios/modificarPorId/{id}", content);
            return response.IsSuccessStatusCode;
        }

        public async Task<bool> ModificarPorNombreAsync(string usuario, UsuarioRequest request)
        {
            var content = new StringContent(JsonSerializer.Serialize(request), Encoding.UTF8, "application/json");
            var response = await _httpClient.PutAsync($"Usuarios/modificarPorNombre/{usuario}", content);
            return response.IsSuccessStatusCode;
        }

        public async Task<bool> EliminarPorIdAsync(int id)
        {
            var response = await _httpClient.DeleteAsync($"Usuarios/eliminarPorId/{id}");
            return response.IsSuccessStatusCode;
        }

        public async Task<bool> EliminarPorNombreAsync(string usuario)
        {
            var response = await _httpClient.DeleteAsync($"Usuarios/eliminarPorNombre/{usuario}");
            return response.IsSuccessStatusCode;
        }
    }
}
