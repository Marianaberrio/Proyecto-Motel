using Microsoft.AspNetCore.Mvc;
using Motel.Web.Models;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace Motel.Web.Controllers
{
    public class ClientesController : Controller
    {
        private readonly HttpClient _httpClient;

        public ClientesController(HttpClient httpClient)
        {
            _httpClient = httpClient;
            _httpClient.BaseAddress = new System.Uri("http://localhost:5199/"); // Ajusta si tu puerto es distinto
        }

        // GET: /Clientes
        public async Task<IActionResult> Index()
        {
            try
            {
                var clientes = await _httpClient.GetFromJsonAsync<List<Cliente>>("api/clientes");
                return View(clientes);
            }
            catch
            {
                ModelState.AddModelError(string.Empty, "No se pudo obtener la lista de clientes.");
                return View(new List<Cliente>());
            }
        }

        // GET: /Clientes/Details/5
        public async Task<IActionResult> Details(int id)
        {
            try
            {
                var cliente = await _httpClient.GetFromJsonAsync<Cliente>($"api/clientes/{id}");
                if (cliente == null) return NotFound();
                return View(cliente);
            }
            catch
            {
                return NotFound();
            }
        }

        // GET: /Clientes/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: /Clientes/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Cliente cliente)
        {
            if (!ModelState.IsValid) return View(cliente);

            try
            {
                var response = await _httpClient.PostAsJsonAsync("api/clientes", cliente);
                if (response.IsSuccessStatusCode)
                    return RedirectToAction(nameof(Index));
                else
                {
                    ModelState.AddModelError(string.Empty, "Error al crear cliente.");
                    return View(cliente);
                }
            }
            catch
            {
                ModelState.AddModelError(string.Empty, "Error de conexión con el servidor.");
                return View(cliente);
            }
        }

        // NUEVO: /Clientes/Profile → redirige al Edit con tu sesión
        public IActionResult Profile()
        {
            var id = HttpContext.Session.GetInt32("ClienteId");
            if (!id.HasValue)
                return RedirectToAction("Login", "Auth");

            return RedirectToAction(nameof(Edit), new { id = id.Value });
        }

        // GET: /Clientes/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            try
            {
                var cliente = await _httpClient.GetFromJsonAsync<Cliente>($"api/clientes/{id}");
                if (cliente == null) return NotFound();
                return View(cliente);
            }
            catch
            {
                return NotFound();
            }
        }

        // POST: /Clientes/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Cliente cliente)
        {
            if (id != cliente.NumCliente) return BadRequest();
            if (!ModelState.IsValid) return View(cliente);

            try
            {
                var response = await _httpClient.PutAsJsonAsync($"api/clientes/{id}", cliente);
                if (response.IsSuccessStatusCode)
                {
                    TempData["Success"] = "Perfil actualizado correctamente.";
                    // Volvemos a Edit para que veas tus cambios y el mensaje
                    return RedirectToAction(nameof(Edit), new { id = id });
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Error al actualizar cliente.");
                    return View(cliente);
                }
            }
            catch
            {
                ModelState.AddModelError(string.Empty, "Error de conexión con el servidor.");
                return View(cliente);
            }
        }

        // GET: /Clientes/Delete/5
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var cliente = await _httpClient.GetFromJsonAsync<Cliente>($"api/clientes/{id}");
                if (cliente == null) return NotFound();
                return View(cliente);
            }
            catch
            {
                return NotFound();
            }
        }

        // POST: /Clientes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            try
            {
                var response = await _httpClient.DeleteAsync($"api/clientes/{id}");
                if (response.IsSuccessStatusCode)
                    return RedirectToAction(nameof(Index));
                else
                {
                    ModelState.AddModelError(string.Empty, "Error al eliminar cliente.");
                    var cliente = await _httpClient.GetFromJsonAsync<Cliente>($"api/clientes/{id}");
                    return View(cliente);
                }
            }
            catch
            {
                ModelState.AddModelError(string.Empty, "Error de conexión con el servidor.");
                var cliente = await _httpClient.GetFromJsonAsync<Cliente>($"api/clientes/{id}");
                return View(cliente);
            }
        }
    }
}