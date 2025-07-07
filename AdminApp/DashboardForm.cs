using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AdminApp
{
    public partial class DashboardForm : Form
    {
        public DashboardForm()
        {
            InitializeComponent();
            lblHabitaciones.Text = $"Habitaciones disponibles: {12}";
            lblReservas.Text = $"Reservas activas: {3}";
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
    }
}
