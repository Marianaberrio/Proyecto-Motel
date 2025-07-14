using Microsoft.AspNetCore.Mvc;
using Motel.Integracion.Reservas;
using System.Linq;
using System.Threading.Tasks;

namespace Motel.Integracion.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ReservasController : ControllerBase
    {
        private readonly ReservaService _reservaService;

        public ReservasController(ReservaService reservaService)
        {
            _reservaService = reservaService;
        }

        /// <summary>
        /// POST api/Reservas
        /// Crea una reserva y devuelve 201 Created con el JSON de la reserva.
        /// </summary>
        [HttpPost]
        public async Task<IActionResult> Crear([FromBody] ReservaRequest request)
        {
            var creada = await _reservaService.CrearAsync(request);
            if (creada == null)
                return StatusCode(500, "Error al registrar la reserva.");

            // CreatedAtAction genera un Location header y devuelve el objeto en el cuerpo
            return CreatedAtAction(
                nameof(ObtenerPorId),
                new { numReserva = creada.NumReserva },
                creada
            );
        }

        [HttpGet]
        public async Task<IActionResult> ObtenerTodas()
        {
            var lista = await _reservaService.ObtenerTodasAsync();
            return lista.Any()
                ? Ok(lista)
                : NotFound("No hay reservas registradas.");
        }

        [HttpGet("{numReserva}")]
        public async Task<IActionResult> ObtenerPorId(int numReserva)
        {
            var reserva = await _reservaService.ObtenerPorIdAsync(numReserva);
            return reserva != null
                ? Ok(reserva)
                : NotFound();
        }

        [HttpGet("cliente/{clienteId}")]
        public async Task<IActionResult> ObtenerPorCliente(int clienteId)
        {
            var lista = await _reservaService.ObtenerPorClienteAsync(clienteId);
            return lista.Any()
                ? Ok(lista)
                : NotFound("No se encontraron reservas para este cliente.");
        }

        [HttpGet("pagas")]
        public async Task<IActionResult> ObtenerPagas()
        {
            var lista = await _reservaService.ObtenerPagasAsync();
            return lista.Any()
                ? Ok(lista)
                : NotFound("No hay reservas pagadas.");
        }

        [HttpGet("ids")]
        public async Task<IActionResult> ObtenerIds()
        {
            var ids = await _reservaService.ObtenerIdsAsync();
            return ids.Any()
                ? Ok(ids)
                : NotFound("No hay reservas registradas.");
        }

        [HttpGet("pendientes")]
        public async Task<IActionResult> ObtenerPendientes()
        {
            var pendientes = await _reservaService.ObtenerPendientesAsync();
            return pendientes.Any()
                ? Ok(pendientes)
                : NotFound("No hay reservas pendientes.");
        }

        [HttpPut("cambiarEstado/{numReserva}")]
        public async Task<IActionResult> CambiarEstado(int numReserva, [FromBody] string nuevoEstado)
        {
            var ok = await _reservaService.CambiarEstadoAsync(numReserva, nuevoEstado);
            return ok ? NoContent() : NotFound();
        }

        [HttpPut("cancelar/{numReserva}")]
        public async Task<IActionResult> Cancelar(int numReserva)
        {
            var ok = await _reservaService.CancelarAsync(numReserva);
            return ok ? NoContent() : NotFound();
        }

        [HttpPut("{numReserva}")]
        public async Task<IActionResult> Actualizar(int numReserva, [FromBody] ReservaRequest request)
        {
            var ok = await _reservaService.ActualizarAsync(numReserva, request);
            return ok ? NoContent() : NotFound("Reserva no encontrada.");
        }

        [HttpGet("reservasActivas")]
        public async Task<IActionResult> ObtenerActivas()
        {
            var activas = await _reservaService.ObtenerActivasAsync();
            return activas.Any()
                ? Ok(activas)
                : NotFound("No hay reservas activas.");
        }
    }
}