using Microsoft.AspNetCore.Mvc;
using Motel.Integracion.Servicios;

namespace Motel.Integracion.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ServiciosController : ControllerBase
    {
        private readonly ServicioService _servicioService;

        public ServiciosController(ServicioService servicioService)
        {
            _servicioService = servicioService;
        }

        [HttpPost]
        public async Task<IActionResult> Crear([FromBody] ServicioRequest request)
        {
            var creado = await _servicioService.CrearAsync(request);
            return creado ? StatusCode(201) : StatusCode(500, "Error al crear el servicio.");
        }

        [HttpGet]
        public async Task<IActionResult> ObtenerTodos()
        {
            var lista = await _servicioService.ObtenerTodosAsync();
            return lista.Any() ? Ok(lista) : NotFound("No hay servicios registrados.");
        }

        [HttpGet("{numServicio}")]
        public async Task<IActionResult> ObtenerPorId(int numServicio)
        {
            var servicio = await _servicioService.ObtenerPorIdAsync(numServicio);
            return servicio != null ? Ok(servicio) : NotFound();
        }

        [HttpPut("{numServicio}")]
        public async Task<IActionResult> Actualizar(int numServicio, [FromBody] ServicioRequest request)
        {
            var actualizado = await _servicioService.ActualizarAsync(numServicio, request);
            return actualizado ? NoContent() : NotFound("Servicio no encontrado.");
        }

        [HttpDelete("{numServicio}")]
        public async Task<IActionResult> Eliminar(int numServicio)
        {
            var eliminado = await _servicioService.EliminarAsync(numServicio);
            return eliminado ? NoContent() : NotFound("Servicio no encontrado.");
        }
    }
}
