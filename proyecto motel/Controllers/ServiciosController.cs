using Microsoft.AspNetCore.Mvc;
using System.Data.SqlClient;
using System.Threading.Tasks;

namespace proyecto_motel.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ServiciosController : ControllerBase
    {
        private readonly string _connectionString;

        public ServiciosController(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("ConexionMotel");
        }

        // POST: api/servicios
        [HttpPost]
        public async Task<IActionResult> CrearServicio([FromBody] Servicios servicio)
        {
            if (servicio == null)
            {
                return BadRequest("El servicio no puede ser nulo.");
            }

            try
            {
                using (SqlConnection connection = new SqlConnection(_connectionString))
                {
                    await connection.OpenAsync();

                    using (SqlCommand command = new SqlCommand("CrearServicio", connection))
                    {
                        command.CommandType = System.Data.CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("@NombreServicio", servicio.NombreServicio);
                        command.Parameters.AddWithValue("@DescripcionServicio", servicio.DescripcionServicio);
                        command.Parameters.AddWithValue("@PrecioServicio", servicio.PrecioServicio);

                        await command.ExecuteNonQueryAsync();
                    }
                }

                return CreatedAtAction(nameof(CrearServicio), new { id = servicio.NumServicio }, servicio);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error en el servidor: {ex.Message}");
            }
        }

        // GET: api/servicios/{numServicio}
        [HttpGet("{numServicio}")]
        public ActionResult<Servicios> Get(int numServicio)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(_connectionString))
                {
                    connection.Open();

                    using (SqlCommand command = new SqlCommand("ConsultarServicio", connection))
                    {
                        command.CommandType = System.Data.CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("@NumServicio", numServicio);

                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                var servicio = new Servicios
                                {
                                    NumServicio = reader.GetInt32(0),
                                    NombreServicio = reader.GetString(1),
                                    DescripcionServicio = reader.IsDBNull(2) ? null : reader.GetString(2),
                                    PrecioServicio = reader.GetDecimal(3)
                                };

                                return Ok(servicio);
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

        // PUT: api/servicios/{numServicio}
        [HttpPut("{numServicio}")]
        public IActionResult Put(int numServicio, [FromBody] Servicios servicio)
        {
            if (servicio == null)
            {
                return BadRequest("El servicio no puede ser nulo.");
            }

            try
            {
                using (SqlConnection connection = new SqlConnection(_connectionString))
                {
                    connection.Open();

                    using (SqlCommand command = new SqlCommand("ActualizarServicio", connection))
                    {
                        command.CommandType = System.Data.CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("@NumServicio", numServicio);
                        command.Parameters.AddWithValue("@NombreServicio", servicio.NombreServicio);
                        command.Parameters.AddWithValue("@DescripcionServicio", servicio.DescripcionServicio);
                        command.Parameters.AddWithValue("@PrecioServicio", servicio.PrecioServicio);

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

        // DELETE: api/servicios/{numServicio}
        [HttpDelete("{numServicio}")]
        public IActionResult Delete(int numServicio)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(_connectionString))
                {
                    connection.Open();

                    using (SqlCommand command = new SqlCommand("EliminarServicio", connection))
                    {
                        command.CommandType = System.Data.CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("@NumServicio", numServicio);

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
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(_connectionString))
                {
                    await connection.OpenAsync();

                    var servicios = new List<Servicios>();
                    using (SqlCommand command = new SqlCommand("SELECT * FROM Servicios", connection))
                    {
                        using (SqlDataReader reader = await command.ExecuteReaderAsync())
                        {
                            while (await reader.ReadAsync())
                            {
                                servicios.Add(new Servicios
                                {
                                    NumServicio = reader.GetInt32(0),
                                    NombreServicio = reader.GetString(1),
                                    DescripcionServicio = reader.IsDBNull(2) ? null : reader.GetString(2),
                                    PrecioServicio = reader.GetDecimal(3)
                                });
                            }
                        }
                    }

                    return Ok(servicios);
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error en el servidor: {ex.Message}");
            }
        }
    }
}
