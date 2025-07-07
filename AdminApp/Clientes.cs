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
    public partial class Clientes : Form
    {

        public Clientes()
        {
            InitializeComponent();
        }

        public async Task<bool> AgregarClienteAsync(ClaseClientes cliente)
        {
            using (var client = new HttpClient())
            {
                try
                {
                    // URL de la API para agregar un cliente (ajustar según tu configuración)
                    string url = "http://localhost:5264/api/clientes"; // Cambia la URL a la que tienes configurada

                    // Convertir el objeto Cliente a JSON
                    var content = new StringContent(JsonConvert.SerializeObject(cliente), Encoding.UTF8, "application/json");

                    // Hacer una solicitud POST a la API
                    var response = await client.PostAsync(url, content);

                    // Verificar si la solicitud fue exitosa (código 2xx)
                    if (response.IsSuccessStatusCode)
                    {
                        return true;
                    }
                    else
                    {
                        // Obtener el contenido de la respuesta en caso de error
                        var errorContent = await response.Content.ReadAsStringAsync();
                        MessageBox.Show($"Error al agregar el cliente. Código: {response.StatusCode}, Detalle: {errorContent}");
                        return false;
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error: {ex.Message}");
                    return false;
                }
            }
        }

        public async Task<ClaseClientes> BuscarClientePorID(int idCliente)
        {
            using (var client = new HttpClient())
            {
                try
                {
                    // URL de la API para buscar un cliente por ID
                    string url = $"http://localhost:5264/api/clientes/{idCliente}"; // Cambiar la URL si es necesario

                    // Hacer la solicitud GET a la API
                    var response = await client.GetAsync(url);

                    // Si la respuesta es exitosa, devolver el cliente encontrado
                    if (response.IsSuccessStatusCode)
                    {
                        var resultado = await response.Content.ReadAsStringAsync();
                        return JsonConvert.DeserializeObject<ClaseClientes>(resultado);  // Deserializa el cliente desde la respuesta
                    }
                    else
                    {
                        return null;  // Si no se encuentra, devolver null
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error en la solicitud HTTP: {ex.Message}");
                    return null;  // Devolver null si hubo un error
                }
            }
        }

        public async Task<bool> ModificarClienteAsync(ClaseClientes cliente)
        {
            using (var client = new HttpClient())
            {
                try
                {
                    string url = $"http://localhost:5264/api/clientes/{cliente.NumCliente}"; // URL de la API para actualizar el cliente

                    var content = new StringContent(JsonConvert.SerializeObject(cliente), Encoding.UTF8, "application/json");

                    var response = await client.PutAsync(url, content);

                    // Verificar si la solicitud fue exitosa (código 2xx)
                    if (response.IsSuccessStatusCode)
                    {
                        return true;
                    }
                    else
                    {
                        var errorContent = await response.Content.ReadAsStringAsync();
                        MessageBox.Show($"Error al modificar el cliente. Código: {response.StatusCode}, Detalle: {errorContent}");
                        return false;
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error: {ex.Message}");
                    return false;
                }
            }
        }

        public async Task<bool> EliminarClienteAsync(int idCliente)
        {
            using (var client = new HttpClient())
            {
                try
                {
                    // URL de la API para eliminar un cliente
                    string url = $"http://localhost:5264/api/clientes/{idCliente}"; // Cambiar la URL a la que tienes configurada

                    // Hacer una solicitud DELETE a la API
                    var response = await client.DeleteAsync(url);

                    // Verificar si la solicitud fue exitosa (código 2xx)
                    if (response.IsSuccessStatusCode)
                    {
                        return true;
                    }
                    else
                    {
                        var errorContent = await response.Content.ReadAsStringAsync();
                        MessageBox.Show($"Error al eliminar el cliente. Código: {response.StatusCode}, Detalle: {errorContent}");
                        return false;
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error: {ex.Message}");
                    return false;
                }
            }
        }

        private void Clientes_Load(object sender, EventArgs e)
        {
            // TODO: This line of code loads data into the 'dbMotelDataSet.Clientes' table. You can move, or remove it, as needed.
            this.clientesTableAdapter.Fill(this.dbMotelDataSet.Clientes);

        }

        private void btnAgregarCliente_Click(object sender, EventArgs e)
        {
            gbAgregarCliente.Visible = true;
        }

        private void btnBuscarCliente_Click(object sender, EventArgs e)
        {
            gbBuscarCliente.Visible = true;
        }

        private void btnModificarCliente_Click(object sender, EventArgs e)
        {
            gbModificarCliente.Visible = true;
        }

        private void btnEliminarCliente_Click(object sender, EventArgs e)
        {
            gbEliminarCliente.Visible = true;
        }

        private void CerrarApp_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private async void btnAgregar_Click(object sender, EventArgs e)
        {
            // Verificar que los campos no estén vacíos
            if (string.IsNullOrWhiteSpace(txtAgregarNombreCliente.Text) ||
                string.IsNullOrWhiteSpace(txtAgregarApellidosCliente.Text) ||
                string.IsNullOrWhiteSpace(txtAgregarCorreoCliente.Text) ||
                string.IsNullOrWhiteSpace(txtAgregarTelefonoCliente.Text) ||
                mcFechaNacCliente.SelectionStart == null)  // Verificar que la fecha no esté vacía
            {
                MessageBox.Show("Por favor, llene todos los campos.");
                return;
            }

            // Crear el objeto Cliente con los valores de los campos
            var cliente = new ClaseClientes
            {
                NombreCliente = txtAgregarNombreCliente.Text,
                ApellidoCliente = txtAgregarApellidosCliente.Text,
                CorreoCliente = txtAgregarCorreoCliente.Text,
                TelefonoCliente = txtAgregarTelefonoCliente.Text,
                FechaNacimiento = mcFechaNacCliente.SelectionStart,  // Obtener la fecha seleccionada
                FechaRegistro = DateTime.Now  // Obtener la fecha y hora actuales
            };

            // Llamar al método para agregar el cliente a la base de datos a través de la API
            bool resultado = await AgregarClienteAsync(cliente);

            if (resultado)
            {
                // Mensaje de éxito
                MessageBox.Show("Cliente agregado con éxito.");

                // Limpiar los campos
                txtAgregarNombreCliente.Clear();
                txtAgregarApellidosCliente.Clear();
                txtAgregarCorreoCliente.Clear();
                txtAgregarTelefonoCliente.Clear();
                mcFechaNacCliente.SetDate(DateTime.Now);

                // Cerrar el GroupBox
                gbAgregarCliente.Visible = false;

                // Actualizar el DataGridView (aquí lo puedes refrescar según el método que estés usando)
                this.clientesTableAdapter.Fill(this.dbMotelDataSet.Clientes);
            }
            else
            {
                MessageBox.Show("Hubo un error al agregar el cliente.");
            }
        }

        private void btnCancelarAgregar_Click(object sender, EventArgs e)
        {
            txtAgregarNombreCliente.Clear();
            txtAgregarApellidosCliente.Clear();
            txtAgregarCorreoCliente.Clear();
            txtAgregarTelefonoCliente.Clear();
            mcFechaNacCliente.SetDate(DateTime.Now);

            gbAgregarCliente.Visible = false;
        }

        private async void btnBuscarIDCLiente_Click(object sender, EventArgs e)
        {
            // Obtener el ID del cliente ingresado
            if (!int.TryParse(txtBuscarIDCliente.Text, out int clienteId) || clienteId <= 0) // Verificar que el ID sea un número válido
            {
                MessageBox.Show("Por favor ingrese un ID de cliente válido.");
                return;
            }

            // Buscar el cliente por ID
            var cliente = await BuscarClientePorID(clienteId);

            // Si el cliente no se encuentra
            if (cliente == null)
            {
                MessageBox.Show("Cliente no encontrado.");
                return;
            }

            // Si el cliente se encuentra, mostrar los datos en los campos correspondientes
            txtBuscarNombreCliente.Text = cliente.NombreCliente;
            txtBuscarApellidosCliente.Text = cliente.ApellidoCliente;
            txtBuscarCorreoCliente.Text = cliente.CorreoCliente;
            txtBuscarTelefonoCliente.Text = cliente.TelefonoCliente;
            txtBuscarFechaNacCliente.Text = cliente.FechaNacimiento.ToString("dd/MM/yyyy");  // Formato de fecha
            txtBuscarFechaRegistroCliente.Text = cliente.FechaRegistro.ToString("dd/MM/yyyy");  // Formato de fecha
        }

        private void btnSalirBuscarCliente_Click(object sender, EventArgs e)
        {
            txtBuscarIDCliente.Clear();
            txtBuscarNombreCliente.Clear();
            txtBuscarApellidosCliente.Clear();
            txtBuscarCorreoCliente.Clear();
            txtBuscarTelefonoCliente.Clear();
            txtBuscarFechaNacCliente.Clear();
            txtBuscarFechaRegistroCliente.Clear();

            gbBuscarCliente.Visible = false;
        }

        private async void btnModificarClient_Click(object sender, EventArgs e)
        {
            // Confirmar con el usuario antes de modificar
            var confirmResult = MessageBox.Show($"¿Está seguro de que quiere hacerle los cambios al cliente {txtBuscarModificarCliente.Text}?",
                                                "Confirmar modificación",
                                                MessageBoxButtons.YesNo);
            if (confirmResult == DialogResult.No)
            {
                return;  // Si el usuario selecciona No, no hacemos nada
            }

            // Crear un objeto Cliente con los nuevos valores
            var clienteModificado = new ClaseClientes
            {
                NumCliente = int.Parse(txtBuscarModificarCliente.Text),  // ID del cliente
                NombreCliente = txtModificarNombrescliente.Text,
                ApellidoCliente = txtModificarApellidosCliente.Text,
                CorreoCliente = txtModificarCorreoCliente.Text,
                TelefonoCliente = txtModificarTelefonoCliente.Text,
                FechaNacimiento = DateTime.Parse(txtModificarFechaNacCliente.Text),  // Convertir la fecha de nacimiento
                FechaRegistro = DateTime.Parse(txtFechaRegistroCliente.Text)  // Convertir la fecha de registro
            };

            // Llamar al método para modificar el cliente en la base de datos a través de la API
            bool resultado = await ModificarClienteAsync(clienteModificado);

            if (resultado)
            {
                // Actualizar el DataGridView (aquí lo puedes refrescar según el método que estés usando)
                gbModificarCliente.Visible = false;


                // Mostrar mensaje de éxito
                MessageBox.Show("Cliente actualizado correctamente.");

                txtBuscarModificarCliente.Clear();
                txtModificarNombrescliente.Clear();
                txtModificarApellidosCliente.Clear();
                txtModificarCorreoCliente.Clear();
                txtModificarTelefonoCliente.Clear();
                txtModificarFechaNacCliente.Clear();
                txtFechaRegistroCliente.Clear();

                this.clientesTableAdapter.Fill(this.dbMotelDataSet.Clientes);
                // Cerrar el GroupBox de modificación
                gbModificarCliente.Visible = false;
            }
            else
            {
                MessageBox.Show("Error al actualizar el cliente.");
            }
        }

        private async void btnbuscarModificarCliente_Click(object sender, EventArgs e)
        {
            string idClienteText = txtBuscarModificarCliente.Text.Trim();  // Obtener el ID ingresado

            // Validar que el ID sea un número válido
            if (!int.TryParse(idClienteText, out int idCliente) || idCliente <= 0)
            {
                MessageBox.Show("Por favor ingrese un ID de cliente válido.");
                return;
            }

            // Buscar el cliente por ID
            var cliente = await BuscarClientePorID(idCliente);

            // Si el cliente no se encuentra
            if (cliente == null)
            {
                MessageBox.Show("Cliente no encontrado.");
                return;
            }

            // Si el cliente se encuentra, mostrar los datos en los campos correspondientes
            txtModificarNombrescliente.Text = cliente.NombreCliente;
            txtModificarApellidosCliente.Text = cliente.ApellidoCliente;
            txtModificarCorreoCliente.Text = cliente.CorreoCliente;
            txtModificarTelefonoCliente.Text = cliente.TelefonoCliente;
            txtModificarFechaNacCliente.Text = cliente.FechaNacimiento.ToString("dd/MM/yyyy");  // Formato de fecha
            txtFechaRegistroCliente.Text = cliente.FechaRegistro.ToString("dd/MM/yyyy");
        }

        private void btnCancelarModificar_Click(object sender, EventArgs e)
        {
            txtBuscarModificarCliente.Clear();
            txtModificarNombrescliente.Clear();
            txtModificarApellidosCliente.Clear();
            txtModificarCorreoCliente.Clear();
            txtModificarTelefonoCliente.Clear();
            txtModificarFechaNacCliente.Clear();
            txtFechaRegistroCliente.Clear();

            gbModificarCliente.Visible = false;
        }

        private void btnCancelarEliminar_Click(object sender, EventArgs e)
        {
            txtEliminarCliente.Clear();

            gbEliminarCliente.Visible = false;
        }

        private async void btnEliminar_Click(object sender, EventArgs e)
        {
            // Obtener el ID del cliente desde el campo de texto
            if (string.IsNullOrWhiteSpace(txtEliminarCliente.Text))
            {
                MessageBox.Show("Por favor, ingrese un ID de cliente para eliminar.");
                return;
            }

            // Convertir el texto del ID a número
            if (!int.TryParse(txtEliminarCliente.Text, out int clienteId) || clienteId <= 0)
            {
                MessageBox.Show("Por favor ingrese un ID de cliente válido.");
                return;
            }

            // Confirmar con el usuario antes de eliminar
            var confirmResult = MessageBox.Show($"¿Está seguro de que quiere eliminar el cliente con ID {clienteId}?",
                                                "Confirmar eliminación",
                                                MessageBoxButtons.YesNo);
            if (confirmResult == DialogResult.No)
            {
                return;  // Si el usuario selecciona No, no hacemos nada
            }

            // Llamar al método para eliminar el cliente
            bool resultado = await EliminarClienteAsync(clienteId);

            if (resultado)
            {
                // Mostrar mensaje de éxito
                MessageBox.Show($"Cliente con ID {clienteId} eliminado correctamente.");

                // Actualizar el DataGridView (aquí lo puedes refrescar según el método que estés usando)
                this.clientesTableAdapter.Fill(this.dbMotelDataSet.Clientes);

                // Limpiar los campos de eliminación
                txtEliminarCliente.Clear();

                // Cerrar el GroupBox de eliminación
                gbEliminarCliente.Visible = false;
            }
            else
            {
                MessageBox.Show("Hubo un error al eliminar el cliente.");
            }
        }

        private void habitacionesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // Redirigir al dashboard principal
            var mainForm = new DashboardForm();
            mainForm.Show();
            this.Hide();
        }

        private void clientesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // Redirigir al dashboard de habitaciones
            var mainForm = new Habitaciones();
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

        private void reportesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var mainForm = new Reportes();
            mainForm.Show();
            this.Hide();
        }
    }
}
