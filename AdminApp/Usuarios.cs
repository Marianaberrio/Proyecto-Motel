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
using Microsoft.VisualBasic;

namespace AdminApp
{
    public partial class Usuarios : Form
    {
        public Usuarios()
        {
            InitializeComponent();

            txtModificarContraseñaUsuario.PasswordChar = '*';
            txtConfirmarModificarContraseñaUsuario.PasswordChar = '*';

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

        private async Task<ClaseUsuarios> BuscarUsuarioPorID(string idUsuario)
        {
            using (var client = new HttpClient())
            {
                // Construir la URL para la búsqueda por ID
                string url = $"http://localhost:5264/api/usuarios/buscarPorId/{idUsuario}";  // URL API correcta
                var response = await client.GetAsync(url);

                if (response.IsSuccessStatusCode)
                {
                    var result = await response.Content.ReadAsStringAsync();
                    return JsonConvert.DeserializeObject<ClaseUsuarios>(result);  // Deserializar el usuario encontrado
                }

                return null;  // Si no se encuentra el usuario
            }
        }

        private async Task<ClaseUsuarios> BuscarUsuarioPorNombre(string nombreUsuario)
        {
            using (var client = new HttpClient())
            {
                // Construir la URL para la búsqueda por nombre de usuario
                string url = $"http://localhost:5264/api/usuarios/buscarPorNombre/{nombreUsuario}";  // URL API correcta
                var response = await client.GetAsync(url);

                if (response.IsSuccessStatusCode)
                {
                    var result = await response.Content.ReadAsStringAsync();
                    return JsonConvert.DeserializeObject<ClaseUsuarios>(result);  // Deserializar el usuario encontrado
                }

                return null;  // Si no se encuentra el usuario
            }
        }

        private async Task<bool> ActualizarUsuarioAsync(ClaseUsuarios usuario)
        {
            using (var client = new HttpClient())
            {
                try
                {
                    // URL de la API para modificar el usuario
                    string url = $"http://localhost:5264/api/usuarios/modificarPorId/{usuario.Id}"; // Cambia el URL según tu configuración

                    // Convertir el objeto usuario a JSON
                    var content = new StringContent(JsonConvert.SerializeObject(usuario), Encoding.UTF8, "application/json");

                    // Hacer una solicitud PUT a la API
                    var response = await client.PutAsync(url, content);

                    // Verificar si la solicitud fue exitosa
                    return response.IsSuccessStatusCode;
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error al modificar el usuario: {ex.Message}");
                    return false;
                }
            }
        }

        private async Task<bool> EliminarUsuarioAsync(string idOusuario)
        {
            using (var client = new HttpClient())
            {
                try
                {
                    // URL de la API para eliminar usuario por ID o nombre
                    string url = string.IsNullOrEmpty(idOusuario)
                        ? $"http://localhost:5264/api/usuarios/eliminarPorNombre/{idOusuario}"
                        : $"http://localhost:5264/api/usuarios/eliminarPorId/{idOusuario}";

                    // Hacer la solicitud DELETE a la API
                    var response = await client.DeleteAsync(url);

                    // Verificar si la solicitud fue exitosa
                    return response.IsSuccessStatusCode;
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error al eliminar el usuario: {ex.Message}");
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

            txtModificarContraseñaUsuario.Enabled = false;
            txtConfirmarModificarContraseñaUsuario.Enabled = false;
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

        private async void btnBuscarUser_Click(object sender, EventArgs e)
        {
            // Asegurarse de que el usuario haya introducido el ID o el nombre
            if (string.IsNullOrWhiteSpace(txtBuscarIDUsuario.Text) && string.IsNullOrWhiteSpace(txtBuscarNombreUsuario.Text))
            {
                MessageBox.Show("Por favor, ingrese un ID o un nombre de usuario.");
                return;
            }

            ClaseUsuarios usuario = null;

            // Buscar por ID
            if (btnBuscarIDUsuario.Checked)
            {
                string idUsuario = txtBuscarIDUsuario.Text;
                usuario = await BuscarUsuarioPorID(idUsuario);  // Llamada a la API para buscar por ID
            }
            // Buscar por Nombre de Usuario
            else if (btnBuscarNombreUsuario.Checked)
            {
                string nombreUsuario = txtBuscarNombreUsuario.Text;
                usuario = await BuscarUsuarioPorNombre(nombreUsuario);  // Llamada a la API para buscar por Nombre
            }

            // Si no encontramos el usuario, mostrar mensaje
            if (usuario == null)
            {
                MessageBox.Show("No se encontró el usuario.");
                return;
            }

            // Mostrar los datos del usuario en los campos correspondientes
            txtBuscarNombreUser.Text = usuario.Usuario;
            txtContraseñaUser.Text = usuario.Contraseña;
            txtBuscarRolUser.Text = usuario.Rol;
        }

        private void btnSalirBuscarUsuario_Click(object sender, EventArgs e)
        {
            // Limpiar todos los campos de búsqueda
            txtBuscarIDUsuario.Clear();
            txtBuscarNombreUsuario.Clear();
            txtBuscarNombreUser.Clear();
            txtContraseñaUser.Clear();
            txtBuscarRolUser.Clear();

            // Cerrar el GroupBox
            gbBuscarUsuario.Visible = false;
        }

        private void btnBuscarIDUsuario_CheckedChanged(object sender, EventArgs e)
        {
            if (btnBuscarIDUsuario.Checked)
            {
                txtBuscarNombreUsuario.Enabled = false;  // Deshabilitar campo de nombre
                txtBuscarIDUsuario.Enabled = true;  // Habilitar campo de ID
            }
        }

        private void btnBuscarNombreUsuario_CheckedChanged(object sender, EventArgs e)
        {
            if (btnBuscarNombreUsuario.Checked)
            {
                txtBuscarIDUsuario.Enabled = false;  // Deshabilitar campo de ID
                txtBuscarNombreUsuario.Enabled = true;  // Habilitar campo de nombre
            }
        }

        private void btnModificarIDUsuario_CheckedChanged(object sender, EventArgs e)
        {
            if (btnModificarIDUsuario.Checked)
            {
                txtModificarNombreUser.Enabled = false;  // Deshabilitar campo de nombre
                txtModificarIdUsuario.Enabled = true;   // Habilitar campo de ID
            }
        }

        private void btnModificarNombreUsuario_CheckedChanged(object sender, EventArgs e)
        {
            if (btnModificarNombreUsuario.Checked)
            {
                txtModificarIdUsuario.Enabled = false;  // Deshabilitar campo de ID
                txtModificarNombreUser.Enabled = true;  // Habilitar campo de nombre
            }
        }

        private async void btnBuscarModificarUsuario_Click(object sender, EventArgs e)
        {
            // Asegurarse de que al menos uno de los campos esté lleno (ID o Nombre)
            if (string.IsNullOrWhiteSpace(txtModificarIdUsuario.Text) && string.IsNullOrWhiteSpace(txtModificarNombreUser.Text))
            {
                MessageBox.Show("Por favor, ingrese un ID o un nombre de usuario.");
                return;
            }

            ClaseUsuarios usuario = null;

            // Buscar por ID
            if (btnModificarIDUsuario.Checked)
            {
                string idUsuario = txtModificarIdUsuario.Text;
                usuario = await BuscarUsuarioPorID(idUsuario);  // Llamada a la API para buscar por ID
            }
            // Buscar por Nombre de Usuario
            else if (btnModificarNombreUsuario.Checked)
            {
                string nombreUsuario = txtModificarNombreUser.Text;
                usuario = await BuscarUsuarioPorNombre(nombreUsuario);  // Llamada a la API para buscar por Nombre
            }

            // Si no encontramos el usuario, mostrar mensaje
            if (usuario == null)
            {
                MessageBox.Show("No se encontró el usuario.");
                return;
            }

            // Mostrar los datos del usuario en los campos correspondientes
            txtModificarNombreUsuario.Text = usuario.Usuario;
            txtModificarContraseñaUsuario.Text = usuario.Contraseña; // Esta contraseña se enmascara con *
            txtModificarRolUsuario.Text = usuario.Rol;

        }

        private void btnCambiarContraseña_Click(object sender, EventArgs e)
        {
            // Mostrar InputBox para que el usuario ingrese la contraseña actual
            string contraseñaIngresada = Interaction.InputBox("Ingrese la contraseña actual:", "Confirmar Contraseña", "");

            // Verificar si la contraseña ingresada coincide con la actual
            if (contraseñaIngresada == txtModificarContraseñaUsuario.Text)
            {
                // Si coincide, habilitar los campos de contraseña para que el usuario pueda cambiarlas
                txtModificarContraseñaUsuario.Enabled = true;
                txtConfirmarModificarContraseñaUsuario.Enabled = true;
            }
            else
            {
                MessageBox.Show("La contraseña ingresada no es correcta.");
            }
        }

        private async void btnModificarUser_Click(object sender, EventArgs e)
        {
            // Verificar si las contraseñas coinciden
            if (txtModificarContraseñaUsuario.Text != txtConfirmarModificarContraseñaUsuario.Text)
            {
                MessageBox.Show("Las contraseñas no coinciden. Por favor, intente de nuevo.");
                return; // Salir de la función si las contraseñas no coinciden
            }

            // Crear un objeto de usuario con los nuevos valores
            var usuarioModificado = new ClaseUsuarios
            {
                // Si tienes un ID del usuario ya cargado en algún campo, lo puedes tomar directamente
                Id = int.Parse(txtModificarIdUsuario.Text), // Asegúrate de que el ID esté presente
                Usuario = txtModificarNombreUsuario.Text,
                Contraseña = txtModificarContraseñaUsuario.Text,
                Rol = txtModificarRolUsuario.Text
            };

            // Llamar a la API para actualizar el usuario
            var result = await ActualizarUsuarioAsync(usuarioModificado);

            // Si la actualización fue exitosa
            if (result)
            {
                MessageBox.Show("El usuario se actualizó correctamente.");

                // Actualizar el DataGridView
                this.usuariosTableAdapter.Fill(this.dbMotelUsuariosDataSet1.Usuarios);

                // Limpiar los campos
                txtModificarIdUsuario.Clear();
                txtModificarNombreUsuario.Clear();
                txtModificarContraseñaUsuario.Clear();
                txtModificarRolUsuario.Clear();
                txtConfirmarModificarContraseñaUsuario.Clear();
                txtModificarNombreUser.Clear();

                // Cerrar el GroupBox
                gbModificarUsuario.Visible = false;
            }
            else
            {
                MessageBox.Show("Hubo un error al actualizar el usuario. Intente nuevamente.");
            }
        }

        private void btnCancelarModificar_Click(object sender, EventArgs e)
        {
            // Limpiar los campos después de la modificación
            txtModificarIdUsuario.Clear();
            txtModificarNombreUsuario.Clear();
            txtModificarContraseñaUsuario.Clear();
            txtModificarRolUsuario.Clear();
            txtConfirmarModificarContraseñaUsuario.Clear();
            txtModificarNombreUser.Clear();

            gbModificarUsuario.Visible = false;

        }

        private void btnEliminarIdUsuario_CheckedChanged(object sender, EventArgs e)
        {
            if (btnEliminarIdUsuario.Checked)
            {
                txtEliminarNombreUsuario.Enabled = false;  // Deshabilitar el campo para nombre
                txtEliminarIDUsuario.Enabled = true;       // Habilitar el campo para ID
            }
        }

        private void btnEliminarNombreUsuario_CheckedChanged(object sender, EventArgs e)
        {
            if (btnEliminarNombreUsuario.Checked)
            {
                txtEliminarIDUsuario.Enabled = false;     // Deshabilitar el campo para ID
                txtEliminarNombreUsuario.Enabled = true;  // Habilitar el campo para nombre de usuario
            }
        }

        private async void btnEliminar_Click(object sender, EventArgs e)
        {
            // Verificar que al menos uno de los campos esté lleno
            if (string.IsNullOrWhiteSpace(txtEliminarIDUsuario.Text) && string.IsNullOrWhiteSpace(txtEliminarNombreUsuario.Text))
            {
                MessageBox.Show("Por favor, ingrese un ID de usuario o un nombre de usuario.");
                return;
            }

            string idUsuario = null;
            string nombreUsuario = null;

            // Si se seleccionó eliminar por ID
            if (btnEliminarIdUsuario.Checked)
            {
                idUsuario = txtEliminarIDUsuario.Text;
            }
            // Si se seleccionó eliminar por Nombre
            else if (btnEliminarNombreUsuario.Checked)
            {
                nombreUsuario = txtEliminarNombreUsuario.Text;
            }

            // Mostrar un mensaje de confirmación
            var confirmResult = MessageBox.Show($"¿Está seguro de que desea eliminar el usuario {idUsuario ?? nombreUsuario}?",
                                                "Confirmar eliminación",
                                                MessageBoxButtons.YesNo);

            // Si el usuario confirma
            if (confirmResult == DialogResult.Yes)
            {
                bool result = false;

                // Si eliminamos por ID
                if (!string.IsNullOrEmpty(idUsuario))
                {
                    result = await EliminarUsuarioAsync(idUsuario);
                }
                // Si eliminamos por nombre de usuario
                else if (!string.IsNullOrEmpty(nombreUsuario))
                {
                    result = await EliminarUsuarioAsync(nombreUsuario);
                }

                // Si la eliminación fue exitosa
                if (result)
                {
                    MessageBox.Show("El usuario fue eliminado exitosamente.");
                    // Limpiar los campos
                    txtEliminarIDUsuario.Clear();
                    txtEliminarNombreUsuario.Clear();
                    btnEliminarIdUsuario.Checked = false;
                    btnEliminarNombreUsuario.Checked = false;

                    // Actualizar el DataGridView
                    this.usuariosTableAdapter.Fill(this.dbMotelUsuariosDataSet1.Usuarios);

                    // Cerrar el GroupBox
                    gbEliminarUsuario.Visible = false;
                }
                else
                {
                    MessageBox.Show("Hubo un error al eliminar el usuario. Inténtalo de nuevo.");
                }
            }
        }

        private void btnCancelarEliminar_Click(object sender, EventArgs e)
        {
            txtEliminarIDUsuario.Clear();
            txtEliminarNombreUsuario.Clear();
            btnEliminarIdUsuario.Checked = false;
            btnEliminarNombreUsuario.Checked = false;

            gbEliminarUsuario.Visible = false;

        }

        private void menuprincipalToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var mainForm = new DashboardForm();
            mainForm.Show();
            this.Hide();
        }

        private void habitacionesToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            var mainForm = new Habitaciones();
            mainForm.Show();
            this.Hide();
        }

        private void clientesToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            var mainForm = new Clientes();
            mainForm.Show();
            this.Hide();
        }

        private void reservasToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            var mainForm = new Reservas();
            mainForm.Show();
            this.Hide();
        }

        private void serviciosToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            var mainForm = new Servicios();
            mainForm.Show();
            this.Hide();
        }

        private void pagosToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            var mainForm = new Pagos();
            mainForm.Show();
            this.Hide();
        }

        private void reportesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var mainForm = new Reportes();
            mainForm.Show();
            this.Hide();
        }

        private void habitacionesPorReservaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var mainForm = new HabitacionesReserva();
            mainForm.Show();
            this.Hide();
        }

        private void serviciosPorReservaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var mainForm = new ServiciosReserva();
            mainForm.Show();
            this.Hide();
        }
    }
}
