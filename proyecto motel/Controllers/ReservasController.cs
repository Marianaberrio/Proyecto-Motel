using Microsoft.AspNetCore.Mvc;
using System.Data;
using System.Data.SqlClient;
using System.Threading.Tasks;
using System.Collections.Generic;

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

                        nuevaId = (int)await cmd.ExecuteScalarAsync();
                    }
                }

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

        // GET: api/reservas/pagas
        [HttpGet("pagas")]
        public async Task<IActionResult> ObtenerReservasPagasAsync()
        {
            try
            {
                var lista = new List<Reservas>();
                using (var conn = new SqlConnection(_connectionString))
                {
                    await conn.OpenAsync();

                    // Ajusta la lista de columnas al esquema de tu tabla Reservas
                    const string sql = @"
                SELECT NumReserva, TotalReserva, FechaEntrada, FechaSalida, EstadoReserva
                FROM Reservas
                WHERE EstadoReserva = 'Paga'
            ";

                    using (var cmd = new SqlCommand(sql, conn))
                    using (var reader = await cmd.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            lista.Add(new Reservas
                            {
                                NumReserva = reader.GetInt32(0),
                                TotalReserva = reader.GetDecimal(1),
                                FechaEntrada = reader.GetDateTime(2),
                                FechaSalida = reader.GetDateTime(3),
                                EstadoReserva = reader.GetString(4)
                            });
                        }
                    }
                }

                if (lista.Count == 0)
                    return NotFound("No hay reservas en estado 'Paga'.");

                return Ok(lista);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error al obtener reservas pagas: {ex.Message}");
            }
        }

        // GET: api/reservas
        [HttpGet]
        public async Task<IActionResult> ObtenerTodasLasReservasAsync()
        {
            try
            {
                var lista = new List<Reservas>();

                // Conexión con la base de datos
                using (var conn = new SqlConnection(_connectionString))
                {
                    await conn.OpenAsync();

                    // Consulta SQL para obtener todas las reservas
                    const string sql = @"
                SELECT NumReserva, NumCliente, FechaReserva, FechaEntrada, FechaSalida, EstadoReserva, TotalReserva, ComentarioReserva
                FROM Reservas
            ";

                    using (var cmd = new SqlCommand(sql, conn))
                    using (var reader = await cmd.ExecuteReaderAsync())
                    {
                        // Leer los datos
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
                    }
                }

                // Si no se encuentran reservas
                if (lista.Count == 0)
                    return NotFound("No hay reservas disponibles.");

                // Retornar la lista de reservas
                return Ok(lista);
            }
            catch (Exception ex)
            {
                // En caso de error
                return StatusCode(500, $"Error al obtener las reservas: {ex.Message}");
            }
        }

        // GET: api/reservas/ids
        [HttpGet("ids")]
        public async Task<IActionResult> ObtenerIdsReservas()
        {
            try
            {
                using (var conn = new SqlConnection(_connectionString))
                {
                    await conn.OpenAsync();
                    var cmd = new SqlCommand("SELECT NumReserva FROM Reservas", conn);
                    var reader = await cmd.ExecuteReaderAsync();

                    var ids = new List<int>();
                    while (await reader.ReadAsync())
                    {
                        ids.Add(reader.GetInt32(0));
                    }

                    return Ok(ids);
                }
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

        // GET: api/reservas/cliente/{clienteId}
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

        // GET: api/reservas/pendientes
        [HttpGet("pendientes")]
        public async Task<IActionResult> ObtenerReservasPendientesAsync()
        {
            try
            {
                var reservas = new List<int>();
                using (var connection = new SqlConnection(_connectionString))
                {
                    await connection.OpenAsync();

                    // Selecciona solo las reservas cuyo estado no sea 'Paga' ni 'Cancelada'
                    const string sql = @"
                        SELECT NumReserva
                        FROM Reservas
                        WHERE EstadoReserva NOT IN ('Paga','Cancelada')
                    ";

                    using (var cmd = new SqlCommand(sql, connection))
                    using (var reader = await cmd.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            reservas.Add(reader.GetInt32(0));
                        }
                    }
                }

                if (reservas.Count == 0)
                    return NotFound("No hay reservas pendientes de pago o canceladas.");

                return Ok(reservas);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error al obtener reservas pendientes: {ex.Message}");
            }
        }

        // PUT: api/reservas/cambiarEstado/123
        [HttpPut("cambiarEstado/{numReserva}")]
        public async Task<IActionResult> CambiarEstadoReserva(int numReserva, [FromBody] string nuevoEstado)
        {
            if (string.IsNullOrWhiteSpace(nuevoEstado))
                return BadRequest("El nuevo estado no puede estar vacío.");

            try
            {
                using (var conn = new SqlConnection(_connectionString))
                {
                    await conn.OpenAsync();
                    const string sql = @"
                    UPDATE Reservas
                       SET EstadoReserva = @Estado
                     WHERE NumReserva     = @NumReserva";
                    using (var cmd = new SqlCommand(sql, conn))
                    {
                        cmd.Parameters.AddWithValue("@Estado", nuevoEstado);
                        cmd.Parameters.AddWithValue("@NumReserva", numReserva);

                        int rows = await cmd.ExecuteNonQueryAsync();
                        if (rows > 0) return NoContent();
                        return NotFound($"No se encontró la reserva {numReserva}.");
                    }
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error al actualizar estado de la reserva: {ex.Message}");
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

        // GET: api/reservas/activas
        [HttpGet("reservasActivas")]
        public async Task<ActionResult<List<Reservas>>> GetReservasActivas()
        {
            try
            {
                using var connection = new SqlConnection(_connectionString);
                await connection.OpenAsync();

                const string sql = @"
                    SELECT * 
                    FROM Reservas
                    WHERE EstadoReserva NOT IN ('Cancelada', 'Completada')";

                using var cmd = new SqlCommand(sql, connection);
                using var reader = await cmd.ExecuteReaderAsync();

                var reservasActivas = new List<Reservas>();
                while (await reader.ReadAsync())
                {
                    reservasActivas.Add(new Reservas
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

                return Ok(reservasActivas);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error en el servidor: {ex.Message}");
            }
        }
    }
}
