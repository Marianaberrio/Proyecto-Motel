using Microsoft.AspNetCore.Mvc;
using Motel.Integracion.Pagos;

namespace Motel.Integracion.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PagosController : ControllerBase
    {
        private readonly PagoService _pagoService;

        public PagosController(PagoService pagoService)
        {
            _pagoService = pagoService;
        }

        [HttpPost]
        public async Task<IActionResult> Crear([FromBody] PagoRequest request)
        {
            var creado = await _pagoService.CrearPagoAsync(request);
            return creado ? StatusCode(201) : StatusCode(500, "Error al registrar el pago.");
        }

        [HttpGet]
        public async Task<IActionResult> ObtenerTodos()
        {
            var lista = await _pagoService.ObtenerPagosAsync();
            return lista.Any() ? Ok(lista) : NotFound("No hay pagos registrados.");
        }

        [HttpGet("{numPago}")]
        public async Task<IActionResult> ObtenerPorNumPago(int numPago)
        {
            var pago = await _pagoService.ObtenerPorNumPagoAsync(numPago);
            return pago != null ? Ok(pago) : NotFound();
        }

        [HttpGet("reserva/{numReserva}")]
        public async Task<IActionResult> ObtenerPorReserva(int numReserva)
        {
            var pago = await _pagoService.ObtenerPorReservaAsync(numReserva);
            return pago != null ? Ok(pago) : NotFound();
        }

        [HttpPut("cancelar/{numPago}")]
        public async Task<IActionResult> Cancelar(int numPago)
        {
            var ok = await _pagoService.CancelarPagoAsync(numPago);
            return ok ? NoContent() : NotFound();
        }

        [HttpPut("{numPago}")]
        public async Task<IActionResult> ActualizarEstado(int numPago, [FromBody] PagoRequest request)
        {
            var actualizado = await _pagoService.ActualizarEstadoPagoAsync(numPago, request);
            return actualizado ? NoContent() : NotFound();
        }

        [HttpPut("modificar/{numPago}")]
        public async Task<IActionResult> Modificar(int numPago, [FromBody] PagoRequest request)
        {
            var actualizado = await _pagoService.ModificarPagoAsync(numPago, request);
            return actualizado ? NoContent() : NotFound("Pago no encontrado.");
        }
    }
}
