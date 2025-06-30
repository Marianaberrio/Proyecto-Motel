using Microsoft.AspNetCore.Mvc;
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

        // POST: api/reservas
        [HttpPost]
        public async Task<IActionResult> CrearReserva([FromBody] Reservas reserva)
        {
            if (reserva == null)
            {
                return BadRequest("La reserva no puede ser nula.");
            }

            try
            {
                using (SqlConnection connection = new SqlConnection(_connectionString))
                {
                    await connection.OpenAsync();

                    // Paso 1: Crear la reserva
                    using (SqlCommand command = new SqlCommand("CrearReserva", connection))
                    {
                        command.CommandType = System.Data.CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("@NumCliente", reserva.NumCliente);
                        command.Parameters.AddWithValue("@FechaEntrada", reserva.FechaEntrada);
                        command.Parameters.AddWithValue("@FechaSalida", reserva.FechaSalida);
                        command.Parameters.AddWithValue("@EstadoReserva", reserva.EstadoReserva);
                        command.Parameters.AddWithValue("@TotalReserva", reserva.TotalReserva);
                        command.Parameters.AddWithValue("@ComentarioReserva", reserva.ComentarioReserva);

                        await command.ExecuteNonQueryAsync();
                    }
                }

                return CreatedAtAction(nameof(CrearReserva), new { id = reserva.NumReserva }, reserva);
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
                        command.CommandType = System.Data.CommandType.StoredProcedure;
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

        // PUT: api/reservas/{numReserva}
        [HttpPut("{numReserva}")]
        public async Task<IActionResult> Put(int numReserva, [FromBody] Reservas reserva)
        {
            if (reserva == null)
            {
                return BadRequest("La reserva no puede ser nula.");
            }

            try
            {
                using (SqlConnection connection = new SqlConnection(_connectionString))
                {
                    await connection.OpenAsync();

                    using (SqlCommand command = new SqlCommand("ActualizarReserva", connection))
                    {
                        command.CommandType = System.Data.CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("@NumReserva", numReserva);  // Número de la reserva que se va a actualizar
                        command.Parameters.AddWithValue("@FechaEntrada", reserva.FechaEntrada);  // Nueva fecha de entrada
                        command.Parameters.AddWithValue("@FechaSalida", reserva.FechaSalida);  // Nueva fecha de salida
                        command.Parameters.AddWithValue("@EstadoReserva", reserva.EstadoReserva);  // Nuevo estado de la reserva
                        command.Parameters.AddWithValue("@TotalReserva", reserva.TotalReserva);  // Nuevo total
                        command.Parameters.AddWithValue("@ComentarioReserva", reserva.ComentarioReserva);  // Nuevo comentario

                        int rowsAffected = await command.ExecuteNonQueryAsync();

                        if (rowsAffected > 0)
                        {
                            return NoContent();  // Si se actualiza correctamente
                        }
                        else
                        {
                            return NotFound();  // Si no se encuentra la reserva
                        }
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

                    // Primero, actualizamos el estado de la reserva a "Cancelada"
                    using (SqlCommand command = new SqlCommand("CancelarReserva", connection))
                    {
                        command.CommandType = System.Data.CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("@NumReserva", numReserva);

                        int rowsAffected = command.ExecuteNonQuery();

                        // Si la reserva fue cancelada correctamente
                        if (rowsAffected > 0)
                        {
                            // Liberamos la habitación (cambiamos el estado de la habitación a "Disponible")
                            using (SqlCommand liberateRoomCommand = new SqlCommand("UPDATE [dbo].[Habitaciones] SET EstadoHabitacion = 'Disponible' WHERE NumHabitacion IN (SELECT NumHabitacion FROM [dbo].[ReservaHabitacion] WHERE NumReserva = @NumReserva)", connection))
                            {
                                liberateRoomCommand.Parameters.AddWithValue("@NumReserva", numReserva);
                                liberateRoomCommand.ExecuteNonQuery();
                            }

                            return NoContent();  // Reserva cancelada correctamente
                        }
                        else
                        {
                            return NotFound();  // Si no se encuentra la reserva
                        }
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
