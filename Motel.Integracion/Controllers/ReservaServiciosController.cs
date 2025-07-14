using Microsoft.AspNetCore.Mvc;
using Motel.Integracion.ReservaServicios;
using Motel.Integracion.Servicios;


namespace Motel.Integracion.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ReservaServiciosController : ControllerBase
    {
        private readonly ReservaServiciosService _service;

        public ReservaServiciosController(ReservaServiciosService service)
        {
            _service = service;
        }

        [HttpGet("todasReservaServicios")]
        public async Task<IActionResult> ObtenerTodas()
        {
            var lista = await _service.ObtenerTodosAsync();
            return lista.Any() ? Ok(lista) : NotFound("No se encontraron servicios de reserva.");
        }

        [HttpGet("servicio/{nombreServicio}")]
        public async Task<IActionResult> ObtenerPorNombre(string nombreServicio)
        {
            var servicio = await _service.ObtenerServicioPorNombreAsync(nombreServicio);
            return servicio != null ? Ok(servicio) : NotFound("Servicio no encontrado.");
        }

        [HttpPut("servicio/{nombreServicio}")]
        public async Task<IActionResult> ActualizarPorNombre(string nombreServicio, [FromBody] ServicioRequest request)
        {
            var actualizado = await _service.ActualizarServicioPorNombreAsync(nombreServicio, request);
            return actualizado ? NoContent() : NotFound("No se pudo actualizar el servicio.");
        }

        [HttpGet("serviciosPorReserva/{numReserva}")]
        public async Task<IActionResult> ObtenerServiciosPorReserva(int numReserva)
        {
            var lista = await _service.ObtenerServiciosPorReservaAsync(numReserva);
            return lista.Any() ? Ok(lista) : NotFound("No se encontraron servicios para esta reserva.");
        }

        [HttpDelete("{numReserva}/{numServicio}")]
        public async Task<IActionResult> Eliminar(int numReserva, int numServicio)
        {
            var eliminado = await _service.EliminarAsync(numReserva, numServicio);
            return eliminado ? NoContent() : NotFound("No se encontró el servicio asociado a la reserva.");
        }

        [HttpPost]
        public async Task<IActionResult> Crear([FromBody] ReservaServicioRequest request)
        {
            var creado = await _service.CrearAsync(request);
            return creado ? StatusCode(201) : StatusCode(500, "Error al registrar el servicio en la reserva.");
        }

        [HttpGet("ids/reservas")]
        public async Task<IActionResult> ObtenerIdsReservas()
        {
            var ids = await _service.ObtenerIdsReservasAsync();
            return ids.Any() ? Ok(ids) : NotFound("No hay reservas registradas.");
        }

        [HttpGet("ids/servicios")]
        public async Task<IActionResult> ObtenerIdsServicios()
        {
            var ids = await _service.ObtenerIdsServiciosAsync();
            return ids.Any() ? Ok(ids) : NotFound("No hay servicios registrados.");
        }

        [HttpGet("nombres-precios-ids")]
        public async Task<IActionResult> ObtenerResumenServicios()
        {
            var resumen = await _service.ObtenerNombresPreciosIdsAsync();
            return resumen.Any() ? Ok(resumen) : NotFound("No se encontraron servicios.");
        }
    }
}
