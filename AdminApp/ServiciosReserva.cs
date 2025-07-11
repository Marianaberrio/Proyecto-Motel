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
    public partial class ServiciosReserva : Form
    {
        public class ReservasResponse
        {
            public List<int> Data { get; set; }
        }

        public ServiciosReserva()
        {
            InitializeComponent();
            cmBoxNombreServicio.SelectedIndexChanged += cmBoxNombreServicio_SelectedIndexChanged;

        }
        // Método para cargar los IDs de las reservas activas
        private async Task CargarIdsReservasActivasAsync()
        {
            try
            {
                using (var client = new HttpClient())
                {
                    // Llamada a la API para obtener las reservas activas
                    string url = "http://localhost:5264/api/reservas/reservasActivas"; // URL correcta de tu API

                    var response = await client.GetAsync(url);
                    if (response.IsSuccessStatusCode)
                    {
                        // Obtener respuesta
                        var jsonResponse = await response.Content.ReadAsStringAsync();

                        // Deserializar la respuesta
                        var reservas = JsonConvert.DeserializeObject<List<ClaseReservas>>(jsonResponse);

                        // Mostrar las reservas en el DataGridView o donde lo necesites
                        cmBoxNumReserva.DataSource = reservas;
                        cmBoxbuscarNumServiciosReserva.DataSource = reservas;
                        cmBoxEliminarNumReserva.DataSource = reservas;
                        // Asignamos la propiedad que queremos mostrar
                        cmBoxNumReserva.DisplayMember = "NumReserva"; // Aquí se usa el ID de la reserva, puedes cambiarlo si es otro campo
                        cmBoxNumReserva.ValueMember = "NumReserva"; // Aquí también asignamos el ID, o cualquier otra propiedad que desees usar como valor
                        cmBoxbuscarNumServiciosReserva.DisplayMember = "NumReserva";
                        cmBoxbuscarNumServiciosReserva.ValueMember = "NumReserva";
                        cmBoxEliminarNumReserva.DisplayMember = "NumReserva";
                        cmBoxEliminarNumReserva.ValueMember = "NumReserva";

                    }
                    else
                    {
                        MessageBox.Show("Error al cargar las reservas activas.");
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al cargar las reservas: {ex.Message}");
            }
        }

        // Método para cargar los nombres, precios y IDs de todos los servicios
        private async Task CargarNombresServiciosAsync()
        {
            try
            {
                using (var client = new HttpClient())
                {
                    // URL de la API para obtener los servicios con nombre, precio e ID
                    string url = "http://localhost:5264/api/ReservaServicios/nombres-precios-ids";

                    var response = await client.GetAsync(url);
                    if (response.IsSuccessStatusCode)
                    {
                        // Obtener la respuesta JSON
                        var jsonResponse = await response.Content.ReadAsStringAsync();

                        // Mostrar la respuesta de la API para verificar los datos (opcional para depuración)
                        // MessageBox.Show(jsonResponse);

                        // Deserializar la respuesta a una lista de objetos de tipo 'Servicios'
                        var servicios = JsonConvert.DeserializeObject<List<ClaseServicios>>(jsonResponse);

                        // Verificar los datos deserializados (opcional para depuración)
                        foreach (var servicio in servicios)
                        {
                            // Mostrar un mensaje con los datos de cada servicio
                            // MessageBox.Show($"NumServicio: {servicio.NumServicio}, NombreServicio: {servicio.NombreServicio}, PrecioServicio: {servicio.PrecioServicio}");
                        }

                        // Convertir la lista de 'Servicios' a una lista de 'ClaseServicios' (mapeo)
                        var claseServicios = servicios.Select(s => new ClaseServicios
                        {
                            NumServicio = s.NumServicio,          // Asignamos el ID (NumServicio)
                            NombreServicio = s.NombreServicio,    // Asignamos el nombre del servicio
                            PrecioServicio = s.PrecioServicio     // Asignamos el precio del servicio
                        }).ToList();

                        // Asignar la lista de 'ClaseServicios' al ComboBox
                        cmBoxNombreServicio.DataSource = claseServicios;
                        cmBoxNombreServicio.DisplayMember = "NombreServicio"; // Mostrar solo el nombre
                        cmBoxNombreServicio.ValueMember = "NumServicio";     // Usar el ID del servicio como valor

                        cmBoxEliminarNombreServicio.DataSource = claseServicios;
                        cmBoxEliminarNombreServicio.DisplayMember = "NombreServicio";
                        cmBoxEliminarNombreServicio.ValueMember = "NumServicio";
                    }
                    else
                    {
                        // Si la API responde con error, mostrar un mensaje
                        MessageBox.Show("Error al cargar los nombres de los servicios.");
                    }
                }
            }
            catch (Exception ex)
            {
                // Si ocurre alguna excepción, mostrar el mensaje de error
                MessageBox.Show($"Error al cargar los servicios: {ex.Message}");
            }
        }

        private async Task AgregarServicioReservaAsync(int numReserva, int numServicio, decimal precioServicio)
        {
            try
            {
                var requestBody = new
                {
                    NumReserva = numReserva,        // ID de la reserva seleccionada
                    NumServicio = numServicio,      // ID del servicio seleccionado
                    PrecioServicio = precioServicio // Precio del servicio
                };

                using (var client = new HttpClient())
                {
                    string url = "http://localhost:5264/api/ReservaServicios"; // URL para agregar el servicio

                    var content = new StringContent(JsonConvert.SerializeObject(requestBody), Encoding.UTF8, "application/json");

                    var response = await client.PostAsync(url, content);

                    if (response.IsSuccessStatusCode)
                    {
                        MessageBox.Show($"El servicio ha sido agregado a la reserva {numReserva}.");
                        // Actualiza el DataGridView o realiza cualquier acción que necesites
                        this.reservaServiciosTableAdapter.Fill(this.dbMotelReservaServiciosDataSet.ReservaServicios);
                    }
                    else
                    {
                        // Mostrar el contenido de la respuesta de error para depuración
                        var responseContent = await response.Content.ReadAsStringAsync();
                        MessageBox.Show($"Error al agregar el servicio a la reserva: {responseContent}");
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al agregar el servicio a la reserva: {ex.Message}");
            }
        }

        private async Task CargarServiciosAsociadosReserva(int numReserva)
        {
            try
            {
                // Aseguramos que las columnas están correctamente configuradas
                InicializarColumnasDataGridView();

                using (var client = new HttpClient())
                {
                    string url = $"http://localhost:5264/api/ReservaServicios/serviciosPorReserva/{numReserva}";

                    var response = await client.GetAsync(url);
                    if (response.IsSuccessStatusCode)
                    {
                        var jsonResponse = await response.Content.ReadAsStringAsync();

                        var servicios = JsonConvert.DeserializeObject<List<ClaseReservaServicio>>(jsonResponse);

                        // Verificamos si la lista de servicios está vacía
                        if (servicios == null || servicios.Count == 0)
                        {
                            // Mostrar mensaje si no hay servicios asociados a la reserva
                            MessageBox.Show("No hay servicios asociados a esta reserva.");
                            return;  // Salir de la función para evitar que se cargue un DataGrid vacío
                        }

                        // Desvinculamos el DataSource antes de agregar las filas manualmente
                        dgvBuscarServicioReserva.DataSource = null;

                        // Limpiar el DataGridView antes de agregar nuevas filas
                        dgvBuscarServicioReserva.Rows.Clear();

                        // Agregar las filas
                        foreach (var servicio in servicios)
                        {
                            dgvBuscarServicioReserva.Rows.Add(
                                servicio.NumServicio,        // Columna 1: ID del servicio
                                servicio.PrecioServicio.ToString("C2")  // Columna 2: Precio del servicio
                            );
                        }

                    }
                    else
                    {
                        MessageBox.Show("Error al cargar los servicios asociados a la reserva.");
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al cargar los servicios: {ex.Message}");
            }
        }

        private async Task EliminarServicioReserva(int numReserva, int numServicio)
        {
            try
            {
                using (var client = new HttpClient())
                {
                    // URL para eliminar el servicio asociado a la reserva
                    string url = $"http://localhost:5264/api/ReservaServicios/{numReserva}/{numServicio}";

                    // Realizar la solicitud DELETE
                    var response = await client.DeleteAsync(url);

                    if (response.IsSuccessStatusCode)
                    {
                        MessageBox.Show($"El servicio ha sido eliminado correctamente de la reserva {numReserva}.");
                    }
                    else
                    {
                        // Mostrar detalles del error
                        var responseContent = await response.Content.ReadAsStringAsync();
                        MessageBox.Show($"Error al eliminar el servicio. Código de estado: {response.StatusCode}\nContenido de la respuesta: {responseContent}");
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al eliminar el servicio: {ex.Message}");
            }
        }

        private async void ServiciosReserva_Load(object sender, EventArgs e)
        {
            // TODO: This line of code loads data into the 'dbMotelReservaServiciosDataSet.ReservaServicios' table. You can move, or remove it, as needed.
            this.reservaServiciosTableAdapter.Fill(this.dbMotelReservaServiciosDataSet.ReservaServicios);
            await CargarIdsReservasActivasAsync();
            await CargarNombresServiciosAsync();
            dgvBuscarServicioReserva.AutoGenerateColumns = false;


        }

        private void InicializarColumnasDataGridView()
        {
            // Verificar si las columnas ya están creadas para evitar duplicados
            if (dgvBuscarServicioReserva.Columns.Count == 0)
            {
                dgvBuscarServicioReserva.Columns.Add("NumServicio", "ID del Servicio");
                dgvBuscarServicioReserva.Columns.Add("PrecioServicio", "Precio del Servicio");
            }
        }

        private void btnAgregarServicioReserva_Click(object sender, EventArgs e)
        {
            gbAgregarServicioReserva.Visible = true;
        }

        private void btnBuscarServicioReserva_Click(object sender, EventArgs e)
        {
            gbBuscarServicioReserva.Visible = true;
        }

        private void btnModificarServicioReserva_Click(object sender, EventArgs e)
        {
        }

        private void btnEliminarServicioReserva_Click(object sender, EventArgs e)
        {
            gbEliminarServicioReserva.Visible = true;
        }

        private void CerrarApp_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void cmBoxNombreServicio_SelectedIndexChanged(object sender, EventArgs e)
        {

            // Verificar si hay un servicio seleccionado
            if (cmBoxNombreServicio.SelectedItem != null)
            {
                // Obtener el servicio seleccionado
                var servicioSeleccionado = (ClaseServicios)cmBoxNombreServicio.SelectedItem;

                // Asignar el precio del servicio al campo de texto
                txtAgregarPrecioServicio.Text = servicioSeleccionado.PrecioServicio.ToString("C2"); // Mostrar el precio con formato monetario
            }
        }

        private async void btnAgregar_Click(object sender, EventArgs e)
        {
            // Verificar que se haya seleccionado una reserva
            if (cmBoxNumReserva.SelectedItem == null)
            {
                MessageBox.Show("Por favor, selecciona una reserva.");
                return;
            }

            // Verificar que se haya seleccionado un servicio
            if (cmBoxNombreServicio.SelectedItem == null)
            {
                MessageBox.Show("Por favor, selecciona un servicio.");
                return;
            }

            // Obtener el ID de la reserva seleccionada
            int numReserva = (int)cmBoxNumReserva.SelectedValue;

            // Obtener el ID del servicio desde SelectedValue (en vez de usar un TextBox)
            int numServicio = (int)cmBoxNombreServicio.SelectedValue; // Aquí obtenemos el ID del servicio desde el ComboBox

            // Obtener el precio del servicio desde el ComboBox seleccionado
            var servicioSeleccionado = (ClaseServicios)cmBoxNombreServicio.SelectedItem;
            decimal precioServicio = servicioSeleccionado.PrecioServicio;

            // Llamar a la función para agregar el servicio a la reserva
            await AgregarServicioReservaAsync(numReserva, numServicio, precioServicio);

            // Limpiar los campos
            cmBoxNumReserva.SelectedIndex = -1;
            cmBoxNombreServicio.SelectedIndex = -1;
            txtAgregarPrecioServicio.Clear();

            // Cerrar el GroupBox
            gbAgregarServicioReserva.Visible = false;
        }

        private void btnCancelarAgregar_Click(object sender, EventArgs e)
        {
            // Limpiar los campos
            cmBoxNumReserva.SelectedIndex = -1;
            cmBoxNombreServicio.SelectedIndex = -1;
            txtAgregarPrecioServicio.Clear();

            // Cerrar el GroupBox
            gbAgregarServicioReserva.Visible = false;
        }

        private async void btnBuscarReserv_Click(object sender, EventArgs e)
        {
            // Verificar que se haya seleccionado una reserva
            if (cmBoxbuscarNumServiciosReserva.SelectedItem == null)
            {
                MessageBox.Show("Por favor, selecciona una reserva.");
                return;
            }

            // Obtener el ID de la reserva seleccionada
            int numReserva = (int)cmBoxbuscarNumServiciosReserva.SelectedValue;

            // Llamar al método para cargar los servicios asociados a la reserva, pasando el numReserva
            await CargarServiciosAsociadosReserva(numReserva);
        }

        private void btnSalirBuscarReserva_Click(object sender, EventArgs e)
        {
            // Cerrar el GroupBox
            gbBuscarServicioReserva.Visible = false;

            // Deseleccionar la reserva en el ComboBox
            cmBoxbuscarNumServiciosReserva.SelectedIndex = -1;

            // Limpiar el DataGridView
            dgvBuscarServicioReserva.Rows.Clear();
        }

        private async void btnEliminarServReserva_Click(object sender, EventArgs e)
        {
            // Verificar que se haya seleccionado una reserva
            if (cmBoxEliminarNumReserva.SelectedItem == null)
            {
                MessageBox.Show("Por favor, selecciona una reserva.");
                return;
            }

            // Verificar que se haya seleccionado un servicio
            if (cmBoxEliminarNombreServicio.SelectedItem == null)
            {
                MessageBox.Show("Por favor, selecciona un servicio.");
                return;
            }

            // Obtener el ID de la reserva seleccionada
            int numReserva = (int)cmBoxEliminarNumReserva.SelectedValue;

            // Obtener el ID del servicio seleccionado (ahora obtenemos el ID desde SelectedValue, no el nombre)
            int numServicio = (int)cmBoxEliminarNombreServicio.SelectedValue;

            // Mostrar mensaje de confirmación
            var confirmResult = MessageBox.Show($"¿Estás seguro de que quieres eliminar el servicio asociado a la reserva {numReserva}?",
                                                 "Confirmar eliminación",
                                                 MessageBoxButtons.YesNo,
                                                 MessageBoxIcon.Warning);

            // Si el usuario confirma, proceder con la eliminación
            if (confirmResult == DialogResult.Yes)
            {
                // Llamar al método para eliminar el servicio
                await EliminarServicioReserva(numReserva, numServicio);

                // Limpiar los campos
                cmBoxEliminarNumReserva.SelectedIndex = -1;
                cmBoxEliminarNombreServicio.SelectedIndex = -1;

                // Actualizar el DataGridView (suponiendo que se actualiza al agregar o eliminar servicios)
                this.reservaServiciosTableAdapter.Fill(this.dbMotelReservaServiciosDataSet.ReservaServicios);

                // Cerrar el GroupBox
                gbEliminarServicioReserva.Visible = false;
            }
        }

        private void menuprincipalToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var mainForm = new DashboardForm();
            mainForm.Show();
            this.Hide();
        }

        private void habitacionesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var mainForm = new Habitaciones();
            mainForm.Show();
            this.Hide();
        }

        private void clientesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var mainForm = new Clientes();
            mainForm.Show();
            this.Hide();
        }

        private void reservasToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var mainForm = new Servicios();
            mainForm.Show();
            this.Hide();
        }

        private void serviciosToolStripMenuItem_Click(object sender, EventArgs e)
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

        private void reservasToolStripMenuItem1_Click(object sender, EventArgs e)
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
    }
}
