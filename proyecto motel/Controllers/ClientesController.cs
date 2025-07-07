using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;  // Necesario para IConfiguration
using System.Data.SqlClient;
using System.Threading.Tasks;

namespace proyecto_motel.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClientesController : ControllerBase
    {
        private readonly string _connectionString;

        public ClientesController(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("ConexionMotel");
        }

        // POST: api/clientes
        [HttpPost]
        public async Task<IActionResult> CrearCliente([FromBody] Cliente cliente)
        {
            if (cliente == null)
            {
                return BadRequest("Cliente no puede ser nulo.");
            }

            try
            {
                using (SqlConnection connection = new SqlConnection(_connectionString))
                {
                    await connection.OpenAsync();

                    using (SqlCommand command = new SqlCommand("CrearCliente", connection))
                    {
                        command.CommandType = System.Data.CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("@NombreCliente", cliente.NombreCliente);
                        command.Parameters.AddWithValue("@ApellidoCliente", cliente.ApellidoCliente);
                        command.Parameters.AddWithValue("@CorreoCliente", cliente.CorreoCliente);
                        command.Parameters.AddWithValue("@TelefonoCliente", cliente.TelefonoCliente);
                        command.Parameters.AddWithValue("@FechaNacimiento", cliente.FechaNacimiento);

                        await command.ExecuteNonQueryAsync();
                    }
                }

                return CreatedAtAction(nameof(CrearCliente), new { id = cliente.NumCliente }, cliente);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error en el servidor: {ex.Message}");
            }
        }

        // GET: api/clientes/{idCliente}
        [HttpGet("{id}")]
        public ActionResult<Cliente> Get(int id)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(_connectionString))
                {
                    connection.Open();

                    using (SqlCommand command = new SqlCommand("ConsultarCliente", connection)) // Usando un SP para el ID
                    {
                        command.CommandType = System.Data.CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("@NumCliente", id);  // Pasamos el ID del cliente

                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                var cliente = new Cliente
                                {
                                    NumCliente = reader.GetInt32(0),
                                    NombreCliente = reader.GetString(1),
                                    ApellidoCliente = reader.GetString(2),
                                    CorreoCliente = reader.GetString(3),
                                    TelefonoCliente = reader.GetString(4),
                                    FechaNacimiento = reader.GetDateTime(5),
                                    FechaRegistro = reader.GetDateTime(6) // Aquí agregamos la Fecha de Registro
                                };

                                return Ok(cliente);  // Cliente encontrado
                            }
                            else
                            {
                                return NotFound();  // Si no se encuentra el cliente
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

        // PUT: api/clientes/{numCliente}
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] Cliente cliente)
        {
            if (cliente == null)
            {
                return BadRequest("Cliente no puede ser nulo.");
            }

            try
            {
                using (SqlConnection connection = new SqlConnection(_connectionString))
                {
                    connection.Open();

                    using (SqlCommand command = new SqlCommand("ActualizarCliente", connection))
                    {
                        command.CommandType = System.Data.CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("@NumCliente", id);
                        command.Parameters.AddWithValue("@NombreCliente", cliente.NombreCliente);
                        command.Parameters.AddWithValue("@ApellidoCliente", cliente.ApellidoCliente);
                        command.Parameters.AddWithValue("@CorreoCliente", cliente.CorreoCliente);
                        command.Parameters.AddWithValue("@TelefonoCliente", cliente.TelefonoCliente);
                        command.Parameters.AddWithValue("@FechaNacimiento", cliente.FechaNacimiento);

                        int rowsAffected = command.ExecuteNonQuery();

                        if (rowsAffected > 0)
                        {
                            return NoContent(); // El cliente fue actualizado
                        }
                        else
                        {
                            return NotFound(); // Si no se encuentra el cliente para actualizar
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error en el servidor: {ex.Message}");
            }
        }

        // DELETE: api/clientes/{numCliente}
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(_connectionString))
                {
                    connection.Open();

                    using (SqlCommand command = new SqlCommand("EliminarCliente", connection))
                    {
                        command.CommandType = System.Data.CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("@NumCliente", id);

                        int rowsAffected = command.ExecuteNonQuery();

                        if (rowsAffected > 0)
                        {
                            return NoContent(); // El cliente fue eliminado
                        }
                        else
                        {
                            return NotFound(); // Si no se encuentra el cliente para eliminar
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
