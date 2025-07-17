using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;
using static caja3.Form1;

namespace caja3
{
    public class UsuarioService
    {
        private readonly HttpClient _httpClient;

        public UsuarioService()
        {
            _httpClient = new HttpClient
            {
                BaseAddress = new Uri("http://localhost:7013/api/")
            };
        }

        public async Task<Usuario> ObtenerPorNombreAsync(string nombre)
        {
            try
            {
                return await _httpClient.GetFromJsonAsync<Usuario>($"usuarios/buscarPorNombre/{nombre}");
            }
            catch
            {
                return null;
            }
        }
    }
}