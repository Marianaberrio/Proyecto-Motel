using Microsoft.AspNetCore.Mvc;
using Motel.Integracion;
using Motel.Integracion.Requests;
using Motel.Integracion.Usuarios;

namespace Motel.Integracion.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UsuariosController : ControllerBase
    {
        private readonly UsuarioService _usuarioService;

        public UsuariosController(UsuarioService usuarioService)
        {
            _usuarioService = usuarioService;
        }

        [HttpPost("agregar")]
        public async Task<IActionResult> Agregar([FromBody] UsuarioRequest request)
        {
            var creado = await _usuarioService.CrearAsync(request);
            return creado ? Created("", null) : StatusCode(500, "Error al crear el usuario.");
        }

        [HttpGet]
        public async Task<IActionResult> ObtenerTodos()
        {
            var lista = await _usuarioService.ObtenerTodosAsync();
            return lista.Any() ? Ok(lista) : NotFound("No se encontraron usuarios.");
        }

        [HttpGet("buscarPorId/{id}")]
        public async Task<IActionResult> BuscarPorId(int id)
        {
            var usuario = await _usuarioService.BuscarPorIdAsync(id);
            return usuario is not null ? Ok(usuario) : NotFound("Usuario no encontrado.");
        }

        [HttpGet("buscarPorNombre/{usuario}")]
        public async Task<IActionResult> BuscarPorNombre(string usuario)
        {
            var resultado = await _usuarioService.BuscarPorNombreAsync(usuario);
            return resultado is not null ? Ok(resultado) : NotFound("Usuario no encontrado.");
        }

        [HttpPut("modificarPorId/{id}")]
        public async Task<IActionResult> ModificarPorId(int id, [FromBody] UsuarioRequest request)
        {
            var actualizado = await _usuarioService.ModificarPorIdAsync(id, request);
            return actualizado ? NoContent() : NotFound("No se pudo actualizar el usuario.");
        }

        [HttpPut("modificarPorNombre/{usuario}")]
        public async Task<IActionResult> ModificarPorNombre(string usuario, [FromBody] UsuarioRequest request)
        {
            var actualizado = await _usuarioService.ModificarPorNombreAsync(usuario, request);
            return actualizado ? NoContent() : NotFound("No se pudo actualizar el usuario.");
        }

        [HttpDelete("eliminarPorId/{id}")]
        public async Task<IActionResult> EliminarPorId(int id)
        {
            var eliminado = await _usuarioService.EliminarPorIdAsync(id);
            return eliminado ? NoContent() : NotFound("No se pudo eliminar el usuario.");
        }

        [HttpDelete("eliminarPorNombre/{usuario}")]
        public async Task<IActionResult> EliminarPorNombre(string usuario)
        {
            var eliminado = await _usuarioService.EliminarPorNombreAsync(usuario);
            return eliminado ? NoContent() : NotFound("No se pudo eliminar el usuario.");
        }
    }
}
