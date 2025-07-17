using System;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace caja3
{
    public partial class Form1 : Form
    {
        private readonly HttpClient _httpClient;

        public Form1()
        {
            InitializeComponent();

            // Instanciamos HttpClient con la URL base de la API de integración
            _httpClient = new HttpClient
            {
                BaseAddress = new Uri("https://localhost:7013/api/") // Reemplaza con tu puerto correcto
            };
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        public class Usuario
        {
            public int Id { get; set; }
            public string UsuarioNombre { get; set; } = string.Empty;
            public string Contraseña { get; set; } = string.Empty;
        }

        private async void ingresarBtn_Click(object sender, EventArgs e)
        {
            string usuario = usuariotxt.Text.Trim();
            string contrasena = contrasenatxt.Text.Trim();

            if (string.IsNullOrEmpty(usuario) || string.IsNullOrEmpty(contrasena))
            {
                MessageBox.Show("Por favor, ingrese ambos campos (usuario y contraseña).",
                                 "Campos incompletos",
                                 MessageBoxButtons.OK,
                                 MessageBoxIcon.Warning);
                return;
            }

            bool autenticado = await AuthenticateUserAsync(usuario, contrasena);

            if (autenticado)
            {
                var mainForm = new Facturar();
                mainForm.Show();
                this.Hide();
            }
            else
            {
                MessageBox.Show("Usuario o contraseña incorrectos.",
                             "Error de autenticación",
                             MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        // Llamada a la API de integración para autenticar
        private async Task<bool> AuthenticateUserAsync(string usuario, string contrasena)
        {
            try
            {
                // Realizar GET a la API de integración
                var response = await _httpClient.GetAsync($"usuarios/buscarPorNombre/{usuario}");

                if (!response.IsSuccessStatusCode)
                    return false;

                var user = await response.Content.ReadFromJsonAsync<Usuario>();

                if (user == null)
                    return false;

                // Comparar contraseñas
                return VerifyPassword(contrasena, user.Contraseña);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error de conexión con la API: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
        }

        private bool VerifyPassword(string enteredPassword, string storedPassword)
        {
            return enteredPassword.Trim().Equals(storedPassword.Trim(), StringComparison.OrdinalIgnoreCase);
        }
    }
}

