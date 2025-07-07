using Microsoft.AspNetCore.Mvc;
using Motel.Web.Models;
using System.Data;
using System.Data.SqlClient;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;

namespace Motel.Web.Controllers
{
    public class AuthController : Controller
    {
        private readonly string _connectionString;
        private readonly ILogger<AuthController> _logger;

        public AuthController(ILogger<AuthController> logger, IConfiguration configuration)
        {
            _logger = logger;
            _connectionString = configuration.GetConnectionString("ConexionMotel");
        }

        [HttpGet]
        public IActionResult Login(string returnUrl = null)
        {
            if (HttpContext.Session.GetInt32("ClienteId").HasValue)
            {
                return RedirectToAction("Index", "Home");
            }

            ViewData["ReturnUrl"] = returnUrl;

            // Mostrar mensajes de error si existen
            if (TempData["Error"] != null)
            {
                ModelState.AddModelError(string.Empty, TempData["Error"].ToString());
            }

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(Cliente clienteLogin, string returnUrl = null)
        {
            try
            {
                if (HttpContext.Session.GetInt32("ClienteId").HasValue)
                {
                    return RedirectToAction("Index", "Home");
                }

                // Validación manual adicional
                if (string.IsNullOrEmpty(clienteLogin.CorreoCliente))
                {
                    TempData["Error"] = "El correo es requerido";
                    return RedirectToAction("Login");
                }

                if (string.IsNullOrEmpty(clienteLogin.TelefonoCliente))
                {
                    TempData["Error"] = "El teléfono es requerido";
                    return RedirectToAction("Login");
                }

                Cliente cliente = null;

                using (SqlConnection connection = new SqlConnection(_connectionString))
                {
                    await connection.OpenAsync();

                    // Consulta mejorada con parámetros
                    string query = @"SELECT NumCliente, NombreCliente, ApellidoCliente, 
                            CorreoCliente, TelefonoCliente 
                            FROM Clientes 
                            WHERE CorreoCliente = @Correo";

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.Add("@Correo", SqlDbType.VarChar, 150).Value = clienteLogin.CorreoCliente;

                        using (var reader = await command.ExecuteReaderAsync())
                        {
                            if (await reader.ReadAsync())
                            {
                                cliente = new Cliente
                                {
                                    NumCliente = reader.GetInt32(0),
                                    NombreCliente = reader.GetString(1),
                                    ApellidoCliente = reader.GetString(2),
                                    CorreoCliente = reader.GetString(3),
                                    TelefonoCliente = reader.GetString(4)
                                };
                            }
                        }
                    }

                    if (cliente == null)
                    {
                        TempData["Error"] = "Credenciales inválidas";
                        return RedirectToAction("Login");
                    }

                    // Comparación exacta del teléfono
                    if (!cliente.TelefonoCliente.Equals(clienteLogin.TelefonoCliente))
                    {
                        TempData["Error"] = "Credenciales inválidas";
                        return RedirectToAction("Login");
                    }

                    // Configuración robusta de la sesión
                    HttpContext.Session.SetInt32("ClienteId", cliente.NumCliente);
                    HttpContext.Session.SetString("ClienteEmail", cliente.CorreoCliente);
                    HttpContext.Session.SetString("ClienteNombre", $"{cliente.NombreCliente} {cliente.ApellidoCliente}");

                    _logger.LogInformation($"Usuario {cliente.CorreoCliente} ha iniciado sesión");

                    // Redirección segura
                    if (!string.IsNullOrEmpty(returnUrl) && Url.IsLocalUrl(returnUrl))
                    {
                        return Redirect(returnUrl);
                    }
                    return RedirectToAction("Index", "Home");
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error durante el login");
                TempData["Error"] = "Error interno. Por favor intente nuevamente.";
                return RedirectToAction("Login");
            }
        }

        [HttpGet]
        public IActionResult Register()
        {
            if (HttpContext.Session.GetInt32("ClienteId").HasValue)
            {
                return RedirectToAction("Index", "Home");
            }

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(Cliente cliente)
        {
            if (HttpContext.Session.GetInt32("ClienteId").HasValue)
            {
                return RedirectToAction("Index", "Home");
            }

            if (!ModelState.IsValid)
            {
                return View(cliente);
            }

            try
            {
                bool emailExiste = false;
                using (SqlConnection connection = new SqlConnection(_connectionString))
                {
                    await connection.OpenAsync();

                    // Verificar si el correo existe
                    using (SqlCommand checkCmd = new SqlCommand(
                        "SELECT COUNT(1) FROM Clientes WHERE CorreoCliente = @Correo", connection))
                    {
                        checkCmd.Parameters.AddWithValue("@Correo", cliente.CorreoCliente);
                        emailExiste = (int)await checkCmd.ExecuteScalarAsync() > 0;
                    }

                    if (emailExiste)
                    {
                        ModelState.AddModelError("CorreoCliente", "Este correo ya está registrado");
                        return View(cliente);
                    }

                    // Registrar nuevo cliente
                    using (SqlCommand command = new SqlCommand("CrearCliente", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("@NombreCliente", cliente.NombreCliente);
                        command.Parameters.AddWithValue("@ApellidoCliente", cliente.ApellidoCliente);
                        command.Parameters.AddWithValue("@CorreoCliente", cliente.CorreoCliente);
                        command.Parameters.AddWithValue("@TelefonoCliente", cliente.TelefonoCliente);
                        command.Parameters.AddWithValue("@FechaNacimiento", cliente.FechaNacimiento);

                        await command.ExecuteNonQueryAsync();
                    }

                    // Obtener ID del nuevo cliente
                    using (SqlCommand getCmd = new SqlCommand(
                        "SELECT NumCliente FROM Clientes WHERE CorreoCliente = @Correo", connection))
                    {
                        getCmd.Parameters.AddWithValue("@Correo", cliente.CorreoCliente);
                        cliente.NumCliente = (int)await getCmd.ExecuteScalarAsync();
                    }
                }

                // Establecer sesión automáticamente después del registro
                HttpContext.Session.SetString("ClienteEmail", cliente.CorreoCliente);
                HttpContext.Session.SetInt32("ClienteId", cliente.NumCliente);
                HttpContext.Session.SetString("ClienteNombre", $"{cliente.NombreCliente} {cliente.ApellidoCliente}");

                _logger.LogInformation("Nuevo cliente registrado: {Correo}", cliente.CorreoCliente);

                return RedirectToAction("Index", "Home");
            }
            catch (SqlException sqlEx) when (sqlEx.Number == 2627)
            {
                ModelState.AddModelError("CorreoCliente", "Este correo ya está registrado");
                return View(cliente);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al registrar cliente");
                ModelState.AddModelError("", "Ocurrió un error al registrar. Intente nuevamente.");
                return View(cliente);
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Logout()
        {
            _logger.LogInformation("Cierre de sesión para {Correo}", HttpContext.Session.GetString("ClienteEmail"));
            HttpContext.Session.Clear();
            return RedirectToAction("Index", "Home");
        }
    }
}