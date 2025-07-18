﻿using Microsoft.AspNetCore.Mvc;
using System.Data.SqlClient;
using System.Threading.Tasks;

namespace proyecto_motel.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PagosController : ControllerBase
    {
        private readonly string _connectionString;

        public PagosController(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("ConexionMotel");
        }

        // POST: api/pagos
        [HttpPost]
        public async Task<IActionResult> RealizarPago([FromBody] Pagos pago)
        {
            if (pago == null)
            {
                return BadRequest("El pago no puede ser nulo.");
            }

            try
            {
                using (SqlConnection connection = new SqlConnection(_connectionString))
                {
                    await connection.OpenAsync();

                    using (SqlCommand command = new SqlCommand("RealizarPago", connection))
                    {
                        command.CommandType = System.Data.CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("@NumReserva", pago.NumReserva);
                        command.Parameters.AddWithValue("@MontoPago", pago.MontoPago);
                        command.Parameters.AddWithValue("@MetodoPago", pago.MetodoPago);
                        command.Parameters.AddWithValue("@EstadoPago", pago.EstadoPago);

                        await command.ExecuteNonQueryAsync();
                    }
                }

                return CreatedAtAction(nameof(RealizarPago), new { id = pago.NumPago }, pago);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error en el servidor: {ex.Message}");
            }
        }

        // PUT api/pagos/cancelar/{numPago}
        [HttpPut("cancelar/{numPago}")]
        public async Task<IActionResult> CancelarPago(int numPago)
        {
            try
            {
                using (var connection = new SqlConnection(_connectionString))
                {
                    await connection.OpenAsync();
                    var sql = @"
                UPDATE Pagos
                SET EstadoPago = 'Cancelado'
                WHERE NumPago = @NumPago";
                    using (var cmd = new SqlCommand(sql, connection))
                    {
                        cmd.Parameters.AddWithValue("@NumPago", numPago);
                        int rows = await cmd.ExecuteNonQueryAsync();
                        if (rows > 0) return NoContent();
                        return NotFound($"No se encontró el pago {numPago}.");
                    }
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error al cancelar el pago: {ex.Message}");
            }
        }

        // GET: api/pagos/{numPago}
        [HttpGet("{numPago}")]
        public ActionResult<Pagos> GetByNumPago(int numPago)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(_connectionString))
                {
                    connection.Open();

                    using (SqlCommand command = new SqlCommand("ConsultarPago", connection))
                    {
                        command.CommandType = System.Data.CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("@NumPago", numPago);

                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                var pago = new Pagos
                                {
                                    NumPago = reader.GetInt32(0),
                                    NumReserva = reader.GetInt32(1),
                                    MontoPago = reader.GetDecimal(2),
                                    FechaPago = reader.GetDateTime(3),
                                    MetodoPago = reader.GetString(4),
                                    EstadoPago = reader.GetString(5),
                                    ComentarioPago = reader.IsDBNull(6) ? null : reader.GetString(6)
                                };

                                return Ok(pago);
                            }
                            else
                            {
                                return NotFound();
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error en el servidor: {ex.Message}");
            }
        }

        // GET: api/pagos/reserva/{numReserva}
        [HttpGet("reserva/{numReserva}")]
        public ActionResult<Pagos> GetByNumReserva(int numReserva)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(_connectionString))
                {
                    connection.Open();

                    using (SqlCommand command = new SqlCommand("ConsultarPago", connection))
                    {
                        command.CommandType = System.Data.CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("@NumReserva", numReserva);

                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                var pago = new Pagos
                                {
                                    NumPago = reader.GetInt32(0),
                                    NumReserva = reader.GetInt32(1),
                                    MontoPago = reader.GetDecimal(2),
                                    FechaPago = reader.GetDateTime(3),
                                    MetodoPago = reader.GetString(4),
                                    EstadoPago = reader.GetString(5),
                                    ComentarioPago = reader.IsDBNull(6) ? null : reader.GetString(6)
                                };

                                return Ok(pago);
                            }
                            else
                            {
                                return NotFound();
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error en el servidor: {ex.Message}");
            }
        }

        // PUT: api/pagos/{numPago}
        [HttpPut("{numPago}")]
        public IActionResult ActualizarEstadoPago(int numPago, [FromBody] Pagos pago)
        {
            if (pago == null)
            {
                return BadRequest("El pago no puede ser nulo.");
            }

            try
            {
                using (SqlConnection connection = new SqlConnection(_connectionString))
                {
                    connection.Open();

                    using (SqlCommand command = new SqlCommand("ActualizarEstadoPago", connection))
                    {
                        command.CommandType = System.Data.CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("@NumPago", numPago);
                        command.Parameters.AddWithValue("@EstadoPago", pago.EstadoPago);

                        int rowsAffected = command.ExecuteNonQuery();

                        if (rowsAffected > 0)
                        {
                            return NoContent();
                        }
                        else
                        {
                            return NotFound();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error en el servidor: {ex.Message}");
            }
        }

        // GET: api/pagos
        // Devuelve todos los pagos
        [HttpGet]
        public async Task<IActionResult> ObtenerTodosLosPagos()
        {
            try
            {
                var lista = new List<Pagos>();
                using (var connection = new SqlConnection(_connectionString))
                {
                    await connection.OpenAsync();

                    var query = @"
                SELECT NumPago, NumReserva, MontoPago, FechaPago, MetodoPago, EstadoPago, ComentarioPago
                FROM Pagos";
                    using (var cmd = new SqlCommand(query, connection))
                    using (var reader = await cmd.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            lista.Add(new Pagos
                            {
                                NumPago = reader.GetInt32(0),
                                NumReserva = reader.GetInt32(1),
                                MontoPago = reader.GetDecimal(2),
                                FechaPago = reader.GetDateTime(3),
                                MetodoPago = reader.GetString(4),
                                EstadoPago = reader.GetString(5),
                                ComentarioPago = reader.IsDBNull(6) ? null : reader.GetString(6)
                            });
                        }
                    }
                }

                if (lista.Count == 0) return NotFound("No hay pagos registrados.");
                return Ok(lista);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error en el servidor: {ex.Message}");
            }
        }

        // PUT: api/pagos/modificar/5
        // Modifica todos los campos de un pago existente
        [HttpPut("modificar/{numPago}")]
        public async Task<IActionResult> ModificarPago(int numPago, [FromBody] Pagos pago)
        {
            if (pago == null || pago.NumPago != numPago)
                return BadRequest("Payload inválido.");

            try
            {
                using (var connection = new SqlConnection(_connectionString))
                {
                    await connection.OpenAsync();

                    var query = @"
                UPDATE Pagos
                SET NumReserva     = @NumReserva,
                    MontoPago      = @MontoPago,
                    FechaPago      = @FechaPago,
                    MetodoPago     = @MetodoPago,
                    EstadoPago     = @EstadoPago,
                    ComentarioPago = @ComentarioPago
                WHERE NumPago = @NumPago";

                    using (var cmd = new SqlCommand(query, connection))
                    {
                        cmd.Parameters.AddWithValue("@NumPago", pago.NumPago);
                        cmd.Parameters.AddWithValue("@NumReserva", pago.NumReserva);
                        cmd.Parameters.AddWithValue("@MontoPago", pago.MontoPago);
                        cmd.Parameters.AddWithValue("@FechaPago", pago.FechaPago);
                        cmd.Parameters.AddWithValue("@MetodoPago", pago.MetodoPago);
                        cmd.Parameters.AddWithValue("@EstadoPago", pago.EstadoPago);
                        cmd.Parameters.AddWithValue("@ComentarioPago", (object)pago.ComentarioPago ?? DBNull.Value);

                        int filas = await cmd.ExecuteNonQueryAsync();
                        if (filas > 0) return NoContent();
                        return NotFound("Pago no encontrado.");
                    }
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error en el servidor: {ex.Message}");
            }
        }

    }
}
