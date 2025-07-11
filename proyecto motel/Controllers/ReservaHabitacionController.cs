using Microsoft.AspNetCore.Mvc;
using System.Data.SqlClient;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using proyecto_motel;

namespace proyecto_motel.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReservaHabitacionController : ControllerBase
    {
        private readonly string _connectionString;

        // Constructor donde se obtiene la cadena de conexión desde appsettings.json
        public ReservaHabitacionController(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("ConexionMotel");
        }

        // Obtener todas las habitaciones asociadas a una reserva
        [HttpGet("habitacionesPorReserva/{numReserva}")]
        public async Task<IActionResult> ObtenerHabitacionesPorReserva(int numReserva)
        {
            try
            {
                using (var connection = new SqlConnection(_connectionString))
                {
                    await connection.OpenAsync();

                    // Query para obtener todas las habitaciones asociadas a la reserva
                    var query = "SELECT NumReserva, IdHabitacion, PrecioHabitacion FROM ReservaHabitacion WHERE NumReserva = @NumReserva";
                    using (var command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@NumReserva", numReserva);

                        using (var reader = await command.ExecuteReaderAsync())
                        {
                            var reservasHabitaciones = new List<ReservaHabitacion>();

                            while (await reader.ReadAsync())
                            {
                                reservasHabitaciones.Add(new ReservaHabitacion
                                {
                                    NumReserva = reader.GetInt32(0),      // ID de la reserva
                                    IdHabitacion = reader.GetInt32(1),   // ID de la habitación
                                    PrecioHabitacion = reader.GetDecimal(2) // Precio de la habitación
                                });
                            }

                            // Si hay habitaciones asociadas, las devolvemos, si no, respondemos con "No encontrado"
                            if (reservasHabitaciones.Count > 0)
                                return Ok(reservasHabitaciones);
                            else
                                return NotFound("No se encontraron habitaciones asociadas a esta reserva.");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error al obtener las habitaciones asociadas a la reserva: {ex.Message}");
            }
        }

        // Obtener todos los datos de la tabla ReservaHabitacion
        [HttpGet("todasReservaHabitacion")]
        public async Task<IActionResult> ObtenerTodasLasReservaHabitacion()
        {
            try
            {
                using (var connection = new SqlConnection(_connectionString))
                {
                    await connection.OpenAsync();

                    // Consulta SQL para obtener todos los registros de la tabla ReservaHabitacion
                    var query = "SELECT NumReserva, IdHabitacion, PrecioHabitacion FROM ReservaHabitacion";
                    using (var command = new SqlCommand(query, connection))
                    {
                        using (var reader = await command.ExecuteReaderAsync())
                        {
                            var reservasHabitaciones = new List<ReservaHabitacion>();

                            while (await reader.ReadAsync())
                            {
                                reservasHabitaciones.Add(new ReservaHabitacion
                                {
                                    NumReserva = reader.GetInt32(0),         // ID de la reserva
                                    IdHabitacion = reader.GetInt32(1),      // ID de la habitación
                                    PrecioHabitacion = reader.GetDecimal(2)  // Precio de la habitación
                                });
                            }

                            if (reservasHabitaciones.Count > 0)
                                return Ok(reservasHabitaciones);
                            else
                                return NotFound("No se encontraron registros en la tabla ReservaHabitacion.");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error al obtener los datos de la tabla ReservaHabitacion: {ex.Message}");
            }
        }

        // Agregar una habitación a una reserva
        [HttpPost]
        public async Task<IActionResult> AgregarHabitacionReserva([FromBody] ReservaHabitacion reservaHabitacion)
        {
            try
            {
                using (var connection = new SqlConnection(_connectionString))
                {
                    await connection.OpenAsync();

                    // Query para insertar una nueva habitación a la reserva
                    var query = "INSERT INTO ReservaHabitacion (NumReserva, IdHabitacion, PrecioHabitacion) " +
                                "VALUES (@NumReserva, @IdHabitacion, @PrecioHabitacion)";
                    using (var command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@NumReserva", reservaHabitacion.NumReserva);
                        command.Parameters.AddWithValue("@IdHabitacion", reservaHabitacion.IdHabitacion); // ID de la habitación
                        command.Parameters.AddWithValue("@PrecioHabitacion", reservaHabitacion.PrecioHabitacion);

                        int rowsAffected = await command.ExecuteNonQueryAsync();
                        if (rowsAffected > 0)
                            return Ok("La habitación ha sido agregada a la reserva.");
                        else
                            return BadRequest("No se pudo agregar la habitación a la reserva.");
                    }
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error al agregar la habitación: {ex.Message}");
            }
        }

        // Eliminar una habitación de una reserva
        [HttpDelete("{numReserva}/{idHabitacion}")]
        public async Task<IActionResult> EliminarHabitacionReserva(int numReserva, int idHabitacion)
        {
            try
            {
                using (var connection = new SqlConnection(_connectionString))
                {
                    await connection.OpenAsync();

                    // Query para eliminar la habitación asociada a la reserva
                    var query = "DELETE FROM ReservaHabitacion WHERE NumReserva = @NumReserva AND IdHabitacion = @IdHabitacion";
                    using (var command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@NumReserva", numReserva);
                        command.Parameters.AddWithValue("@IdHabitacion", idHabitacion);

                        int rowsAffected = await command.ExecuteNonQueryAsync();
                        if (rowsAffected > 0)
                            return NoContent();
                        else
                            return NotFound("No se encontró la habitación asociada a la reserva.");
                    }
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error al eliminar la habitación: {ex.Message}");
            }
        }

        // Obtener todos los IDs de las habitaciones
        [HttpGet("habitaciones")]
        public async Task<IActionResult> ObtenerHabitaciones()
        {
            try
            {
                using (var connection = new SqlConnection(_connectionString))
                {
                    await connection.OpenAsync();

                    // Query para obtener todos los IDs de las habitaciones
                    var query = "SELECT IdHabitacion FROM Habitaciones";
                    using (var command = new SqlCommand(query, connection))
                    {
                        using (var reader = await command.ExecuteReaderAsync())
                        {
                            var habitaciones = new List<int>();
                            while (await reader.ReadAsync())
                            {
                                habitaciones.Add(reader.GetInt32(0)); // Agregamos el ID de la habitación
                            }

                            if (habitaciones.Count > 0)
                                return Ok(habitaciones);
                            else
                                return NotFound("No se encontraron habitaciones.");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error al obtener las habitaciones: {ex.Message}");
            }
        }

        // Obtener los números de las habitaciones que están disponibles
        [HttpGet("habitacionesDisponibles")]
        public async Task<IActionResult> ObtenerHabitacionesDisponibles()
        {
            try
            {
                using (var connection = new SqlConnection(_connectionString))
                {
                    await connection.OpenAsync();

                    // Consulta SQL para obtener las habitaciones cuyo estado es "Disponible"
                    var query = "SELECT IdHabitacion FROM Habitaciones WHERE Estado = 'Disponible'";
                    using (var command = new SqlCommand(query, connection))
                    {
                        using (var reader = await command.ExecuteReaderAsync())
                        {
                            var habitacionesDisponibles = new List<int>();

                            // Leer los resultados de la consulta
                            while (await reader.ReadAsync())
                            {
                                habitacionesDisponibles.Add(reader.GetInt32(0));  // Agregamos el número de la habitación
                            }

                            // Si encontramos habitaciones disponibles, las devolvemos
                            if (habitacionesDisponibles.Count > 0)
                                return Ok(habitacionesDisponibles);
                            else
                                return NotFound("No hay habitaciones disponibles.");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error al obtener las habitaciones disponibles: {ex.Message}");
            }
        }

    }
}
