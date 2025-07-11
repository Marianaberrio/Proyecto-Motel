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
            var (isAuthenticated, userRole) = AuthenticateUser(usuario, contrasena);

            if (isAuthenticated)
            {
                // Verificar si el usuario tiene el rol de "Administrador"
                if (userRole == "Administrador")
                {
                    // Redirigir al dashboard principal o pantalla correspondiente
                    var mainForm = new DashboardForm();
                    mainForm.Show();
                    this.Hide();
                }
                else
                {
                    // Mostrar mensaje de error si no tiene permisos de administrador
                    MessageBox.Show("Usted no tiene los permisos necesarios para acceder a esta aplicación.",
                                     "Acceso denegado",
                                     MessageBoxButtons.OK,
                                     MessageBoxIcon.Error);
                }
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
        // Método para autenticar al usuario en la base de datos
        private (bool, string) AuthenticateUser(string usuario, string contrasena)
        {
            // Obtener la cadena de conexión desde el archivo App.config
            string connectionString = ConfigurationManager.ConnectionStrings["MotelDbConnection"].ConnectionString;

            // Llamar al método para verificar la contraseña y obtener el rol
            return VerifyPasswordInDatabase(usuario, contrasena, connectionString);
        }

        // Verificar las credenciales en la base de datos
        // Verificar las credenciales y obtener el rol en la base de datos
        private (bool, string) VerifyPasswordInDatabase(string usuario, string contrasena, string connectionString)
        {
            // La consulta para obtener el hash de la contraseña y el rol del usuario
            string query = "SELECT Contraseña, Rol FROM Usuarios WHERE Usuario = @usuario";

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                // Establecer la conexión
                conn.Open();

                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    // Definir los parámetros para evitar inyección SQL
                    cmd.Parameters.AddWithValue("@usuario", usuario);

                    // Ejecutar la consulta y obtener el valor
                    using (var reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            string storedPasswordHash = reader.GetString(0);  // Contraseña almacenada
                            string userRole = reader.GetString(1);  // Rol del usuario

                            // Comparar la contraseña ingresada con el hash almacenado
                            if (VerifyPassword(contrasena, storedPasswordHash))
                            {
                                return (true, userRole);  // Usuario autenticado y rol obtenido
                            }
                        }
                    }
                }
            }

            return (false, string.Empty);  // Usuario no encontrado o credenciales incorrectas
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

