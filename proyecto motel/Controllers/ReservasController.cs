using Microsoft.AspNetCore.Mvc;
using System.Data;
using System.Data.SqlClient;
using System.Threading.Tasks;

namespace proyecto_motel.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReservasController : ControllerBase
    {
        private readonly string _connectionString;

        public ReservasController(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("ConexionMotel");
        }

        // POST: api/Reservas
        [HttpPost]
        public async Task<IActionResult> CrearReserva([FromBody] Reservas reserva)
        {
            if (reserva == null)
                return BadRequest("La reserva no puede ser nula.");

            try
            {
                int nuevaId;
                using (var conn = new SqlConnection(_connectionString))
                {
                    await conn.OpenAsync();

                    // Hacemos insert + OUTPUT para capturar el identity
                    const string sql = @"
                INSERT INTO dbo.Reservas
                    (NumCliente, FechaReserva, FechaEntrada, FechaSalida,
                     EstadoReserva, TotalReserva, ComentarioReserva)
                OUTPUT INSERTED.NumReserva
                VALUES
                    (@NumCliente, GETDATE(), @FechaEntrada, @FechaSalida,
                     @EstadoReserva, @TotalReserva, @ComentarioReserva);";

                    using (var cmd = new SqlCommand(sql, conn))
                    {
                        cmd.Parameters.AddWithValue("@NumCliente", reserva.NumCliente);
                        cmd.Parameters.AddWithValue("@FechaEntrada", reserva.FechaEntrada);
                        cmd.Parameters.AddWithValue("@FechaSalida", reserva.FechaSalida);
                        cmd.Parameters.AddWithValue("@EstadoReserva", reserva.EstadoReserva);
                        cmd.Parameters.AddWithValue("@TotalReserva", reserva.TotalReserva);
                        cmd.Parameters.AddWithValue(
                            "@ComentarioReserva",
                            (object)reserva.ComentarioReserva ?? DBNull.Value
                        );

                        // ExecuteScalarAsync devolverá el NumReserva generado
                        nuevaId = (int)await cmd.ExecuteScalarAsync();
                    }
                }

                // Devolvemos CreatedAtAction apuntando al GET por ID
                reserva.NumReserva = nuevaId;
                return CreatedAtAction(
                    nameof(Get),
                    new { numReserva = nuevaId },
                    reserva
                );
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error en el servidor: {ex.Message}");
            }
        }



        // GET: api/reservas/{numReserva}
        [HttpGet("{numReserva}")]
        public ActionResult<Reservas> Get(int numReserva)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(_connectionString))
                {
                    connection.Open();

                    using (SqlCommand command = new SqlCommand("ConsultarReserva", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("@NumReserva", numReserva);

                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                var reserva = new Reservas
                                {
                                    NumReserva = reader.GetInt32(0),
                                    NumCliente = reader.GetInt32(1),
                                    FechaReserva = reader.GetDateTime(2),
                                    FechaEntrada = reader.GetDateTime(3),
                                    FechaSalida = reader.GetDateTime(4),
                                    EstadoReserva = reader.GetString(5),
                                    TotalReserva = reader.GetDecimal(6),
                                    ComentarioReserva = reader.IsDBNull(7) ? null : reader.GetString(7)
                                };

                                return Ok(reserva);
                            }

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
        // GET: api/Reservas/cliente/{clienteId}
        [HttpGet("cliente/{clienteId}")]
        public async Task<ActionResult<List<Reservas>>> GetReservasPorCliente(int clienteId)
        {
            var lista = new List<Reservas>();
            try
            {
                using var connection = new SqlConnection(_connectionString);
                await connection.OpenAsync();

                using var cmd = new SqlCommand("ConsultarReservasPorCliente", connection)
                {
                    CommandType = CommandType.StoredProcedure
                };
                cmd.Parameters.AddWithValue("@NumCliente", clienteId);

                using var reader = await cmd.ExecuteReaderAsync();
                while (await reader.ReadAsync())
                {
                    lista.Add(new Reservas
                    {
                        NumReserva = reader.GetInt32(0),
                        NumCliente = reader.GetInt32(1),
                        FechaReserva = reader.GetDateTime(2),
                        FechaEntrada = reader.GetDateTime(3),
                        FechaSalida = reader.GetDateTime(4),
                        EstadoReserva = reader.GetString(5),
                        TotalReserva = reader.GetDecimal(6),
                        ComentarioReserva = reader.IsDBNull(7) ? null : reader.GetString(7)
                    });
                }

                return Ok(lista);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error en el servidor: {ex.Message}");
            }
        }

        // PUT: api/reservas/{numReserva}
        [HttpPut("{numReserva}")]
        public async Task<IActionResult> Put(int numReserva, [FromBody] Reservas reserva)
        {
            if (reserva == null)
                return BadRequest("La reserva no puede ser nula.");

            try
            {
                using (SqlConnection connection = new SqlConnection(_connectionString))
                {
                    await connection.OpenAsync();

                    using (SqlCommand command = new SqlCommand("ActualizarReserva", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("@NumReserva", numReserva);
                        command.Parameters.AddWithValue("@FechaEntrada", reserva.FechaEntrada);
                        command.Parameters.AddWithValue("@FechaSalida", reserva.FechaSalida);
                        command.Parameters.AddWithValue("@EstadoReserva", reserva.EstadoReserva);
                        command.Parameters.AddWithValue("@TotalReserva", reserva.TotalReserva);
                        command.Parameters.AddWithValue("@ComentarioReserva", reserva.ComentarioReserva ?? (object)DBNull.Value);

                        int rowsAffected = await command.ExecuteNonQueryAsync();
                        return rowsAffected > 0 ? NoContent() : NotFound();
                    }
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error en el servidor: {ex.Message}");
            }
        }

        // PUT: api/reservas/cancelar/{numReserva}
        [HttpPut("cancelar/{numReserva}")]
        public IActionResult CancelarReserva(int numReserva)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(_connectionString))
                {
                    connection.Open();

                    using (SqlCommand command = new SqlCommand("CancelarReserva", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("@NumReserva", numReserva);

                        int rowsAffected = command.ExecuteNonQuery();
                        if (rowsAffected > 0)
                        {
                            using (SqlCommand liberarHabitaciones = new SqlCommand(@"
                                UPDATE Habitaciones 
                                SET EstadoHabitacion = 'Disponible' 
                                WHERE NumHabitacion IN (
                                    SELECT NumHabitacion 
                                    FROM ReservaHabitacion 
                                    WHERE NumReserva = @NumReserva)", connection))
                            {
                                liberarHabitaciones.Parameters.AddWithValue("@NumReserva", numReserva);
                                liberarHabitaciones.ExecuteNonQuery();
                            }

                            return NoContent();
                        }

                        return NotFound();
                    }
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error en el servidor: {ex.Message}");
            }
        }

        [HttpPost("{reservaId}/habitaciones/{habitacionId}")]
        public async Task<IActionResult> AsignarHabitacionAReserva(int reservaId, string habitacionId)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(_connectionString))
                {
                    await connection.OpenAsync();

                    // Actualizar estado de habitación
                    using (SqlCommand command = new SqlCommand("ActualizarEstadoHabitacion", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("@NumHabitacion", habitacionId);
                        command.Parameters.AddWithValue("@EstadoHabitacion", "Reservada");
                        await command.ExecuteNonQueryAsync();
                    }

                    // Asignar habitación a reserva
                    using (SqlCommand command = new SqlCommand("AsignarHabitacionAReserva", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("@NumReserva", reservaId);
                        command.Parameters.AddWithValue("@NumHabitacion", habitacionId);
                        await command.ExecuteNonQueryAsync();
                    }

                    return Ok();
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error en el servidor: {ex.Message}");
            }
        }
        [HttpGet("disponibles")]
        public async Task<IActionResult> GetHabitacionesDisponibles(
    [FromQuery] DateTime fechaInicio,
    [FromQuery] DateTime fechaFin)
        {
            try
            {
                using var conn = new SqlConnection(_connectionString);
                await conn.OpenAsync();

                // 1. Lee todas las habitaciones
                var todas = new List<Habitacion>();
                using (var cmd = new SqlCommand("SELECT * FROM Habitaciones", conn))
                using (var rdr = await cmd.ExecuteReaderAsync())
                {
                    while (await rdr.ReadAsync())
                    {
                        todas.Add(new Habitacion
                        {
                            IdHabitacion = rdr.GetInt32(0),
                            NumHabitacion = rdr.GetString(1),
                            TipoHabitacion = rdr.GetString(2),
                            PrecioHabitacion = rdr.GetDecimal(3),
                            EstadoHabitacion = rdr.GetString(4),
                            CapacidadHabitacion = rdr.GetInt32(5)
                        });
                    }
                }

                // 2. Encuentra habitaciones ocupadas en el rango solapado
                var ocupadas = new HashSet<int>();
                const string sqlOcupadas = @"
            SELECT DISTINCT rh.IdHabitacion
            FROM Reservas r
            JOIN ReservaHabitacion rh 
              ON r.NumReserva = rh.NumReserva
            WHERE r.EstadoReserva != 'Cancelada'
              AND @fInicio < r.FechaSalida
              AND @fFin    > r.FechaEntrada";
                using (var cmd = new SqlCommand(sqlOcupadas, conn))
                {
                    cmd.Parameters.AddWithValue("@fInicio", fechaInicio);
                    cmd.Parameters.AddWithValue("@fFin", fechaFin);
                    using var rdr = await cmd.ExecuteReaderAsync();
                    while (await rdr.ReadAsync())
                        ocupadas.Add(rdr.GetInt32(0));
                }

                // 3. Devuelve solo las libres
                var libres = todas.Where(h => !ocupadas.Contains(h.IdHabitacion)).ToList();
                return Ok(libres);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error en servidor: {ex.Message}");
            }
        }

    }
}
