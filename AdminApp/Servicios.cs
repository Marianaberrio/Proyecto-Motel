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
    public partial class Servicios : Form
    {
        public Servicios()
        {
            InitializeComponent();
        }

        public async Task<bool> AgregarServicioAsync(ClaseServicios servicio)
        {
            using (var client = new HttpClient())
            {
                try
                {
                    // URL de la API para agregar un servicio
                    string url = "http://localhost:5264/api/servicios";  // Cambia esta URL según tu configuración

                    // Convertir el objeto ClaseServicios a JSON
                    var content = new StringContent(JsonConvert.SerializeObject(servicio), Encoding.UTF8, "application/json");

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
                        MessageBox.Show($"Error al agregar el servicio. Código: {response.StatusCode}, Detalle: {errorContent}");
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

        public async Task<ClaseServicios> BuscarServicioPorID(int idServicio)
        {
            using (var client = new HttpClient())
            {
                try
                {
                    // URL de la API para buscar un servicio por ID
                    string url = $"http://localhost:5264/api/servicios/{idServicio}"; // Cambiar la URL si es necesario

                    // Hacer la solicitud GET a la API
                    var response = await client.GetAsync(url);

                    // Si la respuesta es exitosa, devolver el servicio encontrado
                    if (response.IsSuccessStatusCode)
                    {
                        var resultado = await response.Content.ReadAsStringAsync();
                        return JsonConvert.DeserializeObject<ClaseServicios>(resultado);  // Deserializa el servicio desde la respuesta
                    }
                    else
                    {
                        return null;  // Si no se encuentra el servicio, devolver null
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error en la solicitud HTTP: {ex.Message}");
                    return null;  // Devolver null si hubo un error
                }
            }
        }

        public async Task<bool> ModificarServicioAsync(ClaseServicios servicio)
        {
            using (var client = new HttpClient())
            {
                try
                {
                    string url = $"http://localhost:5264/api/servicios/{servicio.NumServicio}"; // URL de la API para actualizar el servicio

                    var content = new StringContent(JsonConvert.SerializeObject(servicio), Encoding.UTF8, "application/json");

                    var response = await client.PutAsync(url, content);

                    // Verificar si la solicitud fue exitosa (código 2xx)
                    if (response.IsSuccessStatusCode)
                    {
                        return true;
                    }
                    else
                    {
                        var errorContent = await response.Content.ReadAsStringAsync();
                        MessageBox.Show($"Error al modificar el servicio. Código: {response.StatusCode}, Detalle: {errorContent}");
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

        public async Task<bool> EliminarServicioAsync(int idServicio)
        {
            using (var client = new HttpClient())
            {
                try
                {
                    // URL de la API para eliminar un servicio
                    string url = $"http://localhost:5264/api/servicios/{idServicio}"; // Cambiar la URL si es necesario

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
                        MessageBox.Show($"Error al eliminar el servicio. Código: {response.StatusCode}, Detalle: {errorContent}");
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



        private void CerrarApp_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void Servicios_Load(object sender, EventArgs e)
        {
            // TODO: This line of code loads data into the 'dbMotelServiciosDataSet.Servicios' table. You can move, or remove it, as needed.
            this.serviciosTableAdapter.Fill(this.dbMotelServiciosDataSet.Servicios);

        }

        private void btnAgregarCliente_Click(object sender, EventArgs e)
        {
            gbAgregarServicio.Visible = true;
        }

        private void btnBuscarCliente_Click(object sender, EventArgs e)
        {
            gbBuscarServicio.Visible = true;
        }

        private void btnModificarCliente_Click(object sender, EventArgs e)
        {
            gbModificarServicio.Visible =true;
        }

        private void btnEliminarCliente_Click(object sender, EventArgs e)
        {
            gbEliminarServicio.Visible = true;
        }

        private void btnCancelarAgregar_Click(object sender, EventArgs e)
        {
            // Limpiar los campos de texto
            txtAgregarNombreServicio.Clear();
            txtAgregarDescripcionServicio.Clear();
            txtAgregarPrecioServicio.Clear();

            // Cerrar el GroupBox de agregar servicio
            gbAgregarServicio.Visible = false;
        }

        private async void btnAgregar_Click(object sender, EventArgs e)
        {
            // Validar que los campos no estén vacíos
            if (string.IsNullOrWhiteSpace(txtAgregarNombreServicio.Text) ||
                string.IsNullOrWhiteSpace(txtAgregarDescripcionServicio.Text) ||
                string.IsNullOrWhiteSpace(txtAgregarPrecioServicio.Text))
            {
                MessageBox.Show("Por favor, complete todos los campos.");
                return;
            }

            // Validar que el precio sea un valor numérico válido
            if (!decimal.TryParse(txtAgregarPrecioServicio.Text, out decimal precioServicio))
            {
                MessageBox.Show("El precio del servicio debe ser un valor numérico válido.");
                return;
            }

            // Crear el objeto servicio con los valores ingresados
            var servicio = new ClaseServicios
            {
                NombreServicio = txtAgregarNombreServicio.Text,
                DescripcionServicio = txtAgregarDescripcionServicio.Text,
                PrecioServicio = precioServicio
            };

            // Llamar al método para agregar el servicio a la base de datos
            bool resultado = await AgregarServicioAsync(servicio);

            if (resultado)
            {
                // Actualizar el DataGridView para reflejar el nuevo servicio
                this.serviciosTableAdapter.Fill(this.dbMotelServiciosDataSet.Servicios);

                // Mostrar un mensaje de éxito
                MessageBox.Show("Servicio agregado exitosamente.");

                // Limpiar los campos de agregar servicio
                txtAgregarNombreServicio.Clear();
                txtAgregarDescripcionServicio.Clear();
                txtAgregarPrecioServicio.Clear();

                // Cerrar el GroupBox de agregar servicio
                gbAgregarServicio.Visible = false;
            }
            else
            {
                MessageBox.Show("Hubo un error al agregar el servicio.");
            }
        }

        private async void btnBuscarIDServicio_Click(object sender, EventArgs e)
        {
            // Obtener el ID del servicio ingresado
            if (string.IsNullOrWhiteSpace(txtBuscarServicio.Text))
            {
                MessageBox.Show("Por favor ingrese un ID de servicio.");
                return;
            }

            // Convertir el ID de texto a número
            if (!int.TryParse(txtBuscarServicio.Text, out int servicioId) || servicioId <= 0)
            {
                MessageBox.Show("Por favor ingrese un ID de servicio válido.");
                return;
            }

            // Buscar el servicio por ID
            var servicio = await BuscarServicioPorID(servicioId);

            // Si no encontramos el servicio
            if (servicio == null)
            {
                MessageBox.Show("Servicio no encontrado.");
                return;
            }

            // Si encontramos el servicio, mostrar los datos en los campos correspondientes
            txtBuscarNombreServicio.Text = servicio.NombreServicio;
            txtBuscarDescripcionServicio.Text = servicio.DescripcionServicio;
            txtBuscarPrecioServicio.Text = servicio.PrecioServicio.ToString("F2");
        }

        private void btnSalirBuscarServicio_Click(object sender, EventArgs e)
        {
            txtBuscarServicio.Clear();
            txtBuscarNombreServicio.Clear();
            txtBuscarDescripcionServicio.Clear();
            txtBuscarPrecioServicio.Clear();

            gbBuscarServicio.Visible = false;

        }

        private async void btnBuscarModificarServicio_Click(object sender, EventArgs e)
        {
            // Obtener el ID del servicio ingresado
            if (string.IsNullOrWhiteSpace(txtModificarServicio.Text))
            {
                MessageBox.Show("Por favor ingrese un ID de servicio.");
                return;
            }

            // Convertir el ID de texto a número
            if (!int.TryParse(txtModificarServicio.Text, out int servicioId) || servicioId <= 0)
            {
                MessageBox.Show("Por favor ingrese un ID de servicio válido.");
                return;
            }

            // Buscar el servicio por ID
            var servicio = await BuscarServicioPorID(servicioId);

            // Si no encontramos el servicio
            if (servicio == null)
            {
                MessageBox.Show("Servicio no encontrado.");
                return;
            }

            // Si encontramos el servicio, mostrar los datos en los campos correspondientes
            txtModificarNombreServicio.Text = servicio.NombreServicio;
            txtModificardescripcionServicio.Text = servicio.DescripcionServicio;
            txtModificarPrecioServicio.Text = servicio.PrecioServicio.ToString("F2");
        }

        private async void btnModificarServ_Click(object sender, EventArgs e)
        {
            // Confirmar con el usuario antes de modificar
            var confirmResult = MessageBox.Show($"¿Está seguro de que quiere hacerle los cambios al servicio {txtModificarServicio.Text}?",
                                                "Confirmar modificación",
                                                MessageBoxButtons.YesNo);
            if (confirmResult == DialogResult.No)
            {
                return;  // Si el usuario selecciona No, no hacemos nada
            }

            // Crear un objeto de servicio con los nuevos valores
            var servicioModificado = new ClaseServicios
            {
                NumServicio = int.Parse(txtModificarServicio.Text),  // ID del servicio
                NombreServicio = txtModificarNombreServicio.Text,
                DescripcionServicio = txtModificardescripcionServicio.Text,
                PrecioServicio = decimal.Parse(txtModificarPrecioServicio.Text) // Convertir el precio a decimal
            };

            // Llamar al método para actualizar el servicio en la base de datos a través de la API
            bool resultado = await ModificarServicioAsync(servicioModificado);

            if (resultado)
            {
                // Actualizar el DataGridView para reflejar el servicio modificado
                this.serviciosTableAdapter.Fill(this.dbMotelServiciosDataSet.Servicios);

                // Mostrar mensaje de éxito
                MessageBox.Show("Servicio actualizado correctamente.");

                // Limpiar los campos de modificación
                txtModificarServicio.Clear();
                txtModificarNombreServicio.Clear();
                txtModificardescripcionServicio.Clear();
                txtModificarPrecioServicio.Clear();

                // Cerrar el GroupBox de modificación
                this.serviciosTableAdapter.Fill(this.dbMotelServiciosDataSet.Servicios);

                gbModificarServicio.Visible = false;
            }
            else
            {
                MessageBox.Show("Error al actualizar el servicio.");
            }
        }

        private void btnCancelarModificar_Click(object sender, EventArgs e)
        {
            // Limpiar los campos de modificación
            txtModificarServicio.Clear();
            txtModificarNombreServicio.Clear();
            txtModificardescripcionServicio.Clear();
            txtModificarPrecioServicio.Clear();

            gbModificarServicio.Visible = false;

        }

        private async void btnEliminar_Click(object sender, EventArgs e)
        {
            // Obtener el ID del servicio desde el campo de texto
            if (string.IsNullOrWhiteSpace(txtEliminarServicio.Text))
            {
                MessageBox.Show("Por favor, ingrese un ID de servicio para eliminar.");
                return;
            }

            // Convertir el texto del ID a número
            if (!int.TryParse(txtEliminarServicio.Text, out int servicioId) || servicioId <= 0)
            {
                MessageBox.Show("Por favor ingrese un ID de servicio válido.");
                return;
            }

            // Confirmar con el usuario antes de eliminar
            var confirmResult = MessageBox.Show($"¿Está seguro de que quiere eliminar el servicio con ID {servicioId}?",
                                                "Confirmar eliminación",
                                                MessageBoxButtons.YesNo);
            if (confirmResult == DialogResult.No)
            {
                return;  // Si el usuario selecciona No, no hacemos nada
            }

            // Llamar al método para eliminar el servicio
            bool resultado = await EliminarServicioAsync(servicioId);

            if (resultado)
            {
                // Mostrar mensaje de éxito
                MessageBox.Show($"Servicio con ID {servicioId} eliminado correctamente.");

                // Actualizar el DataGridView (aquí lo puedes refrescar según el método que estés usando)
                this.serviciosTableAdapter.Fill(this.dbMotelServiciosDataSet.Servicios);

                // Limpiar los campos de eliminación
                txtEliminarServicio.Clear();

                // Cerrar el GroupBox de eliminación
                gbEliminarServicio.Visible = false;
            }
            else
            {
                MessageBox.Show("Hubo un error al eliminar el servicio.");
            }
        }

        private void btnCancelarEliminar_Click(object sender, EventArgs e)
        {
            txtEliminarServicio.Clear();

            gbEliminarServicio.Visible = false;
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
            var mainForm = new Clientes();
            mainForm.Show();
            this.Hide();
        }

        private void serviciosToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var mainForm = new Reservas();
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
