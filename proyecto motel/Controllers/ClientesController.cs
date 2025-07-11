using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Data;
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

        // GET: api/Clientes/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<Cliente>> Get(int id)
        {
            try
            {
                using var conn = new SqlConnection(_connectionString);
                await conn.OpenAsync();

                using var cmd = new SqlCommand("ConsultarCliente", conn)
                {
                    CommandType = CommandType.StoredProcedure
                };
                cmd.Parameters.AddWithValue("@NumCliente", id);

                using var reader = await cmd.ExecuteReaderAsync();
                if (!await reader.ReadAsync())
                    return NotFound();

                var cliente = new Cliente
                {
                    NumCliente = reader.GetInt32(0),
                    NombreCliente = reader.GetString(1),
                    ApellidoCliente = reader.GetString(2),
                    CorreoCliente = reader.GetString(3),
                    TelefonoCliente = reader.GetString(4),
                    FechaNacimiento = reader.GetDateTime(5)
                };

                return Ok(cliente);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error en el servidor: {ex.Message}");
            }
        }

        // PUT: api/Clientes/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] Cliente cliente)
        {
            if (cliente == null || id != cliente.NumCliente)
                return BadRequest("Datos inválidos.");

            try
            {
                using var conn = new SqlConnection(_connectionString);
                await conn.OpenAsync();

                // Usamos SQL directo para asegurar la actualización de FechaNacimiento
                const string sql = @"
                    UPDATE Clientes
                    SET 
                        NombreCliente    = @NombreCliente,
                        ApellidoCliente  = @ApellidoCliente,
                        CorreoCliente    = @CorreoCliente,
                        TelefonoCliente  = @TelefonoCliente,
                        FechaNacimiento  = @FechaNacimiento
                    WHERE NumCliente = @NumCliente";

                using var cmd = new SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@NombreCliente", cliente.NombreCliente);
                cmd.Parameters.AddWithValue("@ApellidoCliente", cliente.ApellidoCliente);
                cmd.Parameters.AddWithValue("@CorreoCliente", cliente.CorreoCliente);
                cmd.Parameters.AddWithValue("@TelefonoCliente", cliente.TelefonoCliente);
                cmd.Parameters.AddWithValue("@FechaNacimiento", cliente.FechaNacimiento);
                cmd.Parameters.AddWithValue("@NumCliente", cliente.NumCliente);

                var rows = await cmd.ExecuteNonQueryAsync();
                if (rows == 0)
                    return NotFound();

                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error en el servidor: {ex.Message}");
            }
        }

        // DELETE: api/Clientes/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                using var conn = new SqlConnection(_connectionString);
                await conn.OpenAsync();

                using var cmd = new SqlCommand("EliminarCliente", conn)
                {
                    CommandType = CommandType.StoredProcedure
                };
                cmd.Parameters.AddWithValue("@NumCliente", id);

                var rows = await cmd.ExecuteNonQueryAsync();
                if (rows == 0)
                    return NotFound();

                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error en el servidor: {ex.Message}");
            }
        }

        // GET: api/Clientes/buscar?email=foo@bar.com
        [HttpGet("buscar")]
        public async Task<IActionResult> BuscarClientePorEmail([FromQuery] string email)
        {
            if (string.IsNullOrWhiteSpace(email))
                return BadRequest("Email es requerido.");

            try
            {
                using var conn = new SqlConnection(_connectionString);
                await conn.OpenAsync();

                using var cmd = new SqlCommand("BuscarClientePorEmail", conn)
                {
                    CommandType = CommandType.StoredProcedure
                };
                cmd.Parameters.AddWithValue("@CorreoCliente", email);

                using var reader = await cmd.ExecuteReaderAsync();
                if (!await reader.ReadAsync())
                    return NotFound();

                var cliente = new Cliente
                {
                    NumCliente = reader.GetInt32(0),
                    NombreCliente = reader.GetString(1),
                    ApellidoCliente = reader.GetString(2),
                    CorreoCliente = reader.GetString(3),
                    TelefonoCliente = reader.GetString(4),
                    FechaNacimiento = reader.GetDateTime(5)
                };

                return Ok(cliente);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error en el servidor: {ex.Message}");
            }
        }

        // GET: api/clientes/ids
        [HttpGet("ids")]
        public async Task<ActionResult<List<int>>> GetAllClienteIds()
        {
            try
            {
                var ids = new List<int>();

                using (var conn = new SqlConnection(_connectionString))
                {
                    await conn.OpenAsync();

                    using (var cmd = new SqlCommand("SELECT NumCliente FROM Clientes", conn))
                    using (var reader = await cmd.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            ids.Add(reader.GetInt32(0)); // Agregar el ID del cliente a la lista
                        }
                    }
                }

                return Ok(ids);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error en el servidor: {ex.Message}");
            }
        }

        // GET: api/clientes/correos
        [HttpGet("correos")]
        public async Task<ActionResult<List<string>>> GetAllClienteEmails()
        {
            try
            {
                var correos = new List<string>();

                using (var conn = new SqlConnection(_connectionString))
                {
                    await conn.OpenAsync();

                    using (var cmd = new SqlCommand("SELECT CorreoCliente FROM Clientes", conn))
                    using (var reader = await cmd.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            correos.Add(reader.GetString(0)); // Agregar el correo del cliente a la lista
                        }
                    }
                }

                return Ok(correos);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error en el servidor: {ex.Message}");
            }
        }

        // GET: api/clientes/obtenerIdPorCorreo/{correo}
        [HttpGet("obtenerIdPorCorreo/{correo}")]
        public async Task<ActionResult<int>> ObtenerIdClientePorCorreo(string correo)
        {
            try
            {
                using var conn = new SqlConnection(_connectionString);
                await conn.OpenAsync();

                using var cmd = new SqlCommand("BuscarClientePorEmail", conn)
                {
                    CommandType = CommandType.StoredProcedure
                };
                cmd.Parameters.AddWithValue("@CorreoCliente", correo);

                using var reader = await cmd.ExecuteReaderAsync();
                if (await reader.ReadAsync())
                {
                    // Retornamos solo el ID del cliente
                    return Ok(reader.GetInt32(0));  // Suponiendo que el primer campo es NumCliente
                }

                return NotFound("Cliente no encontrado.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error en el servidor: {ex.Message}");
            }
        }

        // GET: api/clientes
        [HttpGet]
        public async Task<IActionResult> GetAllClientes()
        {
            try
            {
                var lista = new List<Cliente>();
                using (var conn = new SqlConnection(_connectionString))
                {
                    await conn.OpenAsync();

                    const string sql = @"
                SELECT 
                    NumCliente, 
                    NombreCliente, 
                    ApellidoCliente, 
                    TelefonoCliente, 
                    CorreoCliente,
                    FechaNacimiento,
                    FechaRegistro
                FROM Clientes";

                    using (var cmd = new SqlCommand(sql, conn))
                    using (var rdr = await cmd.ExecuteReaderAsync())
                    {
                        while (await rdr.ReadAsync())
                        {
                            lista.Add(new Cliente
                            {
                                NumCliente = rdr.GetInt32(0),
                                NombreCliente = rdr.GetString(1),
                                ApellidoCliente = rdr.GetString(2),
                                TelefonoCliente = rdr.IsDBNull(3) ? null : rdr.GetString(3),
                                CorreoCliente = rdr.IsDBNull(4) ? null : rdr.GetString(4),
                                FechaNacimiento = rdr.GetDateTime(5),
                                FechaRegistro = rdr.GetDateTime(6)
                            });
                        }
                    }
                }

                if (lista.Count == 0)
                    return NotFound("No hay clientes registrados.");

                return Ok(lista);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error al obtener clientes: {ex.Message}");
            }
        }



    }
}
