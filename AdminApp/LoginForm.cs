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
using static System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel;

namespace AdminApp
{
    public partial class LoginForm : Form
    {
        public LoginForm()
        {
            InitializeComponent();
            txtContrasena.PasswordChar = '*';
        }

        private void btnIngresar_Click(object sender, EventArgs e)
        {
            string usuario = txtUsuario.Text.Trim();  // Eliminar espacios innecesarios
            string contrasena = txtContrasena.Text.Trim();  // Eliminar espacios innecesarios

            // Verificar si los campos están vacíos antes de continuar
            if (string.IsNullOrEmpty(usuario) || string.IsNullOrEmpty(contrasena))
            {
                // Mostrar el mensaje de advertencia si alguno de los campos está vacío
                MessageBox.Show("Por favor, ingrese ambos campos (usuario y contraseña).",
                                 "Campos incompletos",
                                 MessageBoxButtons.OK,
                                 MessageBoxIcon.Warning);
                return; // Detener la ejecución del código y no proceder a la autenticación
            }

            // Si ambos campos están llenos, proceder con la autenticación
            if (AuthenticateUser(usuario, contrasena))
            {
                // Redirigir al dashboard principal o pantalla correspondiente
                var mainForm = new DashboardForm();
                mainForm.Show();
                this.Hide();
            }
            else
            {
                // Si las credenciales son incorrectas
                MessageBox.Show("Usuario o contraseña incorrectos.",
                                 "Error de autenticación",
                                 MessageBoxButtons.OK,
                                 MessageBoxIcon.Error);
            }
        }

        // Método para autenticar al usuario en la base de datos
        private bool AuthenticateUser(string usuario, string contrasena)
        {
            // Obtener la cadena de conexión desde el archivo App.config
            string connectionString = ConfigurationManager.ConnectionStrings["MotelDbConnection"].ConnectionString;

            // Llamar al método para verificar la contraseña
            return VerifyPasswordInDatabase(usuario, contrasena, connectionString);
        }

        // Verificar las credenciales en la base de datos
        private bool VerifyPasswordInDatabase(string usuario, string contrasena, string connectionString)
        {
            // La consulta para obtener el hash de la contraseña para el usuario
            string query = "SELECT Contraseña FROM Usuarios WHERE Usuario = @usuario";

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                // Establecer la conexión
                conn.Open();

                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    // Definir los parámetros para evitar inyección SQL
                    cmd.Parameters.AddWithValue("@usuario", usuario);

                    // Ejecutar la consulta y obtener el valor
                    object result = cmd.ExecuteScalar();

                    // Si no se encuentra el usuario, retornar falso
                    if (result == null)
                    {
                        return false;
                    }

                    // Obtener el hash de la contraseña almacenado en la base de datos
                    string storedPasswordHash = result.ToString();

                    // Comparar el hash de la contraseña ingresada con el hash almacenado
                    return VerifyPassword(contrasena, storedPasswordHash);
                }
            }
        }

        // Comparar la contraseña ingresada con la almacenada
        private bool VerifyPassword(string enteredPassword, string storedPasswordHash)
        {
            // Eliminar los espacios en blanco antes de comparar
            enteredPassword = enteredPassword.Trim();
            storedPasswordHash = storedPasswordHash.Trim();

            // Comparar las contraseñas directamente (sin importar mayúsculas/minúsculas)
            return enteredPassword.Equals(storedPasswordHash, StringComparison.OrdinalIgnoreCase);
        }

    }
}

