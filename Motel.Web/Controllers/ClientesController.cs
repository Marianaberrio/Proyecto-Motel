using Microsoft.AspNetCore.Mvc;
using Motel.Web.Models;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;

namespace Motel.Web.Controllers
{
    public class ClientesController : Controller
    {
        private readonly HttpClient _api;
        public ClientesController(IHttpClientFactory factory)
            => _api = factory.CreateClient("MotelApi");

        public async Task<IActionResult> Index()
        {
            var list = new List<Cliente>();
            try
            {
                var resp = await _api.GetAsync("clientes");
                if (resp.IsSuccessStatusCode)
                    list = await resp.Content.ReadFromJsonAsync<List<Cliente>>();
                else
                    TempData["Error"] = $"API error {resp.StatusCode}";
            }
            catch (Exception ex)
            {
                TempData["Error"] = $"Error de conexión: {ex.Message}";
            }
            return View(list);
        }

        public async Task<IActionResult> Details(int id)
        {
            try
            {
                var resp = await _api.GetAsync($"clientes/{id}");
                if (!resp.IsSuccessStatusCode) return NotFound();
                var cliente = await resp.Content.ReadFromJsonAsync<Cliente>();
                return View(cliente);
            }
            catch
            {
                return NotFound();
            }
        }

        public IActionResult Create() => View();

        [HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Cliente model)
        {
            if (!ModelState.IsValid) return View(model);

            var resp = await _api.PostAsJsonAsync("clientes", model);
            if (resp.IsSuccessStatusCode)
                return RedirectToAction(nameof(Index));

            ModelState.AddModelError("", "Error al crear cliente");
            return View(model);
        }

        // Perfil / Edit
        public IActionResult Profile()
        {
            var id = HttpContext.Session.GetInt32("ClienteId");
            if (!id.HasValue) return RedirectToAction("Login", "Auth");
            return RedirectToAction("Edit", new { id = id.Value });
        }

        public async Task<IActionResult> Edit(int id)
        {
            try
            {
                var resp = await _api.GetAsync($"clientes/{id}");
                if (!resp.IsSuccessStatusCode) return NotFound();
                var cliente = await resp.Content.ReadFromJsonAsync<Cliente>();
                return View(cliente);
            }
            catch
            {
                return NotFound();
            }
        }

        [HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Cliente model)
        {
            if (!ModelState.IsValid) return View(model);

            var resp = await _api.PutAsJsonAsync($"clientes/{model.NumCliente}", model);
            if (!resp.IsSuccessStatusCode)
            {
                TempData["Error"] = "No se pudo actualizar perfil";
                return View(model);
            }

            TempData["Success"] = "Perfil actualizado";
            return RedirectToAction(nameof(Edit), new { id = model.NumCliente });
        }

        [HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteAccount()
        {
            var id = HttpContext.Session.GetInt32("ClienteId");
            if (!id.HasValue) return RedirectToAction("Login", "Auth");

            var resp = await _api.DeleteAsync($"clientes/{id.Value}");
            if (!resp.IsSuccessStatusCode)
            {
                TempData["Error"] = "No se pudo eliminar cuenta";
                return RedirectToAction(nameof(Edit), new { id = id.Value });
            }

            HttpContext.Session.Clear();
            return RedirectToAction("Index", "Home");
        }
    }
}