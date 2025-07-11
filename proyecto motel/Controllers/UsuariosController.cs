using Microsoft.AspNetCore.Mvc;
using System.Data.SqlClient;
using System.Threading.Tasks;

namespace proyecto_motel.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsuariosController : ControllerBase
    {
        private readonly string _connectionString;

        public UsuariosController(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("ConexionMotel");
        }

        // POST: api/usuarios/agregar
        [HttpPost("agregar")]
        public async Task<IActionResult> AgregarUsuario([FromBody] Usuarios usuario)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(_connectionString))
                {
                    await connection.OpenAsync();

                    using (SqlCommand command = new SqlCommand("AgregarUsuario", connection))
                    {
                        command.CommandType = System.Data.CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("@Usuario", usuario.Usuario);
                        command.Parameters.AddWithValue("@Contraseña", usuario.Contraseña);
                        command.Parameters.AddWithValue("@Rol", usuario.Rol);

                        await command.ExecuteNonQueryAsync();
                    }
                }

                return CreatedAtAction(nameof(AgregarUsuario), new { id = usuario.Id }, usuario);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error en el servidor: {ex.Message}");
            }
        }

        // GET: api/usuarios
        [HttpGet]
        public async Task<IActionResult> ObtenerUsuariosAsync()
        {
            try
            {
                var listaUsuarios = new List<Usuarios>();

                using (var conn = new SqlConnection(_connectionString))
                {
                    await conn.OpenAsync();

                    // Consulta SQL para obtener todos los usuarios
                    const string sql = "SELECT Id, Usuario, Contraseña, Rol FROM Usuarios";

                    using (var cmd = new SqlCommand(sql, conn))
                    using (var reader = await cmd.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            listaUsuarios.Add(new Usuarios
                            {
                                Id = reader.GetInt32(0),          // Id
                                Usuario = reader.GetString(1),    // Usuario
                                Contraseña = reader.GetString(2), // Contraseña
                                Rol = reader.GetString(3)         // Rol
                            });
                        }
                    }
                }

                // Si no encontramos usuarios, devolver un NotFound
                if (listaUsuarios.Count == 0)
                    return NotFound("No se encontraron usuarios.");

                // Devolver la lista de usuarios
                return Ok(listaUsuarios);
            }
            catch (Exception ex)
            {
                // Si ocurre un error, devolver un StatusCode 500 con el mensaje de error
                return StatusCode(500, $"Error al obtener los usuarios: {ex.Message}");
            }
        }
        // GET: api/usuarios/buscarPorId/{id}
        [HttpGet("buscarPorId/{id}")]
        public ActionResult<Usuarios> BuscarUsuarioPorId(int id)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(_connectionString))
                {
                    connection.Open();

                    using (SqlCommand command = new SqlCommand("BuscarUsuario", connection))
                    {
                        command.CommandType = System.Data.CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("@Id", id);

                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                var usuario = new Usuarios
                                {
                                    Id = reader.GetInt32(0),
                                    Usuario = reader.GetString(1),
                                    Contraseña = reader.GetString(2),
                                    Rol = reader.GetString(3)
                                };

                                return Ok(usuario); // Devuelve el usuario encontrado
                            }
                            else
                            {
                                return NotFound(); // Si no se encuentra el usuario
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

        // GET: api/usuarios/buscarPorNombre/{usuario}
        [HttpGet("buscarPorNombre/{usuario}")]
        public ActionResult<Usuarios> BuscarUsuarioPorNombre(string usuario)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(_connectionString))
                {
                    connection.Open();

                    using (SqlCommand command = new SqlCommand("BuscarUsuario", connection))
                    {
                        command.CommandType = System.Data.CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("@Usuario", usuario);

                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                var usuarioEncontrado = new Usuarios
                                {
                                    Id = reader.GetInt32(0),
                                    Usuario = reader.GetString(1),
                                    Contraseña = reader.GetString(2),
                                    Rol = reader.GetString(3)
                                };

                                return Ok(usuarioEncontrado); // Devuelve el usuario encontrado
                            }
                            else
                            {
                                return NotFound(); // Si no se encuentra el usuario
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

        [HttpPut("modificarPorId/{id}")]
        public async Task<IActionResult> ModificarUsuarioPorId(int id, [FromBody] Usuarios usuario)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(_connectionString))
                {
                    connection.Open();

                    using (SqlCommand command = new SqlCommand("ModificarUsuario", connection))
                    {
                        command.CommandType = System.Data.CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("@Id", id);
                        command.Parameters.AddWithValue("@Usuario", usuario.Usuario); // Nombre de usuario
                        command.Parameters.AddWithValue("@Contraseña", usuario.Contraseña); // Nueva contraseña
                        command.Parameters.AddWithValue("@Rol", usuario.Rol); // Nuevo rol

                        int rowsAffected = await command.ExecuteNonQueryAsync();

                        if (rowsAffected > 0)
                        {
                            return NoContent(); // Usuario actualizado correctamente
                        }
                        else
                        {
                            return NotFound(); // Si no se encuentra el usuario con ese id
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error en el servidor: {ex.Message}");
            }
        }


        [HttpPut("modificarPorNombre/{usuario}")]
        public async Task<IActionResult> ModificarUsuarioPorNombre(string usuario, [FromBody] Usuarios usuarioModificado)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(_connectionString))
                {
                    connection.Open();

                    using (SqlCommand command = new SqlCommand("ModificarUsuario", connection))
                    {
                        command.CommandType = System.Data.CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("@Usuario", usuario); // Nombre de usuario
                        command.Parameters.AddWithValue("@Contraseña", usuarioModificado.Contraseña); // Nueva contraseña
                        command.Parameters.AddWithValue("@Rol", usuarioModificado.Rol); // Nuevo rol

                        int rowsAffected = await command.ExecuteNonQueryAsync();

                        if (rowsAffected > 0)
                        {
                            return NoContent(); // Usuario actualizado correctamente
                        }
                        else
                        {
                            return NotFound(); // Si no se encuentra el usuario con ese nombre
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error en el servidor: {ex.Message}");
            }
        }


        // DELETE: api/usuarios/eliminarPorId/{id}
        [HttpDelete("eliminarPorId/{id}")]
        public async Task<IActionResult> EliminarUsuarioPorId(int id)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(_connectionString))
                {
                    connection.Open();

                    using (SqlCommand command = new SqlCommand("EliminarUsuario", connection))
                    {
                        command.CommandType = System.Data.CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("@Id", id);

                        int rowsAffected = await command.ExecuteNonQueryAsync();

                        if (rowsAffected > 0)
                        {
                            return NoContent(); // Usuario eliminado correctamente
                        }
                        else
                        {
                            return NotFound(); // Si no se encuentra el usuario con ese id
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error en el servidor: {ex.Message}");
            }
        }

        // DELETE: api/usuarios/eliminarPorNombre/{usuario}
        [HttpDelete("eliminarPorNombre/{usuario}")]
        public async Task<IActionResult> EliminarUsuarioPorNombre(string usuario)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(_connectionString))
                {
                    connection.Open();

                    using (SqlCommand command = new SqlCommand("EliminarUsuario", connection))
                    {
                        command.CommandType = System.Data.CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("@Usuario", usuario);

                        int rowsAffected = await command.ExecuteNonQueryAsync();

                        if (rowsAffected > 0)
                        {
                            return NoContent(); // Usuario eliminado correctamente
                        }
                        else
                        {
                            return NotFound(); // Si no se encuentra el usuario con ese nombre
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
