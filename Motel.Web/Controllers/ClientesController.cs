using Microsoft.AspNetCore.Mvc;
using Motel.Web.Models;
using System;
using System.Collections.Generic;
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
            // Ahora apuntamos al servicio de Integración
            _api = factory.CreateClient("MotelIntegracion");
        }

        // GET: /Clientes
        public async Task<IActionResult> Index()
        {
            var list = new List<Cliente>();
            try
            {
                var resp = await _api.GetAsync("Clientes");
                if (resp.IsSuccessStatusCode)
                    list = await resp.Content.ReadFromJsonAsync<List<Cliente>>();
                else
                    TempData["Error"] = $"API error: {resp.StatusCode}";
            }
            catch (Exception ex)
            {
                TempData["Error"] = $"Error de conexión: {ex.Message}";
            }
            return View(list);
        }

        // GET: /Clientes/Details/5
        public async Task<IActionResult> Details(int id)
        {
            try
            {
                var resp = await _api.GetAsync($"Clientes/{id}");
                if (!resp.IsSuccessStatusCode) return NotFound();
                var cliente = await resp.Content.ReadFromJsonAsync<Cliente>();
                return View(cliente);
            }
            catch
            {
                return NotFound();
            }
        }

        // GET: /Clientes/Create
        public IActionResult Create() => View();

        // POST: /Clientes/Create
        [HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Cliente model)
        {
            if (!ModelState.IsValid)
                return View(model);

            var resp = await _api.PostAsJsonAsync("Clientes", model);
            if (resp.IsSuccessStatusCode)
                return RedirectToAction(nameof(Index));

            ModelState.AddModelError("", "Error al crear cliente");
            return View(model);
        }

        // GET: /Clientes/Profile  -> redirige a Edit si está logueado
        public IActionResult Profile()
        {
            var id = HttpContext.Session.GetInt32("ClienteId");
            if (!id.HasValue)
                return RedirectToAction("Login", "Auth");
            return RedirectToAction("Edit", new { id = id.Value });
        }

        // GET: /Clientes/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            try
            {
                var resp = await _api.GetAsync($"Clientes/{id}");
                if (!resp.IsSuccessStatusCode) return NotFound();

                var cliente = await resp.Content.ReadFromJsonAsync<Cliente>();
                return View(cliente);
            }
            catch
            {
                return NotFound();
            }
        }

        // POST: /Clientes/Edit
        [HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Cliente model)
        {
            if (!ModelState.IsValid)
                return View(model);

            var resp = await _api.PutAsJsonAsync(
                $"Clientes/{model.NumCliente}",
                model);

            if (!resp.IsSuccessStatusCode)
            {
                TempData["Error"] = "No se pudo actualizar perfil";
                return View(model);
            }

            TempData["Success"] = "Perfil actualizado";
            return RedirectToAction(nameof(Edit), new { id = model.NumCliente });
        }

        // POST: /Clientes/DeleteAccount
        [HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteAccount()
        {
            var id = HttpContext.Session.GetInt32("ClienteId");
            if (!id.HasValue)
                return RedirectToAction("Login", "Auth");

            var resp = await _api.DeleteAsync($"Clientes/{id.Value}");
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