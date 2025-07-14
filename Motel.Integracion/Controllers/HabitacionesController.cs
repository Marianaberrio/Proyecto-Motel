using Microsoft.AspNetCore.Mvc;
using Motel.Integracion.Habitaciones;
using System.Collections.Generic;
using System.Threading.Tasks;
using Motel.Integracion.Habitacion;

namespace Motel.Integracion.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class HabitacionesController : ControllerBase
    {
        private readonly HabitacionService _service;

        public HabitacionesController(HabitacionService service)
        {
            _service = service;
        }

        [HttpPost]
        public async Task<IActionResult> Crear([FromBody] HabitacionRequest request)
        {
            if (request == null)
                return BadRequest("Los datos son requeridos.");

            var resultado = await _service.CrearHabitacionAsync(request);
            if (resultado)
                return Ok("Habitación creada correctamente.");

            return StatusCode(500, "No se pudo crear la habitación.");
        }

        [HttpGet]
        public async Task<ActionResult<List<HabitacionResponse>>> ObtenerTodos()
        {
            var habitaciones = await _service.ObtenerTodosAsync();
            return Ok(habitaciones);
        }

        [HttpGet("{numHabitacion}")]
        public async Task<ActionResult<HabitacionResponse>> ObtenerPorNumero(string numHabitacion)
        {
            var habitacion = await _service.ObtenerPorNumeroAsync(numHabitacion);
            if (habitacion == null) return NotFound();
            return Ok(habitacion);
        }

        [HttpGet("byid/{idHabitacion}")]
        public async Task<ActionResult<HabitacionResponse>> ObtenerPorId(int idHabitacion)
        {
            var habitacion = await _service.ObtenerPorIdAsync(idHabitacion);
            if (habitacion == null) return NotFound();
            return Ok(habitacion);
        }

        [HttpPut("{numHabitacion}")]
        public async Task<IActionResult> Actualizar(string numHabitacion, [FromBody] HabitacionResponse habitacion)
        {
            var resultado = await _service.ActualizarHabitacionAsync(numHabitacion, habitacion);
            if (resultado) return NoContent();
            return NotFound();
        }

        [HttpDelete("{numHabitacion}")]
        public async Task<IActionResult> Eliminar(string numHabitacion)
        {
            var resultado = await _service.EliminarHabitacionAsync(numHabitacion);
            if (resultado) return NoContent();
            return NotFound();
        }


        [HttpPut("cambiarEstado/{idHabitacion}")]
        public async Task<IActionResult> CambiarEstado(int idHabitacion, [FromBody] string nuevoEstado)
        {
            var resultado = await _service.CambiarEstadoHabitacionAsync(idHabitacion, nuevoEstado);
            if (resultado) return NoContent();
            return NotFound();
        }

        [HttpGet("habitacionesDisponibles")]
        public async Task<ActionResult<List<HabitacionResponse>>> ObtenerDisponibles()
        {
            var habitaciones = await _service.ObtenerHabitacionesDisponiblesAsync();
            return Ok(habitaciones);
        }

        [HttpGet("disponibles/{tipoHabitacion}/{cantidad}")]
        public async Task<ActionResult<List<HabitacionResponse>>> ObtenerPorTipoCantidad(string tipoHabitacion, int cantidad)
        {
            try
            {
                var habitaciones = await _service.ObtenerDisponiblesPorTipoCantidadAsync(tipoHabitacion, cantidad);

                if (habitaciones.Count < cantidad)
                {
                    return BadRequest(new
                    {
                        mensaje = $"Solo hay {habitaciones.Count} habitaciones disponibles.",
                        disponibles = habitaciones
                    });
                }

                return Ok(habitaciones);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error al obtener habitaciones: {ex.Message}");
            }
        }

        [HttpPut("ocupar")]
        public async Task<IActionResult> OcuparHabitaciones([FromBody] List<int> ids)
        {
            var resultado = await _service.MarcarComoOcupadasAsync(ids);
            if (resultado) return NoContent();
            return StatusCode(500, "Error al ocupar habitaciones.");
        }

        [HttpPost("asignar")]
        public async Task<IActionResult> Asignar([FromBody] AsignarHabitacionesRequest request)
        {
            var resultado = await _service.AsignarHabitacionesAsync(request);
            if (resultado) return Ok("Habitaciones asignadas correctamente.");
            return StatusCode(500, "No se pudieron asignar las habitaciones.");
        }
    }
}
