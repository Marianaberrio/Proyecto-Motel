using Microsoft.AspNetCore.Mvc;
using Motel.Web.Models;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace Motel.Web.Controllers
{
    public class ClientesController : Controller
    {
        private readonly HttpClient _api;

        public ClientesController(IHttpClientFactory factory)
        {
            _api = factory.CreateClient("MotelApi");
        }

        // GET: /Clientes/Profile
        // Muestra el perfil (formulario de edición) del cliente logueado
        public async Task<IActionResult> Profile()
        {
            var clienteId = HttpContext.Session.GetInt32("ClienteId");
            if (!clienteId.HasValue)
                return RedirectToAction("Login", "Auth");

            var resp = await _api.GetAsync($"clientes/{clienteId.Value}");
            if (!resp.IsSuccessStatusCode)
            {
                TempData["Error"] = "No se pudo cargar los datos del perfil.";
                return RedirectToAction("Index", "Home");
            }

            var cliente = await resp.Content.ReadFromJsonAsync<Cliente>();
            return View(cliente);
        }

        // POST: /Clientes/UpdateProfile
        // Recibe el formulario de edición de perfil
        [HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> UpdateProfile(Cliente model)
        {
            if (!ModelState.IsValid)
                return View("Profile", model);

            var resp = await _api.PutAsJsonAsync($"clientes/{model.NumCliente}", model);
            if (!resp.IsSuccessStatusCode)
            {
                TempData["Error"] = "No se pudo actualizar tu perfil.";
                return View("Profile", model);
            }

            TempData["Success"] = "Perfil actualizado con éxito.";
            return RedirectToAction("Profile");
        }

        // POST: /Clientes/DeleteAccount
        // Elimina la cuenta del cliente logueado y cierra sesión
        [HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteAccount()
        {
            var clienteId = HttpContext.Session.GetInt32("ClienteId");
            if (!clienteId.HasValue)
                return RedirectToAction("Login", "Auth");

            var resp = await _api.DeleteAsync($"clientes/{clienteId.Value}");
            if (!resp.IsSuccessStatusCode)
            {
                TempData["Error"] = "No se pudo eliminar tu cuenta.";
                return RedirectToAction("Profile");
            }

            HttpContext.Session.Clear();
            return RedirectToAction("Index", "Home");
        }
    }
}