using Microsoft.AspNetCore.Mvc;
using Motel.Integracion.ReservaHabitacion;

namespace Motel.Integracion.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ReservaHabitacionController : ControllerBase
    {
        private readonly ReservaHabitacionService _service;

        public ReservaHabitacionController(ReservaHabitacionService service)
        {
            _service = service;
        }

        [HttpGet("habitacionesPorReserva/{numReserva}")]
        public async Task<IActionResult> ObtenerPorReserva(int numReserva)
        {
            var resultado = await _service.ObtenerPorReservaAsync(numReserva);
            return resultado.Any() ? Ok(resultado) : NotFound("No hay habitaciones asociadas a esta reserva.");
        }

        [HttpGet("todasReservaHabitacion")]
        public async Task<IActionResult> ObtenerTodas()
        {
            var resultado = await _service.ObtenerTodasAsync();
            return resultado.Any() ? Ok(resultado) : NotFound("No hay registros en la tabla ReservaHabitacion.");
        }

        [HttpPost]
        public async Task<IActionResult> Crear([FromBody] ReservaHabitacionRequest request)
        {
            var creado = await _service.CrearAsync(request);
            return creado ? Ok("Habitación agregada a la reserva.") : StatusCode(500, "Error al crear.");
        }

        [HttpDelete("{numReserva}/{idHabitacion}")]
        public async Task<IActionResult> Eliminar(int numReserva, int idHabitacion)
        {
            var eliminado = await _service.EliminarAsync(numReserva, idHabitacion);
            return eliminado ? NoContent() : NotFound("No se encontró la habitación asociada a la reserva.");
        }

        [HttpGet("habitaciones")]
        public async Task<IActionResult> ObtenerHabitaciones()
        {
            var ids = await _service.ObtenerIdsHabitacionesAsync();
            return ids.Any() ? Ok(ids) : NotFound("No hay habitaciones registradas.");
        }

        [HttpGet("habitacionesDisponibles")]
        public async Task<IActionResult> ObtenerDisponibles()
        {
            var disponibles = await _service.ObtenerHabitacionesDisponiblesAsync();
            return disponibles.Any() ? Ok(disponibles) : NotFound("No hay habitaciones disponibles.");
        }
    }
}
