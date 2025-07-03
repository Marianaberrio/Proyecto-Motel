using Microsoft.AspNetCore.Mvc;
using Motel.Web.Models;
using Newtonsoft.Json;
using System.Net.Http.Json;

namespace Motel.Web.Controllers
{
    public class ReservasController : Controller
    {
        private readonly HttpClient _httpClient;
        private readonly ILogger<ReservasController> _logger;

        public ReservasController(HttpClient httpClient, ILogger<ReservasController> logger)
        {
            _httpClient = httpClient;
            _logger = logger;
            _httpClient.BaseAddress = new Uri("http://localhost:5264/api/");
        }

        public async Task<IActionResult> Index()
        {
            var clienteId = HttpContext.Session.GetInt32("ClienteId");
            if (clienteId == null) return RedirectToAction("Login", "Auth");

            try
            {
                var response = await _httpClient.GetAsync($"Reservas/cliente/{clienteId}");
                if (response.IsSuccessStatusCode)
                {
                    var reservas = await response.Content.ReadFromJsonAsync<List<Reserva>>();

                    // Obtener detalles completos de cada reserva
                    var reservasCompletas = new List<Reserva>();
                    foreach (var reserva in reservas)
                    {
                        // Obtener habitaciones
                        var habitacionesResponse = await _httpClient.GetAsync($"Reservas/{reserva.NumReserva}/habitaciones");
                        if (habitacionesResponse.IsSuccessStatusCode)
                        {
                            reserva.Habitaciones = await habitacionesResponse.Content.ReadFromJsonAsync<List<Habitacion>>();
                        }

                        // Obtener servicios
                        var serviciosResponse = await _httpClient.GetAsync($"Reservas/{reserva.NumReserva}/servicios");
                        if (serviciosResponse.IsSuccessStatusCode)
                        {
                            reserva.Servicios = await serviciosResponse.Content.ReadFromJsonAsync<List<Servicio>>();
                        }

                        reservasCompletas.Add(reserva);
                    }

                    return View(reservasCompletas);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al obtener reservas");
            }

            return View(new List<Reserva>());
        }

        [HttpGet]
        public IActionResult BuscarDisponibilidad()
        {
            if (string.IsNullOrEmpty(HttpContext.Session.GetString("ClienteEmail")))
                return RedirectToAction("Login", "Auth", new { returnUrl = "/Reservas/BuscarDisponibilidad" });

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> HabitacionesDisponibles(DateTime fechaEntrada, DateTime fechaSalida)
        {
            if (fechaEntrada >= fechaSalida)
            {
                TempData["Error"] = "La fecha de salida debe ser posterior a la de entrada";
                return RedirectToAction("BuscarDisponibilidad");
            }

            if (fechaEntrada < DateTime.Today)
            {
                TempData["Error"] = "No se pueden hacer reservas para fechas pasadas";
                return RedirectToAction("BuscarDisponibilidad");
            }

            var fechaInicioEncoded = Uri.EscapeDataString(fechaEntrada.ToString("o"));
            var fechaFinEncoded = Uri.EscapeDataString(fechaSalida.ToString("o"));

            var response = await _httpClient.GetAsync($"Habitaciones/disponibles?fechaInicio={fechaInicioEncoded}&fechaFin={fechaFinEncoded}");

            if (!response.IsSuccessStatusCode)
            {
                TempData["Error"] = "Error al consultar disponibilidad";
                return RedirectToAction("BuscarDisponibilidad");
            }

            var habitaciones = await response.Content.ReadFromJsonAsync<List<Habitacion>>();
            if (habitaciones == null || !habitaciones.Any())
            {
                TempData["Error"] = "No hay habitaciones disponibles para las fechas seleccionadas";
                return RedirectToAction("BuscarDisponibilidad");
            }

            TempData["FechaEntrada"] = fechaEntrada.ToString("o");
            TempData["FechaSalida"] = fechaSalida.ToString("o");
            TempData.Keep("FechaEntrada");
            TempData.Keep("FechaSalida");

            return View(habitaciones);
        }

        [HttpPost]
        public async Task<IActionResult> ElegirServicios(string habitacionIds)
        {
            if (string.IsNullOrEmpty(habitacionIds))
            {
                TempData["Error"] = "Debe seleccionar al menos una habitación";
                return RedirectToAction("BuscarDisponibilidad");
            }

            TempData["HabitacionIds"] = habitacionIds;
            TempData.Keep("FechaEntrada");
            TempData.Keep("FechaSalida");
            TempData.Keep("HabitacionIds");

            try
            {
                var response = await _httpClient.GetAsync("Servicios");
                if (response.IsSuccessStatusCode)
                {
                    var servicios = await response.Content.ReadFromJsonAsync<List<Servicio>>();
                    return View(servicios ?? new List<Servicio>());
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al obtener servicios");
            }

            return View(new List<Servicio>());
        }

        [HttpPost]
        public IActionResult DatosCliente(string habitacionIds, string servicioIds = null)
        {
            if (string.IsNullOrEmpty(habitacionIds))
            {
                TempData["Error"] = "Debe seleccionar al menos una habitación";
                return RedirectToAction("BuscarDisponibilidad");
            }

            TempData["ServicioIds"] = servicioIds;
            TempData["HabitacionIds"] = habitacionIds;

            TempData.Keep("HabitacionIds");
            TempData.Keep("ServicioIds");
            TempData.Keep("FechaEntrada");
            TempData.Keep("FechaSalida");

            var clienteEmail = HttpContext.Session.GetString("ClienteEmail");
            return View(new Cliente { CorreoCliente = clienteEmail });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ConfirmarReserva(
    Cliente cliente,
    string habitacionIds,
    string servicioIds,
    DateTime fechaEntrada,
    DateTime fechaSalida)
        {
            if (!ModelState.IsValid)
            {
                return View("DatosCliente", cliente);
            }

            try
            {
                // 1. Registrar o actualizar cliente
                var clienteId = HttpContext.Session.GetInt32("ClienteId");
                if (clienteId == null)
                {
                    var respCliente = await _httpClient.PostAsJsonAsync("Clientes", cliente);
                    if (!respCliente.IsSuccessStatusCode)
                    {
                        ModelState.AddModelError("", "Error al registrar cliente");
                        return View("DatosCliente", cliente);
                    }
                    var cli = await respCliente.Content.ReadFromJsonAsync<Cliente>();
                    clienteId = cli.NumCliente;
                    HttpContext.Session.SetInt32("ClienteId", clienteId.Value);
                    HttpContext.Session.SetString("ClienteEmail", cliente.CorreoCliente);
                }

                // 2. Procesar la reserva con los datos recibidos
                var (numReserva, error) = await ProcesarReserva(
                    clienteId.Value,
                    habitacionIds.Split(',', StringSplitOptions.RemoveEmptyEntries),
                    servicioIds?.Split(',', StringSplitOptions.RemoveEmptyEntries),
                    fechaEntrada,
                    fechaSalida);

                if (!string.IsNullOrEmpty(error))
                {
                    ModelState.AddModelError("", error);
                    return View("DatosCliente", cliente);
                }

                TempData["MensajeExito"] = "¡Reserva realizada con éxito!";
                return RedirectToAction("Confirmacion", new { id = numReserva });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al confirmar reserva");
                ModelState.AddModelError("", $"Error al procesar la reserva: {ex.Message}");
                return View("DatosCliente", cliente);
            }
        }


        private async Task<(int numReserva, string error)> ProcesarReserva(
    int clienteId,
    string[] habitacionIds,
    string[] servicioIds,
    DateTime fechaEntrada,
    DateTime fechaSalida)
        {
            try
            {
                // 1. Calcular horas de estadía
                var horasEstadia = (decimal)(fechaSalida - fechaEntrada).TotalHours;

                // 2. Obtener habitaciones por NumHabitacion
                var habitaciones = new List<Habitacion>();
                foreach (var numHab in habitacionIds)
                {
                    var r = await _httpClient.GetAsync($"Habitaciones/{numHab.Trim()}");
                    if (r.IsSuccessStatusCode)
                    {
                        var h = await r.Content.ReadFromJsonAsync<Habitacion>();
                        if (h != null) habitaciones.Add(h);
                    }
                    else
                    {
                        _logger.LogWarning($"No se encontró habitación: {numHab}");
                    }
                }
                if (!habitaciones.Any())
                    return (0, "No se encontraron las habitaciones seleccionadas");

                // 3. Obtener servicios (si hay)
                var servicios = new List<Servicio>();
                if (servicioIds != null)
                {
                    foreach (var sId in servicioIds)
                    {
                        var r = await _httpClient.GetAsync($"Servicios/{sId.Trim()}");
                        if (r.IsSuccessStatusCode)
                        {
                            var s = await r.Content.ReadFromJsonAsync<Servicio>();
                            if (s != null) servicios.Add(s);
                        }
                    }
                }

                // 4. Calcular total
                decimal total = habitaciones.Sum(h => h.PrecioHabitacion * horasEstadia)
                              + servicios.Sum(s => s.PrecioServicio);

                // 5. Crear reserva en la API
                var nuevaReserva = new
                {
                    NumCliente = clienteId,
                    FechaEntrada = fechaEntrada,
                    FechaSalida = fechaSalida,
                    EstadoReserva = "Confirmada",
                    TotalReserva = total,
                    FechaReserva = DateTime.Now,
                    ComentarioReserva = $"Estadía: {horasEstadia} horas"
                };
                var resp = await _httpClient.PostAsJsonAsync("Reservas", nuevaReserva);
                if (!resp.IsSuccessStatusCode)
                {
                    var err = await resp.Content.ReadAsStringAsync();
                    _logger.LogError($"Error al crear reserva: {err}");
                    return (0, "Error al crear la reserva");
                }
                var creada = await resp.Content.ReadFromJsonAsync<Reserva>();
                if (creada == null)
                    return (0, "Error al obtener la reserva creada");

                // 6. Asignar habitaciones y servicios
                foreach (var h in habitaciones)
                {
                    await _httpClient.PostAsync($"Reservas/{creada.NumReserva}/habitaciones/{h.NumHabitacion}", null);
                }
                foreach (var s in servicios)
                {
                    await _httpClient.PostAsync($"Reservas/{creada.NumReserva}/servicios/{s.NumServicio}", null);
                }

                return (creada.NumReserva, null);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error en ProcesarReserva");
                return (0, $"Error inesperado: {ex.Message}");
            }
        }



        public async Task<IActionResult> Confirmacion(int id)
        {
            var resp = await _httpClient.GetAsync($"Reservas/{id}");
            if (!resp.IsSuccessStatusCode) return NotFound();

            var reserva = await resp.Content.ReadFromJsonAsync<Reserva>();
            var cliId = HttpContext.Session.GetInt32("ClienteId");
            if (reserva == null || reserva.NumCliente != cliId)
                return RedirectToAction("Login", "Auth");

            // traer habitaciones
            var hResp = await _httpClient.GetAsync($"Reservas/{id}/habitaciones");
            if (hResp.IsSuccessStatusCode)
                reserva.Habitaciones = await hResp.Content.ReadFromJsonAsync<List<Habitacion>>();

            // traer servicios
            var sResp = await _httpClient.GetAsync($"Reservas/{id}/servicios");
            if (sResp.IsSuccessStatusCode)
                reserva.Servicios = await sResp.Content.ReadFromJsonAsync<List<Servicio>>();

            return View(reserva);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Cancelar(int id)
        {
            // Llama al endpoint de la API Core que marca Cancelada y libera habitaciones
            var response = await _httpClient.PutAsync($"Reservas/cancelar/{id}", null);
            if (response.IsSuccessStatusCode)
                TempData["Success"] = "Reserva cancelada correctamente.";
            else
                TempData["Error"] = "No se pudo cancelar la reserva.";
            return RedirectToAction("Index");
        }


    }
}
