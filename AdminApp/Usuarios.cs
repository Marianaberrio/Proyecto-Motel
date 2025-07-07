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

namespace AdminApp
{
    public partial class Usuarios : Form
    {
        public Usuarios()
        {
            InitializeComponent();
        }

        private async Task<bool> AgregarUsuarioAsync(ClaseUsuarios usuario)
        {
            using (var client = new HttpClient())
            {
                try
                {
                    // URL de la API que maneja la creación de usuario
                    string url = "http://localhost:5264/api/usuarios/agregar";  // Cambiar esta URL si es necesario

                    // Convertir el objeto de usuario a JSON
                    var content = new StringContent(JsonConvert.SerializeObject(usuario), Encoding.UTF8, "application/json");

                    // Realizar la solicitud POST para agregar el usuario
                    var response = await client.PostAsync(url, content);

                    // Verificar si la respuesta fue exitosa (código 2xx)
                    return response.IsSuccessStatusCode;
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error al agregar el usuario: {ex.Message}");
                    return false;
                }
            }
        }
        private void habitacionesToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void clientesToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void reservasToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void serviciosToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void usuariosToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void pagosToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void Usuarios_Load(object sender, EventArgs e)
        {
            // TODO: This line of code loads data into the 'dbMotelUsuariosDataSet1.Usuarios' table. You can move, or remove it, as needed.
            this.usuariosTableAdapter.Fill(this.dbMotelUsuariosDataSet1.Usuarios);

        }

        private void btnAgregarUsuario_Click(object sender, EventArgs e)
        {
            gbAgregarUsuario.Visible = true;

            string contrasenaGenerada = GenerarContraseñaAleatoria();
            txtAgregarContraseña.Text = contrasenaGenerada;
            txtAgregarConfirmarContrasena.Text = contrasenaGenerada;
        }

        private string GenerarContraseñaAleatoria()
        {
            Random random = new Random();
            const string letras = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ";
            const string numeros = "0123456789";
            StringBuilder contrasena = new StringBuilder();

            // Generar 4 números aleatorios
            for (int i = 0; i < 4; i++)
            {
                contrasena.Append(numeros[random.Next(numeros.Length)]);
            }

            // Generar 8 letras aleatorias
            for (int i = 0; i < 8; i++)
            {
                contrasena.Append(letras[random.Next(letras.Length)]);
            }

            return contrasena.ToString();
        }

        private void btnBuscarUsuario_Click(object sender, EventArgs e)
        {
            gbBuscarUsuario.Visible = true;
        }

        private void btnModificarUsuario_Click(object sender, EventArgs e)
        {
            gbModificarUsuario.Visible = true;
        }

        private void btnEliminarUsuario_Click(object sender, EventArgs e)
        {
            gbEliminarUsuario.Visible = true;
        }

        private void CerrarApp_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private async void btnAgregar_Click(object sender, EventArgs e)
        {
            // Verificar que todos los campos estén completos
            if (string.IsNullOrWhiteSpace(txtAgregarNombreUsuario.Text) ||
                string.IsNullOrWhiteSpace(txtAgregarContraseña.Text) ||
                string.IsNullOrWhiteSpace(txtAgregarConfirmarContrasena.Text) ||
                string.IsNullOrWhiteSpace(txtAgregarRolUsuario.Text))
            {
                MessageBox.Show("Por favor, complete todos los campos.");
                return;
            }

            // Verificar que las contraseñas coincidan
            if (txtAgregarContraseña.Text != txtAgregarConfirmarContrasena.Text)
            {
                MessageBox.Show("Las contraseñas no coinciden. Por favor, verifícalas.");
                return;
            }

            // Crear un objeto de ClaseUsuarios con los datos ingresados
            var nuevoUsuario = new ClaseUsuarios
            {
                Usuario = txtAgregarNombreUsuario.Text,
                Contraseña = txtAgregarContraseña.Text, // La contraseña será la ingresada
                Rol = txtAgregarRolUsuario.Text
            };

            // Llamar a la función para agregar el usuario
            bool resultado = await AgregarUsuarioAsync(nuevoUsuario);

            // Si la creación fue exitosa, mostrar un mensaje, actualizar la vista y limpiar los campos
            if (resultado)
            {
                MessageBox.Show("El usuario fue creado exitosamente.");

                // Actualizar el DataGridView
                this.usuariosTableAdapter.Fill(this.dbMotelUsuariosDataSet1.Usuarios);

                // Limpiar los campos
                // Limpiar los campos de texto
                txtAgregarNombreUsuario.Clear();
                txtAgregarContraseña.Clear();
                txtAgregarConfirmarContrasena.Clear();
                txtAgregarRolUsuario.Clear();

                // Cerrar el GroupBox
                gbAgregarUsuario.Visible = false;
            }
            else
            {
                MessageBox.Show("Hubo un error al crear el usuario.");
            }
        }

        private void btnCancelarAgregar_Click(object sender, EventArgs e)
        {
            // Limpiar los campos de texto
            txtAgregarNombreUsuario.Clear();
            txtAgregarContraseña.Clear();
            txtAgregarConfirmarContrasena.Clear();
            txtAgregarRolUsuario.Clear();

            gbAgregarUsuario.Visible = false;
        }
    }
}
