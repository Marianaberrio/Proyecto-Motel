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

        // GET: api/habitaciones/{numHabitacion}
[HttpGet("{numHabitacion}")]
public ActionResult<Habitacion> Get(string numHabitacion)
{
    try
    {
        using (SqlConnection connection = new SqlConnection(_connectionString))
        {
            connection.Open();

            using (SqlCommand command = new SqlCommand("ConsultarHabitacion", connection))
            {
                command.CommandType = System.Data.CommandType.StoredProcedure;
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
                            CapacidadHabitacion = reader.GetInt32(5),
                        };

                        return Ok(habitacion); // Devuelve la habitación encontrada
                    }
                    else
                    {
                        return NotFound(); // Si no se encuentra la habitación
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


        // PUT: api/habitaciones/{numHabitacion}
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

                        // Cambiar el parámetro de @IdHabitacion a @NumHabitacion
                        command.Parameters.AddWithValue("@IdHabitacion", habitacion.IdHabitacion);
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
                            return NotFound(); // Si no se encuentra la habitación con ese número
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
        [HttpGet("disponibles")]
        public async Task<IActionResult> GetHabitacionesDisponibles([FromQuery] DateTime fechaInicio, [FromQuery] DateTime fechaFin)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(_connectionString))
                {
                    await connection.OpenAsync();

                    // 1. Obtener todas las habitaciones
                    var habitaciones = new List<Habitacion>();
                    using (SqlCommand command = new SqlCommand("SELECT * FROM Habitaciones WHERE EstadoHabitacion = 'Disponible'", connection))
                    {
                        using (SqlDataReader reader = await command.ExecuteReaderAsync())
                        {
                            while (await reader.ReadAsync())
                            {
                                habitaciones.Add(new Habitacion
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

                    // 2. Obtener reservas activas en ese rango de fechas
                    var reservasActivas = new List<Reservas>();
                    using (SqlCommand command = new SqlCommand(
                        @"SELECT r.* FROM Reservas r 
                  WHERE r.EstadoReserva != 'Cancelada'
                  AND ((@FechaInicio < r.FechaSalida) AND (@FechaFin > r.FechaEntrada))",
                        connection))
                    {
                        command.Parameters.AddWithValue("@FechaInicio", fechaInicio);
                        command.Parameters.AddWithValue("@FechaFin", fechaFin);

                        using (SqlDataReader reader = await command.ExecuteReaderAsync())
                        {
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
                        }
                    }

                    // 3. Obtener las habitaciones ocupadas en esas reservas
                    var habitacionesOcupadas = new List<int>();
                    if (reservasActivas.Any())
                    {
                        var reservaIds = string.Join(",", reservasActivas.Select(r => r.NumReserva));
                        using (SqlCommand command = new SqlCommand(
                            $"SELECT DISTINCT IdHabitacion FROM ReservaHabitacion WHERE NumReserva IN ({reservaIds})",
                            connection))
                        {
                            using (SqlDataReader reader = await command.ExecuteReaderAsync())
                            {
                                while (await reader.ReadAsync())
                                {
                                    habitacionesOcupadas.Add(reader.GetInt32(0));
                                }
                            }
                        }
                    }

                    // 4. Filtrar habitaciones disponibles
                    var habitacionesDisponibles = habitaciones
                        .Where(h => !habitacionesOcupadas.Contains(h.IdHabitacion))
                        .ToList();

                    return Ok(habitacionesDisponibles);
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error en el servidor: {ex.Message}");
            }
        }
    }

}
