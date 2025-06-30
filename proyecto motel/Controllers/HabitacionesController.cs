using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
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
    }
}
