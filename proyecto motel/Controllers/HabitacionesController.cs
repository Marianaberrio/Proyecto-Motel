using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Threading.Tasks;

namespace proyecto_motel.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HabitacionesController : ControllerBase
    {
        private readonly string _connectionString;

        public HabitacionesController(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("ConexionMotel");
        }

        // POST: api/habitaciones
        [HttpPost]
        public async Task<IActionResult> CrearHabitacion([FromBody] Habitacion habitacion)
        {
            if (habitacion == null)
            {
                return BadRequest("La habitación no puede ser nula.");
            }

            try
            {
                using (SqlConnection connection = new SqlConnection(_connectionString))
                {
                    await connection.OpenAsync();

                    using (SqlCommand command = new SqlCommand("CrearHabitacion", connection))
                    {
                        command.CommandType = System.Data.CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("@NumHabitacion", habitacion.NumHabitacion);
                        command.Parameters.AddWithValue("@TipoHabitacion", habitacion.TipoHabitacion);
                        command.Parameters.AddWithValue("@CapacidadHabitacion", habitacion.CapacidadHabitacion);
                        command.Parameters.AddWithValue("@PrecioHabitacion", habitacion.PrecioHabitacion);
                        command.Parameters.AddWithValue("@EstadoHabitacion", habitacion.EstadoHabitacion);

                        await command.ExecuteNonQueryAsync();
                    }
                }

                return CreatedAtAction(nameof(CrearHabitacion), new { id = habitacion.NumHabitacion }, habitacion);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error en el servidor: {ex.Message}");
            }
        }

        // PATCH api/Habitaciones/cambiarEstado/5
        [HttpPut("cambiarEstado/{idHabitacion}")]
        public async Task<IActionResult> CambiarEstadoHabitacion(int idHabitacion, [FromBody] string nuevoEstado)
        {
            try
            {
                using (var connection = new SqlConnection(_connectionString))
                {
                    await connection.OpenAsync();

                    var query = @"
                UPDATE Habitaciones
                SET EstadoHabitacion = @Estado
                WHERE IdHabitacion = @IdHabitacion
            ";
                    using (var command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@Estado", nuevoEstado);
                        command.Parameters.AddWithValue("@IdHabitacion", idHabitacion);

                        int rows = await command.ExecuteNonQueryAsync();
                        if (rows > 0) return NoContent();
                        return NotFound("Habitación no encontrada.");
                    }
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error al cambiar el estado: {ex.Message}");
            }
        }

        // GET: api/habitaciones/{numHabitacion}
        [HttpGet("{numHabitacion}")]
        public ActionResult<Habitacion> Get(string numHabitacion)
        {
            try
            {
                // Verificación de lo que recibimos en el parámetro
                Console.WriteLine("Recibido numHabitacion: " + numHabitacion);

                using (SqlConnection connection = new SqlConnection(_connectionString))
                {
                    connection.Open();

                    // Usamos una consulta directa para obtener la habitación por número
                    string sql = @"SELECT IdHabitacion, NumHabitacion, TipoHabitacion, 
                                  PrecioHabitacion, EstadoHabitacion, CapacidadHabitacion
                           FROM Habitaciones 
                           WHERE NumHabitacion = @NumHabitacion";

                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        command.Parameters.AddWithValue("@NumHabitacion", numHabitacion);

                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                var habitacion = new Habitacion
                                {
                                    IdHabitacion = reader.GetInt32(0),
                                    NumHabitacion = reader.GetString(1),
                                    TipoHabitacion = reader.GetString(2),
                                    PrecioHabitacion = reader.GetDecimal(3),
                                    EstadoHabitacion = reader.GetString(4),
                                    CapacidadHabitacion = reader.GetInt32(5)
                                };

                                return Ok(habitacion);  // Si la habitación existe, devolvemos la información
                            }
                            else
                            {
                                return NotFound();  // Si no se encuentra la habitación
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

        // GET: api/habitaciones/byid/{idHabitacion}
        [HttpGet("byid/{idHabitacion}")]
        public ActionResult<Habitacion> GetById(int idHabitacion)
        {
            try
            {
                // Verificación de lo que recibimos en el parámetro
                Console.WriteLine("Recibido idHabitacion: " + idHabitacion);

                using (SqlConnection connection = new SqlConnection(_connectionString))
                {
                    connection.Open();

                    // Usamos una consulta directa para obtener la habitación por ID
                    string sql = @"SELECT IdHabitacion, NumHabitacion, TipoHabitacion, 
                                  PrecioHabitacion, EstadoHabitacion, CapacidadHabitacion
                           FROM Habitaciones 
                           WHERE IdHabitacion = @IdHabitacion";

                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        command.Parameters.AddWithValue("@IdHabitacion", idHabitacion);

                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                var habitacion = new Habitacion
                                {
                                    IdHabitacion = reader.GetInt32(0),
                                    NumHabitacion = reader.GetString(1),
                                    TipoHabitacion = reader.GetString(2),
                                    PrecioHabitacion = reader.GetDecimal(3),
                                    EstadoHabitacion = reader.GetString(4),
                                    CapacidadHabitacion = reader.GetInt32(5)
                                };

                                return Ok(habitacion);  // Si la habitación existe, devolvemos la información
                            }
                            else
                            {
                                return NotFound();  // Si no se encuentra la habitación
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

        [HttpPut("{numHabitacion}")]
        public IActionResult Put(string numHabitacion, [FromBody] Habitacion habitacion)
        {
            if (habitacion == null)
            {
                return BadRequest("La habitación no puede ser nula.");
            }

            try
            {
                using (SqlConnection connection = new SqlConnection(_connectionString))
                {
                    connection.Open();

                    using (SqlCommand command = new SqlCommand("ActualizarHabitacion", connection))
                    {
                        command.CommandType = System.Data.CommandType.StoredProcedure;

                        // Pasamos IdHabitacion solo si se ha proporcionado
                        command.Parameters.AddWithValue("@IdHabitacion", habitacion.IdHabitacion != 0 ? habitacion.IdHabitacion : (object)DBNull.Value);
                        command.Parameters.AddWithValue("@NumHabitacion", numHabitacion);
                        command.Parameters.AddWithValue("@TipoHabitacion", habitacion.TipoHabitacion);
                        command.Parameters.AddWithValue("@PrecioHabitacion", habitacion.PrecioHabitacion);
                        command.Parameters.AddWithValue("@CapacidadHabitacion", habitacion.CapacidadHabitacion);
                        command.Parameters.AddWithValue("@EstadoHabitacion", habitacion.EstadoHabitacion);

                        int rowsAffected = command.ExecuteNonQuery();

                        if (rowsAffected > 0)
                        {
                            return NoContent(); // La habitación fue actualizada correctamente
                        }
                        else
                        {
                            return NotFound(); // Si no se encuentra la habitación con ese número o ID
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error en el servidor: {ex.Message}");
            }
        }



        // DELETE: api/habitaciones/{numHabitacion}
        [HttpDelete("{numHabitacion}")]
        public IActionResult Delete(string numHabitacion)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(_connectionString))
                {
                    connection.Open();

                    using (SqlCommand command = new SqlCommand("EliminarHabitacion", connection))
                    {
                        command.CommandType = System.Data.CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("@NumHabitacion", numHabitacion);

                        int rowsAffected = command.ExecuteNonQuery();

                        if (rowsAffected > 0)
                        {
                            return NoContent(); // La habitación fue eliminada
                        }
                        else
                        {
                            return NotFound(); // Si no se encuentra la habitación para eliminar
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error en el servidor: {ex.Message}");
            }
        }

        // GET: api/Habitaciones
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Habitacion>>> GetAll()
        {
            try
            {
                using var connection = new SqlConnection(_connectionString);
                await connection.OpenAsync();

                var lista = new List<Habitacion>();
                const string sql = @"
            SELECT IdHabitacion, NumHabitacion, TipoHabitacion,
                   PrecioHabitacion, EstadoHabitacion, CapacidadHabitacion
            FROM Habitaciones";

                using var cmd = new SqlCommand(sql, connection);
                using var reader = await cmd.ExecuteReaderAsync();
                while (await reader.ReadAsync())
                {
                    lista.Add(new Habitacion
                    {
                        IdHabitacion = reader.GetInt32(0),
                        NumHabitacion = reader.GetString(1),
                        TipoHabitacion = reader.GetString(2),
                        PrecioHabitacion = reader.GetDecimal(3),
                        EstadoHabitacion = reader.GetString(4),
                        CapacidadHabitacion = reader.GetInt32(5)
                    });
                }

                return Ok(lista);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error en servidor: {ex.Message}");
            }
        }

        // Obtener los números de las habitaciones disponibles
        [HttpGet("habitacionesDisponibles")]
        public async Task<IActionResult> ObtenerHabitacionesDisponibles()
        {
            try
            {
                using var connection = new SqlConnection(_connectionString);
                await connection.OpenAsync();

                var query = @"
            SELECT 
                IdHabitacion, 
                NumHabitacion, 
                TipoHabitacion, 
                PrecioHabitacion, 
                EstadoHabitacion, 
                CapacidadHabitacion
            FROM Habitaciones
            WHERE EstadoHabitacion = 'Disponible'
        ";

                using var command = new SqlCommand(query, connection);
                using var reader = await command.ExecuteReaderAsync();

                var lista = new List<Habitacion>();
                while (await reader.ReadAsync())
                {
                    lista.Add(new Habitacion
                    {
                        IdHabitacion = reader.GetInt32(0),
                        NumHabitacion = reader.GetString(1),
                        TipoHabitacion = reader.GetString(2),
                        PrecioHabitacion = reader.GetDecimal(3),
                        EstadoHabitacion = reader.GetString(4),
                        CapacidadHabitacion = reader.GetInt32(5),
                    });
                }

                return Ok(lista);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error al obtener habitaciones disponibles: {ex.Message}");
            }
        }


        // GET: api/habitaciones/disponibles/{tipoHabitacion}/{cantidad}
        [HttpGet("disponibles/{tipoHabitacion}/{cantidad}")]
        public async Task<IActionResult> ObtenerHabitacionesDisponibles(string tipoHabitacion, int cantidad)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(_connectionString))
                {
                    await connection.OpenAsync();

                    // Obtener las habitaciones disponibles por tipo y cantidad
                    var habitacionesDisponibles = new List<Habitacion>();

                    // Consulta SQL para obtener las habitaciones del tipo solicitado
                    string sql = "SELECT * FROM Habitaciones WHERE TipoHabitacion = @TipoHabitacion AND EstadoHabitacion = 'Disponible'";
                    using (var cmd = new SqlCommand(sql, connection))
                    {
                        cmd.Parameters.AddWithValue("@TipoHabitacion", tipoHabitacion);
                        using (var reader = await cmd.ExecuteReaderAsync())
                        {
                            while (await reader.ReadAsync() && habitacionesDisponibles.Count < cantidad)
                            {
                                habitacionesDisponibles.Add(new Habitacion
                                {
                                    IdHabitacion = reader.GetInt32(0),
                                    NumHabitacion = reader.GetString(1),
                                    TipoHabitacion = reader.GetString(2),
                                    PrecioHabitacion = reader.GetDecimal(3),
                                    EstadoHabitacion = reader.GetString(4),
                                    CapacidadHabitacion = reader.GetInt32(5)
                                });
                            }
                        }
                    }

                    // Verificar si hay suficientes habitaciones disponibles
                    if (habitacionesDisponibles.Count < cantidad)
                    {
                        return BadRequest($"No hay suficientes habitaciones de tipo {tipoHabitacion}. Solo hay {habitacionesDisponibles.Count} disponibles.");
                    }

                    return Ok(habitacionesDisponibles);
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error en el servidor: {ex.Message}");
            }
        }


        // PUT: api/habitaciones/ocupar
        [HttpPut("ocupar")]
        public async Task<IActionResult> MarcarHabitacionesComoOcupadas([FromBody] List<int> habitacionesIds)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(_connectionString))
                {
                    await connection.OpenAsync();

                    foreach (var idHabitacion in habitacionesIds)
                    {
                        string sql = "UPDATE Habitaciones SET EstadoHabitacion = 'Ocupada' WHERE IdHabitacion = @IdHabitacion";
                        using (SqlCommand command = new SqlCommand(sql, connection))
                        {
                            command.Parameters.AddWithValue("@IdHabitacion", idHabitacion);
                            await command.ExecuteNonQueryAsync();
                        }
                    }

                    return NoContent();  // Devuelve 204 si las habitaciones fueron actualizadas correctamente
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error en el servidor: {ex.Message}");
            }
        }


        // POST: api/habitaciones/asignar
        [HttpPost("asignar")]
        public async Task<IActionResult> AsignarHabitacionesAReserva([FromBody] AsignarHabitacionesRequest request)
        {
            if (request == null || request.Habitaciones == null || request.Habitaciones.Count == 0)
            {
                return BadRequest("No se proporcionaron habitaciones o la solicitud está vacía.");
            }

            try
            {
                using (SqlConnection connection = new SqlConnection(_connectionString))
                {
                    await connection.OpenAsync();

                    // Iniciar una transacción
                    using (var transaction = connection.BeginTransaction())
                    {
                        try
                        {
                            // Para cada habitación en la solicitud, cambiar su estado a "Ocupada" y asignarla a la reserva
                            foreach (var habitacion in request.Habitaciones)
                            {
                                // Cambiar el estado de la habitación a "Ocupada"
                                string updateHabitacionQuery = "UPDATE Habitaciones SET EstadoHabitacion = 'Ocupada' WHERE IdHabitacion = @IdHabitacion";
                                using (var cmd = new SqlCommand(updateHabitacionQuery, connection, transaction))
                                {
                                    cmd.Parameters.AddWithValue("@IdHabitacion", habitacion.IdHabitacion);
                                    await cmd.ExecuteNonQueryAsync();
                                }

                                // Insertar la relación entre la reserva y la habitación en la tabla ReservaHabitacion
                                string insertReservaHabitacionQuery = @"
                            INSERT INTO ReservaHabitacion (NumReserva, IdHabitacion, PrecioHabitacion)
                            VALUES (@NumReserva, @IdHabitacion, @PrecioHabitacion)";
                                using (var cmd = new SqlCommand(insertReservaHabitacionQuery, connection, transaction))
                                {
                                    cmd.Parameters.AddWithValue("@NumReserva", request.ReservaId);
                                    cmd.Parameters.AddWithValue("@IdHabitacion", habitacion.IdHabitacion);
                                    cmd.Parameters.AddWithValue("@PrecioHabitacion", habitacion.PrecioHabitacion);
                                    await cmd.ExecuteNonQueryAsync();
                                }
                            }

                            // Si todo fue bien, confirmamos la transacción
                            transaction.Commit();
                            return Ok("Habitaciones asignadas correctamente a la reserva.");
                        }
                        catch (Exception ex)
                        {
                            // Si hay un error, revertimos la transacción
                            transaction.Rollback();
                            return StatusCode(500, $"Error al asignar habitaciones: {ex.Message}");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error al procesar la solicitud: {ex.Message}");
            }
        }


    }

}
