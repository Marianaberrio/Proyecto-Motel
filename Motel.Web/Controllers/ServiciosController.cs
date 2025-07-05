using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Motel.Web.Models;
using System.Collections.Generic;
using System.Data;
using Microsoft.Data.SqlClient;


namespace Motel.Web.Controllers
{
    public class ServiciosController : Controller
    {
        private readonly string _connectionString;

        public ServiciosController(IConfiguration config)
        {
            _connectionString = config.GetConnectionString("ConexionMotel");
        }

        public IActionResult Index()
        {
            var servicios = new List<Servicio>();

            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                conn.Open();
                using (SqlCommand cmd = new SqlCommand("SELECT * FROM Servicios", conn)) // o usar un SP tipo ListarServicios
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        servicios.Add(new Servicio
                        {
                            NumServicio = reader.GetInt32(0),
                            NombreServicio = reader.GetString(1),
                            DescripcionServicio = reader.IsDBNull(2) ? null : reader.GetString(2),
                            PrecioServicio = reader.GetDecimal(3)
                        });
                    }
                }
            }

            return View(servicios);
        }

        public IActionResult Crear()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Crear(Servicio servicio)
        {
            if (!ModelState.IsValid) return View(servicio);

            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                conn.Open();
                using (SqlCommand cmd = new SqlCommand("CrearServicio", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@NombreServicio", servicio.NombreServicio);
                    cmd.Parameters.AddWithValue("@DescripcionServicio", servicio.DescripcionServicio ?? "");
                    cmd.Parameters.AddWithValue("@PrecioServicio", servicio.PrecioServicio);
                    cmd.ExecuteNonQuery();
                }
            }

            return RedirectToAction("Index");
        }

        public IActionResult Editar(int id)
        {
            Servicio servicio = null;

            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                conn.Open();
                using (SqlCommand cmd = new SqlCommand("ConsultarServicio", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@NumServicio", id);
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            servicio = new Servicio
                            {
                                NumServicio = reader.GetInt32(0),
                                NombreServicio = reader.GetString(1),
                                DescripcionServicio = reader.IsDBNull(2) ? null : reader.GetString(2),
                                PrecioServicio = reader.GetDecimal(3)
                            };
                        }
                    }
                }
            }

            if (servicio == null) return NotFound();
            return View(servicio);
        }

        [HttpPost]
        public IActionResult Editar(Servicio servicio)
        {
            if (!ModelState.IsValid) return View(servicio);

            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                conn.Open();
                using (SqlCommand cmd = new SqlCommand("ActualizarServicio", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@NumServicio", servicio.NumServicio);
                    cmd.Parameters.AddWithValue("@NombreServicio", servicio.NombreServicio);
                    cmd.Parameters.AddWithValue("@DescripcionServicio", servicio.DescripcionServicio ?? "");
                    cmd.Parameters.AddWithValue("@PrecioServicio", servicio.PrecioServicio);
                    cmd.ExecuteNonQuery();
                }
            }

            return RedirectToAction("Index");
        }

        public IActionResult Eliminar(int id)
        {
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                conn.Open();
                using (SqlCommand cmd = new SqlCommand("EliminarServicio", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@NumServicio", id);
                    cmd.ExecuteNonQuery();
                }
            }

            return RedirectToAction("Index");
        }
    }
}
