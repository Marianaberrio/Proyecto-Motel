using Microsoft.AspNetCore.Mvc;
using System.Data;
using System.Data.SqlClient;
using System.Threading.Tasks;

namespace proyecto_motel.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReservaServiciosController : ControllerBase
    {
        private readonly string _connectionString;

        public ReservaServiciosController(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("ConexionMotel");
        }

        // GET: api/reservaservicios/todasReservaServicios
        [HttpGet("todasReservaServicios")]
        public async Task<IActionResult> ObtenerTodosLosServiciosReservaAsync()
        {
            try
            {
                var lista = new List<ReservaServicio>();

                using (var conn = new SqlConnection(_connectionString))
                {
                    await conn.OpenAsync();

                    // Consulta SQL para obtener todos los registros de la tabla ReservaServicio
                    const string sql = @"
                    SELECT NumReserva, NumServicio, PrecioServicio
                    FROM ReservaServicios
                ";

                    using (var cmd = new SqlCommand(sql, conn))
                    using (var reader = await cmd.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            lista.Add(new ReservaServicio
                            {
                                NumReserva = reader.GetInt32(0),
                                NumServicio = reader.GetInt32(1),
                                PrecioServicio = reader.GetDecimal(2)
                            });
                        }
                    }
                }

                // Si no encontramos servicios de reserva, devolver un NotFound
                if (lista.Count == 0)
                    return NotFound("No se encontraron servicios de reserva.");

                // Devolver la lista de servicios de reserva
                return Ok(lista);
            }
            catch (Exception ex)
            {
                // En caso de error, devolver un StatusCode 500 con el mensaje de error
                return StatusCode(500, $"Error al obtener los servicios de reserva: {ex.Message}");
            }
        }

        // GET: api/ReservaServicios/servicio/{nombreServicio}
        [HttpGet("servicio/{nombreServicio}")]
        public async Task<IActionResult> GetServicioByName(string nombreServicio)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(_connectionString))
                {
                    connection.Open();

                    var query = "SELECT * FROM Servicios WHERE NombreServicio = @NombreServicio";
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@NombreServicio", nombreServicio);
                        var reader = await command.ExecuteReaderAsync();

                        if (reader.Read())
                        {
                            var servicio = new
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
                            return NotFound("Servicio no encontrado");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error en el servidor: {ex.Message}");
            }
        }

        //buscar todos los servicios asociados a una reserva
        [HttpGet("serviciosPorReserva/{numReserva}")]
        public async Task<IActionResult> GetServiciosPorReserva(int numReserva)
        {
            try
            {
                using (var connection = new SqlConnection(_connectionString))
                {
                    await connection.OpenAsync();

                    // Consulta SQL para obtener los servicios asociados a una reserva específica
                    const string sql = "SELECT rs.NumReserva, rs.NumServicio, rs.PrecioServicio " +
                                       "FROM ReservaServicios rs " +
                                       "WHERE rs.NumReserva = @NumReserva";

                    using (var command = new SqlCommand(sql, connection))
                    {
                        // Añadir el parámetro para evitar inyección de SQL
                        command.Parameters.AddWithValue("@NumReserva", numReserva);

                        using (var reader = await command.ExecuteReaderAsync())
                        {
                            var serviciosReserva = new List<ReservaServicio>();

                            // Leer los resultados y mapearlos a la clase ReservaServicio
                            while (await reader.ReadAsync())
                            {
                                var reservaServicio = new ReservaServicio
                                {
                                    NumReserva = reader.GetInt32(0),         // ID de la reserva
                                    NumServicio = reader.GetInt32(1),        // ID del servicio
                                    PrecioServicio = reader.GetDecimal(2)    // Precio del servicio
                                };
                                serviciosReserva.Add(reservaServicio);
                            }

                            // Si no se encontraron servicios asociados a esta reserva
                            if (serviciosReserva.Count == 0)
                            {
                                return NotFound($"No se encontraron servicios para la reserva con ID {numReserva}.");
                            }

                            // Si encontramos los servicios, devolverlos como respuesta
                            return Ok(serviciosReserva);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error al obtener los servicios: {ex.Message}");
            }
        }



        // DELETE: api/ReservaServicios/servicio/{nombreServicio}
        [HttpDelete("{numReserva}/{numServicio}")]
        public async Task<IActionResult> DeleteServicioById(int numReserva, int numServicio)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(_connectionString))
                {
                    connection.Open();

                    // Cambiar la consulta para eliminar el servicio basado en el ID de la reserva y el ID del servicio
                    var query = "DELETE FROM ReservaServicios WHERE NumReserva = @NumReserva AND NumServicio = @NumServicio";
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@NumReserva", numReserva);
                        command.Parameters.AddWithValue("@NumServicio", numServicio);

                        int rowsAffected = await command.ExecuteNonQueryAsync();

                        if (rowsAffected > 0)
                        {
                            return NoContent(); // El servicio fue eliminado correctamente
                        }
                        else
                        {
                            return NotFound("Servicio no encontrado en la reserva especificada.");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error en el servidor: {ex.Message}");
            }
        }


        // PUT: api/ReservaServicios/servicio/{nombreServicio}
        [HttpPut("servicio/{nombreServicio}")]
        public async Task<IActionResult> UpdateServicioByName(string nombreServicio, [FromBody] Servicios servicio)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(_connectionString))
                {
                    connection.Open();

                    var query = "UPDATE Servicios SET NombreServicio = @NombreServicio, DescripcionServicio = @DescripcionServicio, PrecioServicio = @PrecioServicio WHERE NombreServicio = @NombreServicio";
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@NombreServicio", servicio.NombreServicio);
                        command.Parameters.AddWithValue("@DescripcionServicio", servicio.DescripcionServicio ?? (object)DBNull.Value);
                        command.Parameters.AddWithValue("@PrecioServicio", servicio.PrecioServicio);

                        int rowsAffected = await command.ExecuteNonQueryAsync();

                        if (rowsAffected > 0)
                        {
                            return NoContent();
                        }
                        else
                        {
                            return NotFound("Servicio no encontrado");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error en el servidor: {ex.Message}");
            }
        }

        // POST: api/ReservaServicios
        [HttpPost]
        public async Task<IActionResult> AddServicioToReserva([FromBody] ReservaServicio reservaServicio)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(_connectionString))
                {
                    connection.Open();

                    var query = "INSERT INTO ReservaServicios (NumReserva, NumServicio, PrecioServicio) VALUES (@NumReserva, @NumServicio, @PrecioServicio)";
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@NumReserva", reservaServicio.NumReserva);
                        command.Parameters.AddWithValue("@NumServicio", reservaServicio.NumServicio);
                        command.Parameters.AddWithValue("@PrecioServicio", reservaServicio.PrecioServicio);

                        await command.ExecuteNonQueryAsync();
                    }
                }

                return CreatedAtAction(nameof(AddServicioToReserva), new { reservaId = reservaServicio.NumReserva }, reservaServicio);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error en el servidor: {ex.Message}");
            }
        }

        // GET: api/ReservaServicios/ids/reservas
        [HttpGet("ids/reservas")]
        public async Task<IActionResult> GetReservasIds()
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(_connectionString))
                {
                    connection.Open();

                    var query = "SELECT NumReserva FROM Reservas";
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        var reader = await command.ExecuteReaderAsync();
                        var idsReservas = new List<int>();

                        while (await reader.ReadAsync())
                        {
                            idsReservas.Add(reader.GetInt32(0));
                        }

                        return Ok(idsReservas);
                    }
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error en el servidor: {ex.Message}");
            }
        }

        // GET: api/ReservaServicios/ids/servicios
        [HttpGet("ids/servicios")]
        public async Task<IActionResult> GetServiciosIds()
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(_connectionString))
                {
                    connection.Open();

                    var query = "SELECT NumServicio FROM Servicios";
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        var reader = await command.ExecuteReaderAsync();
                        var idsServicios = new List<int>();

                        while (await reader.ReadAsync())
                        {
                            idsServicios.Add(reader.GetInt32(0));
                        }

                        return Ok(idsServicios);
                    }
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error en el servidor: {ex.Message}");
            }
        }

        // GET: api/ReservaServicios/nombres-precios-ids
        [HttpGet("nombres-precios-ids")]
        public async Task<IActionResult> ObtenerNombresPreciosIdsServicios()
        {
            try
            {
                using (var connection = new SqlConnection(_connectionString))
                {
                    await connection.OpenAsync();

                    // Consulta SQL para obtener los ID, Nombre y Precio de los servicios
                    const string sql = "SELECT NumServicio, NombreServicio, PrecioServicio FROM Servicios";

                    using (var command = new SqlCommand(sql, connection))
                    {
                        using (var reader = await command.ExecuteReaderAsync())
                        {
                            var servicios = new List<Servicios>(); // Usamos la clase Servicios que ya tienes

                            while (await reader.ReadAsync())
                            {
                                var servicio = new Servicios
                                {
                                    NumServicio = reader.GetInt32(0),          // ID del servicio
                                    NombreServicio = reader.GetString(1),      // Nombre del servicio
                                    PrecioServicio = reader.GetDecimal(2)      // Precio del servicio
                                };
                                servicios.Add(servicio);
                            }

                            return Ok(servicios);  // Devolvemos la lista de servicios con ID, Nombre y Precio
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error al obtener los servicios: {ex.Message}");
            }
        }

    }
}
