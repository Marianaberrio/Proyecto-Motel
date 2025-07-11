using Microsoft.Reporting.WinForms;
using Microsoft.SqlServer.Types;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.Reporting.WinForms;

namespace AdminApp
{
    public partial class Reportes : Form
    {

        public Reportes()
        {
            InitializeComponent();
        }

        private void Reportes_Load(object sender, EventArgs e)
        {

            this.reportePagos.RefreshReport();
        }

        protected override async void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            await CargarReportePagosAsync();

            await CargarReporteClientesAsync();

            await CargarReporteHabitacionesAsync();

            await CargarReporteReservaHabitacionAsync();

            await CargarReporteReservaAsync();

            await CargarReporteReservaServicioAsync();

            await CargarReporteServicios();

            await CargarReporteUsuarios();
        }

        private async Task CargarReportePagosAsync()
        {
            // 1) Obtener la lista de pagos desde la API
            List<ClasePagos> pagos;
            try
            {
                pagos = await ObtenerTodosLosPagosAsync();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al obtener los pagos: {ex.Message}",
                                "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // 2) Preparar el ReportDataSource
            //    Asegúrate de que en tu RptPagos.rdlc el DataSet se llame "DataSet1"
            var rds = new ReportDataSource("DataSet1", pagos);

            // 3) Limpiar y asignar el nuevo origen de datos
            reportePagos.LocalReport.DataSources.Clear();
            reportePagos.LocalReport.DataSources.Add(rds);

            // 4) Especificar el reporte embebido (ajusta el namespace si fuese distinto)
            reportePagos.LocalReport.ReportEmbeddedResource = "AdminApp.RptPagos.rdlc";

            // 5) Refrescar el control
            reportePagos.RefreshReport();
        }
        private async Task CargarReporteClientesAsync()
        {
            // 1) Obtener la lista de clientes desde la API
            List<ClaseClientes> clientes;
            try
            {
                clientes = await ObtenerTodosLosClientesAsync();
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    $"Error al obtener los clientes: {ex.Message}",
                    "Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error
                );
                return;
            }

            // 2) Preparar el ReportDataSource
            //    Asegúrate de que en tu RptClientes.rdlc el DataSet se llame "DataSetClientes"
            var rds = new ReportDataSource("DataSet1", clientes);

            // 3) Limpiar y asignar el nuevo origen de datos
            ReporteClientes.LocalReport.DataSources.Clear();
            ReporteClientes.LocalReport.DataSources.Add(rds);

            // 4) Especificar el reporte embebido (ajusta el namespace/archivo si fuese distinto)
            ReporteClientes.LocalReport.ReportEmbeddedResource = "AdminApp.RptClientes.rdlc";

            // 5) Refrescar el control para renderizar los datos
            ReporteClientes.RefreshReport();
        }

        private async Task CargarReporteHabitacionesAsync()
        {
            // 1) Obtener la lista de habitaciones desde la API
            List<ClaseHabitacion> habitaciones;
            try
            {
                habitaciones = await ObtenerHabitacionesAsync();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al obtener las habitaciones: {ex.Message}",
                                "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // 2) Preparar el ReportDataSource
            //    Asegúrate de que en tu RptHabitaciones.rdlc el DataSet se llame "DataSet1"
            var rds = new ReportDataSource("Habitaciones", habitaciones);

            // 3) Limpiar y asignar el nuevo origen de datos
            ReporteHabitaciones.LocalReport.DataSources.Clear();
            ReporteHabitaciones.LocalReport.DataSources.Add(rds);

            // 4) Especificar el reporte embebido (ajusta el namespace si fuese distinto)
            ReporteHabitaciones.LocalReport.ReportEmbeddedResource = "AdminApp.RptHabitaciones.rdlc";

            // 5) Refrescar el control
            ReporteHabitaciones.RefreshReport();
        }

        private async Task CargarReporteReservaHabitacionAsync()
        {
            // 1) Obtener la lista de reservaHabitaciones desde la API
            List<ClaseReservaHabitacion> reservaHabitaciones;
            try
            {
                reservaHabitaciones = await ObtenerReservaHabitacionesAsync();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al obtener las reservas de habitaciones: {ex.Message}",
                                "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // 2) Preparar el ReportDataSource
            //    Asegúrate de que en tu RptReservaHabitacion.rdlc el DataSet se llame "ReservaHabitacion"
            var rds = new ReportDataSource("ReservaHabitacion", reservaHabitaciones);

            // 3) Limpiar y asignar el nuevo origen de datos
            reporteReservaHabitacion.LocalReport.DataSources.Clear();
            reporteReservaHabitacion.LocalReport.DataSources.Add(rds);

            // 4) Especificar el reporte embebido (ajusta el namespace si fuese distinto)
            reporteReservaHabitacion.LocalReport.ReportEmbeddedResource = "AdminApp.RptReservaHabitacion.rdlc";

            // 5) Refrescar el control
            reporteReservaHabitacion.RefreshReport();
        }

        private async Task CargarReporteReservaAsync()
        {
            // 1) Obtener la lista de reservas desde la API
            List<ClaseReservas> reservas;
            try
            {
                reservas = await ObtenerReservasAsync();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al obtener las reservas: {ex.Message}",
                                "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // 2) Preparar el ReportDataSource
            //    Asegúrate de que en tu RptReserva.rdlc el DataSet se llame "Reserva"
            var rds = new ReportDataSource("Reservas", reservas);

            // 3) Limpiar y asignar el nuevo origen de datos
            reporteReservas.LocalReport.DataSources.Clear();
            reporteReservas.LocalReport.DataSources.Add(rds);

            // 4) Especificar el reporte embebido (ajusta el namespace si fuese distinto)
            reporteReservas.LocalReport.ReportEmbeddedResource = "AdminApp.RptReservas.rdlc";

            // 5) Refrescar el control
            reporteReservas.RefreshReport();
        }

        private async Task CargarReporteReservaServicioAsync()
        {
            // 1) Obtener la lista de ReservaServicios desde la API
            List<ClaseReservaServicio> reservaServicios;
            try
            {
                reservaServicios = await ObtenerReservaServiciosAsync();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al obtener los servicios de reserva: {ex.Message}",
                                "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // 2) Preparar el ReportDataSource
            //    Asegúrate de que en tu RptReservaServicio.rdlc el DataSet se llame "ReservaServicio"
            var rds = new ReportDataSource("ReservaServicio", reservaServicios);

            // 3) Limpiar y asignar el nuevo origen de datos
            reporteReservaServicio.LocalReport.DataSources.Clear();
            reporteReservaServicio.LocalReport.DataSources.Add(rds);

            // 4) Especificar el reporte embebido (ajusta el namespace si fuese distinto)
            reporteReservaServicio.LocalReport.ReportEmbeddedResource = "AdminApp.RptReservaServicio.rdlc";

            // 5) Refrescar el control
            reporteReservaServicio.RefreshReport();
        }

        private async Task CargarReporteServicios()
        {
            // 1) Obtener los servicios desde la API
            List<ClaseServicios> servicios;
            try
            {
                servicios = await ObtenerServiciosAsync();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al obtener los servicios: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // 2) Preparar el ReportDataSource con la referencia completa
            var rds = new Microsoft.Reporting.WinForms.ReportDataSource("Servicios", servicios);

            // 3) Limpiar y asignar el nuevo origen de datos al ReportViewer
            reporteServicios.LocalReport.DataSources.Clear();
            reporteServicios.LocalReport.DataSources.Add(rds);

            // 4) Especificar el reporte embebido (ajusta la ruta según sea necesario)
            reporteServicios.LocalReport.ReportEmbeddedResource = "AdminApp.RptServicios.rdlc"; // Asegúrate de que la ruta del reporte sea correcta

            // 5) Refrescar el ReportViewer
            reporteServicios.RefreshReport();
        }

        private async Task CargarReporteUsuarios()
        {
            // 1) Obtener los servicios desde la API
            List<ClaseUsuarios> usuarios;
            try
            {
                usuarios = await ObtenerUsuariosAsync();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al obtener los servicios: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // 2) Preparar el ReportDataSource con la referencia completa
            var rds = new Microsoft.Reporting.WinForms.ReportDataSource("Usuarios", usuarios);

            // 3) Limpiar y asignar el nuevo origen de datos al ReportViewer
            reporteUsuarios.LocalReport.DataSources.Clear();
            reporteUsuarios.LocalReport.DataSources.Add(rds);

            // 4) Especificar el reporte embebido (ajusta la ruta según sea necesario)
            reporteUsuarios.LocalReport.ReportEmbeddedResource = "AdminApp.RptUsuarios.rdlc"; // Asegúrate de que la ruta del reporte sea correcta

            // 5) Refrescar el ReportViewer
            reporteUsuarios.RefreshReport();
        }


        private async Task<List<ClasePagos>> ObtenerTodosLosPagosAsync()
        {
            using (var client = new HttpClient())
            {
                var resp = await client.GetAsync("http://localhost:5264/api/pagos");
                resp.EnsureSuccessStatusCode();

                var json = await resp.Content.ReadAsStringAsync();
                var list = JsonConvert.DeserializeObject<List<ClasePagos>>(json);
                return list ?? new List<ClasePagos>();
            }
        }
        private async Task<List<ClaseClientes>> ObtenerTodosLosClientesAsync()
        {
            using (var client = new HttpClient())
            {
                var resp = await client.GetAsync("http://localhost:5264/api/clientes");
                resp.EnsureSuccessStatusCode();
                var json = await resp.Content.ReadAsStringAsync();
                return JsonConvert
                    .DeserializeObject<List<ClaseClientes>>(json)
                    ?? new List<ClaseClientes>();
            }
        }

        private async Task<List<ClaseHabitacion>> ObtenerHabitacionesAsync()
        {
            try
            {
                using (var client = new HttpClient())
                {
                    string url = "http://localhost:5264/api/habitaciones";  // La URL de tu API para obtener todas las habitaciones
                    var response = await client.GetAsync(url);

                    if (!response.IsSuccessStatusCode)
                    {
                        MessageBox.Show("Error al obtener las habitaciones: " + response.ReasonPhrase);
                        return new List<ClaseHabitacion>();
                    }

                    var json = await response.Content.ReadAsStringAsync();
                    var habitaciones = JsonConvert.DeserializeObject<List<ClaseHabitacion>>(json);
                    return habitaciones ?? new List<ClaseHabitacion>();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Excepción al obtener las habitaciones: " + ex.Message);
                return new List<ClaseHabitacion>();
            }
        }

        private async Task<List<ClaseReservaHabitacion>> ObtenerReservaHabitacionesAsync()
        {
            try
            {
                using (var client = new HttpClient())
                {
                    string url = "http://localhost:5264/api/ReservaHabitacion/todasReservaHabitacion";  // Asegúrate de que esta URL sea la correcta
                    var response = await client.GetAsync(url);

                    if (response.IsSuccessStatusCode)
                    {
                        var jsonResponse = await response.Content.ReadAsStringAsync();
                        var reservaHabitaciones = JsonConvert.DeserializeObject<List<ClaseReservaHabitacion>>(jsonResponse);
                        return reservaHabitaciones ?? new List<ClaseReservaHabitacion>();
                    }
                    else
                    {
                        MessageBox.Show("Error al obtener las reservas de habitaciones.");
                        return new List<ClaseReservaHabitacion>();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al obtener las reservas de habitaciones: {ex.Message}");
                return new List<ClaseReservaHabitacion>();
            }
        }

        private async Task<List<ClaseReservas>> ObtenerReservasAsync()
        {
            try
            {
                using (var client = new HttpClient())
                {
                    string url = "http://localhost:5264/api/Reservas";  // Asegúrate de que esta URL sea la correcta
                    var response = await client.GetAsync(url);

                    if (response.IsSuccessStatusCode)
                    {
                        var jsonResponse = await response.Content.ReadAsStringAsync();
                        var reservas = JsonConvert.DeserializeObject<List<ClaseReservas>>(jsonResponse);
                        return reservas ?? new List<ClaseReservas>();
                    }
                    else
                    {
                        MessageBox.Show("Error al obtener las reservas.");
                        return new List<ClaseReservas>();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al obtener las reservas: {ex.Message}");
                return new List<ClaseReservas>();
            }
        }

        private async Task<List<ClaseReservaServicio>> ObtenerReservaServiciosAsync()
        {
            try
            {
                using (var client = new HttpClient())
                {
                    string url = "http://localhost:5264/api/ReservaServicios/todasReservaServicios";  // Asegúrate de que esta URL sea la correcta
                    var response = await client.GetAsync(url);

                    if (response.IsSuccessStatusCode)
                    {
                        var jsonResponse = await response.Content.ReadAsStringAsync();
                        var reservaServicios = JsonConvert.DeserializeObject<List<ClaseReservaServicio>>(jsonResponse);
                        return reservaServicios ?? new List<ClaseReservaServicio>();
                    }
                    else
                    {
                        MessageBox.Show("Error al obtener los servicios de reserva.");
                        return new List<ClaseReservaServicio>();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al obtener los servicios de reserva: {ex.Message}");
                return new List<ClaseReservaServicio>();
            }
        }

        private async Task<List<ClaseServicios>> ObtenerServiciosAsync()
        {
            try
            {
                using (var client = new HttpClient())
                {
                    string url = "http://localhost:5264/api/Servicios";  // Asegúrate de que esta URL sea la correcta
                    var response = await client.GetAsync(url);

                    if (response.IsSuccessStatusCode)
                    {
                        // Leer la respuesta como un string
                        var jsonResponse = await response.Content.ReadAsStringAsync();

                        // Deserializar el JSON a una lista de objetos de tipo ClaseServicio
                        var servicios = JsonConvert.DeserializeObject<List<ClaseServicios>>(jsonResponse);

                        return servicios ?? new List<ClaseServicios>(); // Si es nulo, retorna una lista vacía
                    }
                    else
                    {
                        MessageBox.Show("Error al obtener los servicios.");
                        return new List<ClaseServicios>(); // Si la respuesta no es exitosa, retorna una lista vacía
                    }
                }
            }
            catch (Exception ex)
            {
                // Si ocurre una excepción, muestra el error y retorna una lista vacía
                MessageBox.Show($"Error al obtener los servicios: {ex.Message}");
                return new List<ClaseServicios>(); // Retorna una lista vacía en caso de error
            }
        }

        private async Task<List<ClaseUsuarios>> ObtenerUsuariosAsync()
        {
            try
            {
                using (var client = new HttpClient())
                {
                    string url = "http://localhost:5264/api/Usuarios";  // Asegúrate de que esta URL sea la correcta
                    var response = await client.GetAsync(url);

                    if (response.IsSuccessStatusCode)
                    {
                        // Leer la respuesta como string
                        var jsonResponse = await response.Content.ReadAsStringAsync();

                        // Deserializar la respuesta JSON en una lista de objetos Usuario
                        var usuarios = JsonConvert.DeserializeObject<List<ClaseUsuarios>>(jsonResponse);

                        return usuarios ?? new List<ClaseUsuarios>(); // Si es nulo, retorna una lista vacía
                    }
                    else
                    {
                        MessageBox.Show("Error al obtener los usuarios.");
                        return new List<ClaseUsuarios>(); // Si la respuesta no es exitosa, retorna una lista vacía
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al obtener los usuarios: {ex.Message}");
                return new List<ClaseUsuarios>(); // Retorna una lista vacía en caso de error
            }
        }


        private void btnReportePagos_Click(object sender, EventArgs e)
        {
            //reportePagos.Visible = true;
        }

        private void btnReportePagos_MouseClick(object sender, MouseEventArgs e)
        {
            reportePagos.Visible = !reportePagos.Visible;

            // Si acabas de mostrarlo, refresca para que se pinte correctamente
            if (reportePagos.Visible)
            {
                reportePagos.RefreshReport();
                ReporteClientes.Visible = false;
                ReporteHabitaciones.Visible = false;
                reporteReservaHabitacion.Visible = false;
                reporteReservas.Visible = false;
                reporteReservaServicio.Visible = false;
                reporteServicios.Visible = false;
                reporteUsuarios.Visible = false;
            }


        }

        private void btnReporteClientes_MouseClick(object sender, MouseEventArgs e)
        {
            ReporteClientes.Visible = !ReporteClientes.Visible;
            if(ReporteClientes.Visible)
            {
                ReporteClientes.RefreshReport();
                reportePagos.RefreshReport();
                reportePagos.Visible = false;
                ReporteHabitaciones.Visible = false;
                reporteReservaHabitacion.Visible = false;
                reporteReservas.Visible = false;
                reporteReservaServicio.Visible = false;
                reporteServicios.Visible = false;
                reporteUsuarios.Visible = false;
            }
        }

        private void btnReporteHabitaciones_Click(object sender, EventArgs e)
        {
            ReporteHabitaciones.Visible = !ReporteHabitaciones.Visible;
            if (ReporteHabitaciones.Visible)
            {
                ReporteHabitaciones.RefreshReport();
                reportePagos.Visible = false;
                ReporteClientes.Visible = false;
                reporteReservaHabitacion.Visible = false;
                reporteReservas.Visible = false;
                reporteReservaServicio.Visible = false;
                reporteServicios.Visible = false;
                reporteUsuarios.Visible = false;
            }
        }

        private void buttonbtnReporteHabitacionesReserva_Click(object sender, EventArgs e)
        {
            reporteReservaHabitacion.Visible = !reporteReservaHabitacion.Visible;
            if (reporteReservaHabitacion.Visible)
            {
                reporteReservaHabitacion.RefreshReport();
                reportePagos.Visible = false;
                ReporteClientes.Visible = false;
                ReporteHabitaciones.Visible = false;
                reporteReservas.Visible = false;
                reporteReservaServicio.Visible = false;
                reporteServicios.Visible = false;
                reporteUsuarios.Visible = false;
            }
        }

        private void btnReporteReservas_Click(object sender, EventArgs e)
        {
            reporteReservas.Visible = !reporteReservas.Visible;
            if (reporteReservas.Visible)
            {
                reporteReservas.RefreshReport();
                reportePagos.Visible = false;
                ReporteClientes.Visible = false;
                ReporteHabitaciones.Visible = false;
                reporteReservaHabitacion.Visible = false;
                reporteReservaServicio.Visible = false;
                reporteServicios.Visible = false;
                reporteUsuarios.Visible = false;
            }
        }

        private void btnServiciosReserva_Click(object sender, EventArgs e)
        {
            reporteReservaServicio.Visible = !reporteReservaServicio.Visible;
            if (reporteReservaServicio.Visible)
            {
                reporteReservaServicio.RefreshReport();
                reportePagos.Visible = false;
                ReporteClientes.Visible = false;
                ReporteHabitaciones.Visible = false;
                reporteReservaHabitacion.Visible = false;
                reporteReservas.Visible = false;
                reporteServicios.Visible = false;
                reporteUsuarios.Visible = false;
            }
        }

        private void btnReporteServicios_Click(object sender, EventArgs e)
        {
            reporteServicios.Visible = !reporteServicios.Visible;
            if (reporteServicios.Visible)
            {
                reporteServicios.RefreshReport();
                reportePagos.Visible = false;
                ReporteClientes.Visible = false;
                ReporteHabitaciones.Visible = false;
                reporteReservaHabitacion.Visible = false;
                reporteReservas.Visible = false;
                reporteReservaServicio.Visible = false;
                reporteUsuarios.Visible = false;
            }
        }

        private void btnReporteUsuarios_Click(object sender, EventArgs e)
        {
            reporteUsuarios.Visible = !reporteUsuarios.Visible;
            if (reporteUsuarios.Visible)
            {
                reporteUsuarios.RefreshReport();
                reportePagos.Visible = false;
                ReporteClientes.Visible = false;
                ReporteHabitaciones.Visible = false;
                reporteReservaHabitacion.Visible = false;
                reporteReservas.Visible = false;
                reporteReservaServicio.Visible = false;
                reporteServicios.Visible = false;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void habitacionesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var mainForm = new DashboardForm();
            mainForm.Show();
            this.Hide();
        }

        private void clientesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var mainForm = new Habitaciones();
            mainForm.Show();
            this.Hide();
        }

        private void reservasToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var mainForm = new Reservas();
            mainForm.Show();
            this.Hide();
        }

        private void serviciosPorReservaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var mainForm = new ServiciosReserva();
            mainForm.Show();
            this.Hide();
        }

        private void habitacionesPorReservaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var mainForm = new HabitacionesReserva();
            mainForm.Show();
            this.Hide();
        }

        private void serviciosToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var mainForm = new Servicios();
            mainForm.Show();
            this.Hide();
        }

        private void usuariosToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var mainForm = new Usuarios();
            mainForm.Show();
            this.Hide();
        }

        private void pagosToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var mainForm = new Pagos();
            mainForm.Show();
            this.Hide();
        }
    }
}
