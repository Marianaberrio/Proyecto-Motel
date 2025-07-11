using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AdminApp
{
    public partial class DashboardForm : Form
    {
        private string _connectionString = ConfigurationManager.ConnectionStrings["MotelDbConnection"].ConnectionString;

        public DashboardForm()
        {
            InitializeComponent();
            LoadDashboardData();
        }

        // Método para cargar las cantidades de habitaciones y reservas
        private void LoadDashboardData()
        {
            try
            {
                // Obtener el número de habitaciones disponibles
                int availableRooms = GetAvailableRooms();
                lblHabitaciones.Text = $"Habitaciones disponibles: {availableRooms}";

                // Obtener el número de reservas activas
                int activeReservations = GetActiveReservations();
                lblReservas.Text = $"Reservas activas: {activeReservations}";
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al cargar los datos del dashboard: {ex.Message}",
                                "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // Método para obtener la cantidad de habitaciones disponibles
        private int GetAvailableRooms()
        {
            int availableRooms = 0;

            string query = "SELECT COUNT(*) FROM Habitaciones WHERE EstadoHabitacion = 'Disponible'";  // Asegúrate de que el nombre de la columna y el estado coincidan con tu base de datos

            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                conn.Open();
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    availableRooms = (int)cmd.ExecuteScalar();  // Ejecuta la consulta y obtiene el resultado
                }
            }

            return availableRooms;
        }

        // Método para obtener la cantidad de reservas activas
        private int GetActiveReservations()
        {
            int activeReservations = 0;

            string query = "SELECT COUNT(*) FROM Reservas WHERE EstadoReserva = 'Activa'";  // Asegúrate de que el nombre de la columna y el estado coincidan con tu base de datos

            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                conn.Open();
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    activeReservations = (int)cmd.ExecuteScalar();  // Ejecuta la consulta y obtiene el resultado
                }
            }

            return activeReservations;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            // Redirigir al dashboard principal o pantalla correspondiente
            var mainForm = new Habitaciones();
            mainForm.Show();
            this.Hide();
        }

        private void btnReservas_Click(object sender, EventArgs e)
        {
            var mainForm = new Reservas();
            mainForm.Show();
            this.Hide();
        }

        private void habitacionesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // Redirigir al dashboard de habitaciones
            var mainForm = new Habitaciones();
            mainForm.Show();
            this.Hide();
        }

        private void clientesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // Redirigir al dashboard de clientes
            var mainForm = new Clientes();
            mainForm.Show();
            this.Hide();
        }

        private void reservasToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // Redirigir al dashboard de reservas
            var mainForm = new Reservas();
            mainForm.Show();
            this.Hide();
        }

        private void serviciosToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // Redirigir al dashboard de servicios
            var mainForm = new Servicios();
            mainForm.Show();
            this.Hide();
        }

        private void usuariosToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // Redirigir al dashboard de usuarios
            var mainForm = new Usuarios();
            mainForm.Show();
            this.Hide();
        }

        private void pagosToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // Redirigir al dashboard de pagos
            var mainForm = new Pagos();
            mainForm.Show();
            this.Hide();
        }

        private void reportesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // Redirigir al dashboard de reportes
            var mainForm = new Reportes();
            mainForm.Show();
            this.Hide();
        }

        private void CerrarApp_Click(object sender, EventArgs e)
        {
            this.Close();
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
    }
}
