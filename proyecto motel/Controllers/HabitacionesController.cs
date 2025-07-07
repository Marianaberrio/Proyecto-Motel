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
                // 1. Verificación de lo que recibimos en el parámetro
                Console.WriteLine("Recibido numHabitacion: " + numHabitacion);  // Verificar si estamos recibiendo el valor correctamente

                using (SqlConnection connection = new SqlConnection(_connectionString))
                {
                    connection.Open();

                    using (SqlCommand command = new SqlCommand("ConsultarHabitacion", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;

                        // 2. Asegurarse de que pasamos el valor de numHabitacion o DBNull
                        if (string.IsNullOrEmpty(numHabitacion))
                        {
                            Console.WriteLine("Pasando NULL para @NumHabitacion");  // Si está vacío, pasamos NULL
                            command.Parameters.AddWithValue("@NumHabitacion", DBNull.Value);
                        }
                        else
                        {
                            Console.WriteLine("Pasando el número de habitación: " + numHabitacion);  // Si no está vacío, lo pasamos
                            command.Parameters.AddWithValue("@NumHabitacion", numHabitacion);
                        }

                        command.Parameters.AddWithValue("@IdHabitacion", DBNull.Value);  // No pasamos ID en este caso

                        // 3. Ejecutar la consulta
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

                                return Ok(habitacion); // Si la encontramos, devolvemos los datos
                            }
                            else
                            {
                                Console.WriteLine("No se encontró la habitación con el número proporcionado.");  // Si no se encuentra la habitación
                                return NotFound();  // Si no se encuentra
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
                // 1. Verificación de lo que recibimos en el parámetro
                Console.WriteLine("Recibido idHabitacion: " + idHabitacion);  // Verificar si estamos recibiendo el valor correctamente

                using (SqlConnection connection = new SqlConnection(_connectionString))
                {
                    connection.Open();

                    using (SqlCommand command = new SqlCommand("ConsultarHabitacion", connection))  // Usando el SP para ID
                    {
                        command.CommandType = CommandType.StoredProcedure;

                        // 2. Asegurarse de que pasamos el valor de idHabitacion o DBNull
                        if (idHabitacion <= 0)
                        {
                            Console.WriteLine("Pasando NULL para @IdHabitacion");  // Si el ID es <= 0, pasamos NULL
                            command.Parameters.AddWithValue("@IdHabitacion", DBNull.Value);
                        }
                        else
                        {
                            Console.WriteLine("Pasando el ID de habitación: " + idHabitacion);  // Si es válido, lo pasamos
                            command.Parameters.AddWithValue("@IdHabitacion", idHabitacion);
                        }

                        command.Parameters.AddWithValue("@NumHabitacion", DBNull.Value);  // No pasamos numHabitacion en este caso

                        // 3. Ejecutar la consulta
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

                                return Ok(habitacion);  // Si la encontramos, devolvemos los datos
                            }
                            else
                            {
                                Console.WriteLine("No se encontró la habitación con el ID proporcionado.");  // Si no se encuentra
                                return NotFound();  // Si no se encuentra
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
    }
}
