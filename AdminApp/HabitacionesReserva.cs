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
    public partial class HabitacionesReserva : Form
    {
        public HabitacionesReserva()
        {
            InitializeComponent();
        }


        private async Task CargarHabitacionesDisponiblesAsync()
        {
            try
            {
                using (var client = new HttpClient())
                {
                    string url = "http://localhost:5264/api/Habitaciones/habitacionesDisponibles";

                    var response = await client.GetAsync(url);
                    if (!response.IsSuccessStatusCode)
                    {
                        MessageBox.Show("Error al cargar las habitaciones disponibles.");
                        return;
                    }

                    var json = await response.Content.ReadAsStringAsync();
                    var habitaciones = JsonConvert.DeserializeObject<List<ClaseHabitacion>>(json);

                    cmBoxNombreHabitacion.DataSource = habitaciones;
                    cmBoxNombreHabitacion.DisplayMember = nameof(ClaseHabitacion.NumHabitacion);
                    cmBoxNombreHabitacion.ValueMember = nameof(ClaseHabitacion.IdHabitacion);

                    cmBoxEliminarNombreHabitacion.DataSource = habitaciones;
                    cmBoxEliminarNombreHabitacion.DisplayMember = nameof(ClaseHabitacion.NumHabitacion);
                    cmBoxEliminarNombreHabitacion.ValueMember = nameof(ClaseHabitacion.IdHabitacion);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al cargar las habitaciones disponibles: {ex.Message}");
            }
        }

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

        private async Task CambiarEstadoHabitacionAsync(int idHabitacion, string nuevoEstado)
        {
            try
            {
                using (var client = new HttpClient())
                {
                    string url = $"http://localhost:5264/api/Habitaciones/cambiarEstado/{idHabitacion}";
                    // Serializamos la cadena como JSON: "Ocupada"
                    var content = new StringContent($"\"{nuevoEstado}\"", Encoding.UTF8, "application/json");
                    var response = await client.PutAsync(url, content);
                    if (!response.IsSuccessStatusCode)
                    {
                        var error = await response.Content.ReadAsStringAsync();
                        MessageBox.Show($"Error al cambiar el estado de la habitación: {error}");
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al cambiar el estado de la habitación: {ex.Message}");
            }
        }

        private void InicializarColumnasHabitacionGrid()
        {
            // Solo crea las columnas una vez
            if (dgvBuscarHabitacionReserva.Columns.Count == 0)
            {
                dgvBuscarHabitacionReserva.AutoGenerateColumns = false;
                dgvBuscarHabitacionReserva.Columns.Add("IdHabitacion", "ID Habitación");
                dgvBuscarHabitacionReserva.Columns.Add("PrecioHabitacion", "Precio");
                dgvBuscarHabitacionReserva.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            }
        }

        // 2) Método que carga las habitaciones asociadas a una reserva
        private async Task CargarHabitacionesAsociadasReserva(int numReserva)
        {
            try
            {
                InicializarColumnasHabitacionGrid();

                using (var client = new HttpClient())
                {
                    string url = $"http://localhost:5264/api/ReservaHabitacion/habitacionesPorReserva/{numReserva}";
                    var response = await client.GetAsync(url);
                    if (!response.IsSuccessStatusCode)
                    {
                        MessageBox.Show("Error al cargar las habitaciones asociadas a la reserva.");
                        return;
                    }

                    var json = await response.Content.ReadAsStringAsync();
                    var habitaciones = JsonConvert.DeserializeObject<List<ClaseReservaHabitacion>>(json);

                    if (habitaciones == null || habitaciones.Count == 0)
                    {
                        MessageBox.Show("No hay habitaciones asociadas a esta reserva.");
                        return;
                    }

                    // Limpia cualquier fuente de datos anterior
                    dgvBuscarHabitacionReserva.DataSource = null;
                    dgvBuscarHabitacionReserva.Rows.Clear();

                    // Agrega fila por fila
                    foreach (var h in habitaciones)
                    {
                        dgvBuscarHabitacionReserva.Rows.Add(
                            h.IdHabitacion,
                            h.PrecioHabitacion.ToString("C2")
                        );
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al cargar las habitaciones: {ex.Message}");
            }
        }

        private async Task CargarHabitacionesParaEliminarAsync()
        {
            // 1. Si no hay reserva seleccionada, limpia el combo y sale
            if (cmBoxEliminarNumReserva.SelectedItem == null)
            {
                cmBoxEliminarNombreHabitacion.DataSource = null;
                return;
            }

            // 2. Saca la reserva como objeto y de ahí el NumReserva
            var reservaObj = cmBoxEliminarNumReserva.SelectedItem as ClaseReservas;
            if (reservaObj == null)
            {
                cmBoxEliminarNombreHabitacion.DataSource = null;
                return;
            }
            int numReserva = reservaObj.NumReserva;

            // 3. Llamada a la API para traer las habitaciones de esa reserva
            using (var client = new HttpClient())
            {
                string url = $"http://localhost:5264/api/ReservaHabitacion/habitacionesPorReserva/{numReserva}";
                var resp = await client.GetAsync(url);
                if (!resp.IsSuccessStatusCode)
                {
                    //MessageBox.Show("Error al obtener habitaciones asociadas.");
                    cmBoxEliminarNombreHabitacion.DataSource = null;
                    return;
                }

                var json = await resp.Content.ReadAsStringAsync();
                var lista = JsonConvert.DeserializeObject<List<ClaseReservaHabitacion>>(json);

                if (lista == null || lista.Count == 0)
                {
                    MessageBox.Show("La reserva seleccionada no tiene ninguna habitación asociada.");
                    cmBoxEliminarNombreHabitacion.DataSource = null;
                    return;
                }

                // 4. Poblamos el combo con la lista de ReservaHabitacion
                cmBoxEliminarNombreHabitacion.DataSource = lista;
                cmBoxEliminarNombreHabitacion.DisplayMember = nameof(ClaseReservaHabitacion.IdHabitacion);
                cmBoxEliminarNombreHabitacion.ValueMember = nameof(ClaseReservaHabitacion.IdHabitacion);
            }
        }

        private void CerrarApp_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private async void HabitacionesReserva_Load(object sender, EventArgs e)
        {
            // TODO: This line of code loads data into the 'dbMotelReservaHabitacionDataSet.ReservaHabitacion' table. You can move, or remove it, as needed.
            this.reservaHabitacionTableAdapter.Fill(this.dbMotelReservaHabitacionDataSet.ReservaHabitacion);
            await CargarIdsReservasActivasAsync();
            await CargarHabitacionesDisponiblesAsync();
            cmBoxEliminarNumReserva.SelectedIndex = -1;
            cmBoxEliminarNombreHabitacion.SelectedIndex = -1;
        }

        private void btnAgregarHabitacionReserva_Click(object sender, EventArgs e)
        {
            gbAgregarHabiacionReserva.Visible = true;
        }

        private void btnBuscarHabitacionReserva_Click(object sender, EventArgs e)
        {
            gbBuscarHabitacionReserva.Visible = true;
        }

        private async void btnEliminarHabitacionReserva_Click(object sender, EventArgs e)
        {
            gbEliminarHabitacionReserva.Visible = true;
            await CargarHabitacionesDisponiblesAsync();

        }

        private void cmBoxNombreHabitacion_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmBoxNombreHabitacion.SelectedItem is ClaseHabitacion hab)
            {
                txtAgregarPrecioHabitacion.Text = hab.PrecioHabitacion.ToString("C2");
            }
            else
            {
                txtAgregarPrecioHabitacion.Clear();
            }
        }

        private async void btnAgregar_Click(object sender, EventArgs e)
        {
            // 1. Validaciones
            if (cmBoxNumReserva.SelectedItem == null)
            {
                MessageBox.Show("Por favor, selecciona una reserva.");
                return;
            }

            if (cmBoxNombreHabitacion.SelectedItem == null)
            {
                MessageBox.Show("Por favor, selecciona una habitación.");
                return;
            }

            // 2. Obtén los valores
            int numReserva = (int)cmBoxNumReserva.SelectedValue;
            int idHabitacion = (int)cmBoxNombreHabitacion.SelectedValue;
            var habitacionSeleccionada = (ClaseHabitacion)cmBoxNombreHabitacion.SelectedItem;
            decimal precioHabitacion = habitacionSeleccionada.PrecioHabitacion;

            // 3. Prepara el body de la petición
            var requestBody = new
            {
                NumReserva = numReserva,
                IdHabitacion = idHabitacion,
                PrecioHabitacion = precioHabitacion
            };

            try
            {
                using (var client = new HttpClient())
                {
                    string url = "http://localhost:5264/api/ReservaHabitacion";
                    var content = new StringContent(
                        JsonConvert.SerializeObject(requestBody),
                        Encoding.UTF8,
                        "application/json"
                    );

                    var response = await client.PostAsync(url, content);
                    if (response.IsSuccessStatusCode)
                    {
                        // 4. Éxito
                        MessageBox.Show($"La habitación {habitacionSeleccionada.NumHabitacion} ha sido agregada a la reserva {numReserva}.");
                        await CambiarEstadoHabitacionAsync(idHabitacion, "Ocupada");

                        // 5. Actualizar el grid
                        this.reservaHabitacionTableAdapter.Fill(this.dbMotelReservaHabitacionDataSet.ReservaHabitacion);

                        // 6. Limpiar campos
                        cmBoxNumReserva.SelectedIndex = -1;
                        cmBoxNombreHabitacion.SelectedIndex = -1;
                        txtAgregarPrecioHabitacion.Clear();

                        // 7. Cerrar el groupbox
                        gbAgregarHabiacionReserva.Visible = false;
                    }
                    else
                    {
                        // Si la API devolvió un error
                        var error = await response.Content.ReadAsStringAsync();
                        MessageBox.Show($"Error al agregar la habitación a la reserva: {error}");
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al agregar la habitación a la reserva: {ex.Message}");
            }
        }

        private void btnCancelarAgregar_Click(object sender, EventArgs e)
        {
            cmBoxNumReserva.SelectedIndex = -1;
            cmBoxNombreHabitacion.SelectedIndex = -1;
            txtAgregarPrecioHabitacion.Clear();

            // 7. Cerrar el groupbox
            gbAgregarHabiacionReserva.Visible = false;
        }

        private async void btnBuscarReserv_Click(object sender, EventArgs e)
        {
            if (cmBoxbuscarNumServiciosReserva.SelectedItem == null)
            {
                MessageBox.Show("Por favor, selecciona una reserva.");
                return;
            }

            int numReserva = (int)cmBoxbuscarNumServiciosReserva.SelectedValue;
            await CargarHabitacionesAsociadasReserva(numReserva);
        }

        private void btnSalirBuscarReserva_Click(object sender, EventArgs e)
        {
            // 1. Cierra el GroupBox
            gbBuscarHabitacionReserva.Visible = false;

            // 2. Deselecciona la reserva en el ComboBox
            cmBoxbuscarNumServiciosReserva.SelectedIndex = -1;

            // 3. Limpia las filas del DataGridView
            if (dgvBuscarHabitacionReserva.Rows.Count > 0)
            {
                dgvBuscarHabitacionReserva.Rows.Clear();
            }
        }

        private async void btnEliminarHabReserva_Click(object sender, EventArgs e)
        {
            // 1. Validaciones de selección
            if (cmBoxEliminarNumReserva.SelectedItem == null)
            {
                MessageBox.Show("Por favor, selecciona una reserva.");
                return;
            }
            if (cmBoxEliminarNombreHabitacion.SelectedItem == null)
            {
                MessageBox.Show("Por favor, selecciona una habitación.");
                return;
            }

            // 2. Obtener el ID de la reserva
            var reservaObj = cmBoxEliminarNumReserva.SelectedItem as ClaseReservas;
            if (reservaObj == null)
            {
                MessageBox.Show("Error al leer la reserva seleccionada.");
                return;
            }
            int numReserva = reservaObj.NumReserva;

            // 3. Obtener la asociación reserva-habitación seleccionada
            var reservaHab = cmBoxEliminarNombreHabitacion.SelectedItem as ClaseReservaHabitacion;
            if (reservaHab == null)
            {
                MessageBox.Show("Error al leer la habitación asociada a la reserva.");
                return;
            }
            int idHabitacion = reservaHab.IdHabitacion;

            // 4. Confirmar eliminación
            var confirm = MessageBox.Show(
                $"¿Estás seguro de que quieres eliminar la habitación {idHabitacion} de la reserva {numReserva}?",
                "Confirmar eliminación",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Warning
            );
            if (confirm != DialogResult.Yes)
                return;

            try
            {
                // 5. Llamada DELETE a la API de ReservaHabitacion
                using (var client = new HttpClient())
                {
                    string url = $"http://localhost:5264/api/ReservaHabitacion/{numReserva}/{idHabitacion}";
                    var response = await client.DeleteAsync(url);
                    if (!response.IsSuccessStatusCode)
                    {
                        var error = await response.Content.ReadAsStringAsync();
                        MessageBox.Show($"Error al eliminar la habitación de la reserva: {error}");
                        return;
                    }
                }

                // 6. Cambiar estado de la habitación a "Disponible"
                await CambiarEstadoHabitacionAsync(idHabitacion, "Disponible");

                // 7. Refrescar datos en el DataGridView y el TableAdapter
                this.reservaHabitacionTableAdapter.Fill(this.dbMotelReservaHabitacionDataSet.ReservaHabitacion);
                await CargarHabitacionesAsociadasReserva(numReserva);

                // 8. Limpiar campos y cerrar el GroupBox
                cmBoxEliminarNumReserva.SelectedIndex = -1;
                cmBoxEliminarNombreHabitacion.DataSource = null;
                gbEliminarHabitacionReserva.Visible = false;

                MessageBox.Show("Habitación eliminada correctamente de la reserva.");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al eliminar la habitación de la reserva: {ex.Message}");
            }
        }

        private void btnCancelarEliminar_Click(object sender, EventArgs e)
        {
            cmBoxEliminarNumReserva.SelectedIndex = -1;
            cmBoxEliminarNombreHabitacion.SelectedIndex = -1;
            gbEliminarHabitacionReserva.Visible = false;

        }

        private async void cmBoxEliminarNumReserva_SelectedIndexChanged(object sender, EventArgs e)
        {
            await CargarHabitacionesParaEliminarAsync();

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
