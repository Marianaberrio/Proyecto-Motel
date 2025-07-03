using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
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

        // POST: api/Clientes
        [HttpPost]
        public async Task<IActionResult> CrearCliente([FromBody] Cliente cliente)
        {
            if (cliente == null)
                return BadRequest("Cliente no puede ser nulo.");

            try
            {
                using var conn = new SqlConnection(_connectionString);
                await conn.OpenAsync();

                using var cmd = new SqlCommand("CrearCliente", conn)
                {
                    CommandType = CommandType.StoredProcedure
                };
                cmd.Parameters.AddWithValue("@NombreCliente",   cliente.NombreCliente);
                cmd.Parameters.AddWithValue("@ApellidoCliente", cliente.ApellidoCliente);
                cmd.Parameters.AddWithValue("@CorreoCliente",   cliente.CorreoCliente);
                cmd.Parameters.AddWithValue("@TelefonoCliente", cliente.TelefonoCliente);
                cmd.Parameters.AddWithValue("@FechaNacimiento", cliente.FechaNacimiento);

                // OUTPUT parameter
                var outId = new SqlParameter("@NumCliente", SqlDbType.Int)
                {
                    Direction = ParameterDirection.Output
                };
                cmd.Parameters.Add(outId);

                await cmd.ExecuteNonQueryAsync();
                cliente.NumCliente = (int)outId.Value;

                return CreatedAtAction(nameof(Get), new { id = cliente.NumCliente }, cliente);
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
                    NumCliente      = reader.GetInt32(0),
                    NombreCliente   = reader.GetString(1),
                    ApellidoCliente = reader.GetString(2),
                    CorreoCliente   = reader.GetString(3),
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
                cmd.Parameters.AddWithValue("@NombreCliente",   cliente.NombreCliente);
                cmd.Parameters.AddWithValue("@ApellidoCliente", cliente.ApellidoCliente);
                cmd.Parameters.AddWithValue("@CorreoCliente",   cliente.CorreoCliente);
                cmd.Parameters.AddWithValue("@TelefonoCliente", cliente.TelefonoCliente);
                cmd.Parameters.AddWithValue("@FechaNacimiento", cliente.FechaNacimiento);
                cmd.Parameters.AddWithValue("@NumCliente",       cliente.NumCliente);

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
                    NumCliente      = reader.GetInt32(0),
                    NombreCliente   = reader.GetString(1),
                    ApellidoCliente = reader.GetString(2),
                    CorreoCliente   = reader.GetString(3),
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
    }
}