using Microsoft.AspNetCore.Mvc;
using Motel.Integracion.Clientes;

namespace Motel.Integracion.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ClientesController : ControllerBase
    {
        private readonly ClienteService _clienteService;

        public ClientesController(ClienteService clienteService)
        {
            _clienteService = clienteService;
        }

        [HttpPost]
        public async Task<IActionResult> Crear([FromBody] ClienteRequest request)
        {
            var resultado = await _clienteService.CrearClienteAsync(request);
            return resultado ? Ok(true) : BadRequest("No se pudo crear el cliente.");
        }

        [HttpGet]
        public async Task<ActionResult<List<ClienteResponse>>> ObtenerTodos()
        {
            var data = await _clienteService.ObtenerTodosAsync();
            return Ok(data);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ClienteResponse>> ObtenerPorId(int id)
        {
            var cliente = await _clienteService.ObtenerPorIdAsync(id);
            return cliente != null ? Ok(cliente) : NotFound();
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Actualizar(int id, [FromBody] ClienteRequest request)
        {
            var resultado = await _clienteService.ActualizarClienteAsync(id, request);
            return resultado ? NoContent() : NotFound();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Eliminar(int id)
        {
            var resultado = await _clienteService.EliminarClienteAsync(id);
            return resultado ? NoContent() : NotFound();
        }

        [HttpGet("buscar")]
        public async Task<ActionResult<ClienteResponse>> BuscarPorCorreo([FromQuery] string email)
        {
            var cliente = await _clienteService.BuscarPorCorreoAsync(email);
            return cliente != null ? Ok(cliente) : NotFound();
        }

        [HttpGet("ids")]
        public async Task<ActionResult<List<int>>> ObtenerIds()
        {
            var data = await _clienteService.ObtenerIdsAsync();
            return Ok(data);
        }

        [HttpGet("correos")]
        public async Task<ActionResult<List<string>>> ObtenerCorreos()
        {
            var data = await _clienteService.ObtenerCorreosAsync();
            return Ok(data);
        }

        [HttpGet("obtenerIdPorCorreo/{correo}")]
        public async Task<ActionResult<int>> ObtenerIdPorCorreo(string correo)
        {
            var id = await _clienteService.ObtenerIdPorCorreoAsync(correo);
            return id != null ? Ok(id) : NotFound();
        }
    }
}