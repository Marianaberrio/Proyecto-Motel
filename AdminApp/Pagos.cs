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
    public partial class Pagos : Form
    {
        public Pagos()
        {
            InitializeComponent();
        }

        private async Task CargarReservasAsync()
        {
            try
            {
                using (var client = new HttpClient())
                {
                    // URL que devuelve List<ClaseReservas> (todas o activas)
                    string url = "http://localhost:5264/api/reservas/reservasActivas";
                    var response = await client.GetAsync(url);
                    if (!response.IsSuccessStatusCode)
                    {
                        MessageBox.Show("Error al cargar las reservas.");
                        return;
                    }

                    var json = await response.Content.ReadAsStringAsync();
                    var todas = JsonConvert.DeserializeObject<List<ClaseReservas>>(json);

                    // Filtramos: excluimos las que ya están 'Paga' o 'Cancelada'
                    var pendientes = todas
                        .Where(r => r.EstadoReserva != "Paga"
                                 && r.EstadoReserva != "Cancelada")
                        .ToList();

                    if (pendientes.Count == 0)
                    {
                        MessageBox.Show("No hay reservas pendientes de pago.");
                        cmBoxAgregarNumReserva.DataSource = null;
                        return;
                    }

                    cmBoxAgregarNumReserva.DataSource = pendientes;
                    cmBoxAgregarNumReserva.DisplayMember = nameof(ClaseReservas.NumReserva);
                    cmBoxAgregarNumReserva.ValueMember = nameof(ClaseReservas.NumReserva);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al cargar las reservas: {ex.Message}");
            }
        }

        private async Task CambiarEstadoReservaAsync(int numReserva, string nuevoEstado)
        {
            try
            {
                using (var client = new HttpClient())
                {
                    // Serializamos el nuevo estado como JSON
                    var jsonEstado = JsonConvert.SerializeObject(nuevoEstado);
                    var content = new StringContent(jsonEstado, Encoding.UTF8, "application/json");

                    // Llamada PUT al endpoint que actualiza el estado de la reserva
                    string url = $"http://localhost:5264/api/reservas/cambiarEstado/{numReserva}";
                    var response = await client.PutAsync(url, content);

                    if (!response.IsSuccessStatusCode)
                    {
                        var error = await response.Content.ReadAsStringAsync();
                        MessageBox.Show($"Error al actualizar estado de la reserva: {error}",
                                        "Error",
                                        MessageBoxButtons.OK,
                                        MessageBoxIcon.Error);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al actualizar estado de la reserva: {ex.Message}",
                                "Excepción",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Error);
            }
        }

        private async Task CargarReservasPagasAsync()
        {
            try
            {
                using (var client = new HttpClient())
                {
                    string url = "http://localhost:5264/api/reservas/pagas";
                    var response = await client.GetAsync(url);
                    if (!response.IsSuccessStatusCode)
                    {
                        MessageBox.Show("Error al cargar las reservas pagas.");
                        return;
                    }

                    var json = await response.Content.ReadAsStringAsync();
                    var reservas = JsonConvert.DeserializeObject<List<ClaseReservas>>(json);

                    cmBoxbuscarNumReserva.DataSource = reservas;
                    cmBoxbuscarNumReserva.DisplayMember = nameof(ClaseReservas.NumReserva);
                    cmBoxbuscarNumReserva.ValueMember = nameof(ClaseReservas.NumReserva);

                    cmBoxModificarNumReserva.DataSource = reservas;
                    cmBoxModificarNumReserva.DisplayMember = nameof(ClaseReservas.NumReserva);
                    cmBoxModificarNumReserva.ValueMember = nameof(ClaseReservas.NumReserva);


                    cmBoxCancelarNumReserva.DataSource = reservas;
                    cmBoxCancelarNumReserva.DisplayMember = nameof(ClaseReservas.NumReserva);
                    cmBoxCancelarNumReserva.ValueMember = nameof(ClaseReservas.NumReserva);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al cargar las reservas pagas: {ex.Message}");
            }
        }

        private async Task CargarPagosAsync()
        {
            try
            {
                using (var client = new HttpClient())
                {
                    string url = "http://localhost:5264/api/pagos";
                    var response = await client.GetAsync(url);
                    if (!response.IsSuccessStatusCode)
                    {
                        MessageBox.Show("Error al cargar los pagos.");
                        return;
                    }

                    var json = await response.Content.ReadAsStringAsync();
                    var pagos = JsonConvert.DeserializeObject<List<ClasePagos>>(json);

                    cmBoxbuscarNumPago.DataSource = pagos;
                    cmBoxbuscarNumPago.DisplayMember = nameof(ClasePagos.NumPago);
                    cmBoxbuscarNumPago.ValueMember = nameof(ClasePagos.NumPago);

                    cmBoxModificarNumPago.DataSource = pagos;
                    cmBoxModificarNumPago.DisplayMember = nameof(ClasePagos.NumPago);
                    cmBoxModificarNumPago.ValueMember = nameof(ClasePagos.NumPago);

                    cmBoxCancelarNumPago.DataSource = pagos;
                    cmBoxCancelarNumPago.DisplayMember = nameof(ClasePagos.NumPago);
                    cmBoxCancelarNumPago.ValueMember = nameof(ClasePagos.NumPago);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al cargar los pagos: {ex.Message}");
            }
        }
        private async void Pagos_Load(object sender, EventArgs e)
        {
            // TODO: This line of code loads data into the 'dbMotelDataSet2.Pagos' table. You can move, or remove it, as needed.
            this.pagosTableAdapter.Fill(this.dbMotelDataSet2.Pagos);
            await CargarReservasAsync();
            await CargarReservasPagasAsync();
            await CargarPagosAsync();

        }

        private void btnAgregarPago_Click(object sender, EventArgs e)
        {
            gbAgregarPago.Visible = true;
        }

        private void btnBuscarPago_Click(object sender, EventArgs e)
        {
            gbBuscarPago.Visible = true;
        }

        private void btnModificarPago_Click(object sender, EventArgs e)
        {
            gbModificarPago.Visible = true;
        }

        private void btnCancelarPago_Click(object sender, EventArgs e)
        {
            gbCancelarPago.Visible = true;
        }

        private async void cmBoxAgregarNumReserva_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmBoxAgregarNumReserva.SelectedItem is ClaseReservas reserva)
            {
                // Usa aquí la propiedad que en tu ClaseReservas contenga el total (ej. TotalReserva)
                txtAgregarMontoPago.Text = reserva.TotalReserva.ToString("C2");
            }
            else
            {
                txtAgregarMontoPago.Clear();
            }
        }

        private async void btnAgregar_Click(object sender, EventArgs e)
        {
            // 1. Validaciones básicas
            if (cmBoxAgregarNumReserva.SelectedItem == null)
            {
                MessageBox.Show("Por favor selecciona una reserva.");
                return;
            }
            if (string.IsNullOrWhiteSpace(txtAgregarMontoPago.Text))
            {
                MessageBox.Show("El monto de pago no puede estar vacío.");
                return;
            }
            if (cmBoxAgregarMetodoPago.SelectedItem == null)
            {
                MessageBox.Show("Por favor selecciona un método de pago.");
                return;
            }

            // 2. Leer valores de los controles
            var reserva = (ClaseReservas)cmBoxAgregarNumReserva.SelectedItem;
            int numReserva = reserva.NumReserva;
            decimal monto;
            if (!decimal.TryParse(
                    txtAgregarMontoPago.Text.Trim().Replace("$", ""),
                    System.Globalization.NumberStyles.Currency,
                    null,
                    out monto))
            {
                MessageBox.Show("Monto inválido.");
                return;
            }
            string metodo = cmBoxAgregarMetodoPago.SelectedItem.ToString();
            string comentario = txtAgregarComentarioPago.Text.Trim();
            DateTime fechaPago = DateTime.Now;
            string estadoPago = "Procesado";

            // 3. Construir el objeto para enviar
            var pagoRequest = new
            {
                NumReserva = numReserva,
                MontoPago = monto,
                FechaPago = fechaPago,
                MetodoPago = metodo,
                EstadoPago = estadoPago,
                ComentarioPago = string.IsNullOrEmpty(comentario) ? null : comentario
            };

            try
            {
                using (var client = new HttpClient())
                {
                    string url = "http://localhost:5264/api/pagos";
                    var json = JsonConvert.SerializeObject(pagoRequest);
                    var content = new StringContent(json, Encoding.UTF8, "application/json");

                    var response = await client.PostAsync(url, content);
                    if (!response.IsSuccessStatusCode)
                    {
                        var err = await response.Content.ReadAsStringAsync();
                        MessageBox.Show($"Error al crear el pago: {err}");
                        return;
                    }
                }

                // 4. Cambiar estado de la reserva a "Paga"
                await CambiarEstadoReservaAsync(numReserva, "Paga");

                // 5. Mensaje de éxito
                MessageBox.Show("El pago se ha creado con éxito.");

                // 6. Refrescar el DataGrid/TableAdapter
                this.pagosTableAdapter.Fill(this.dbMotelDataSet2.Pagos);

                // 7. Limpiar controles
                cmBoxAgregarNumReserva.SelectedIndex = -1;
                txtAgregarMontoPago.Clear();
                cmBoxAgregarMetodoPago.SelectedIndex = -1;
                txtAgregarComentarioPago.Clear();

                // 8. Cerrar el GroupBox
                gbAgregarPago.Visible = false;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al procesar el pago: {ex.Message}");
            }
        }

        private void CerrarApp_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void btnBuscarNumReserva_CheckedChanged(object sender, EventArgs e)
        {
            cmBoxbuscarNumPago.Enabled = false;
            cmBoxbuscarNumReserva.Enabled = true;
        }

        private void btnBuscarNumPago_CheckedChanged(object sender, EventArgs e)
        {
            cmBoxbuscarNumReserva.Enabled = false;
            cmBoxbuscarNumPago.Enabled = true;
        }

        private async void btnBuscarPagos_Click(object sender, EventArgs e)
        {
            try
            {
                using (var client = new HttpClient())
                {
                    HttpResponseMessage response;
                    ClasePagos pago = null;

                    // ¿Buscar por reserva o por pago?
                    if (cmBoxbuscarNumReserva.Enabled && cmBoxbuscarNumReserva.SelectedItem is ClaseReservas resObj)
                    {
                        int numReserva = resObj.NumReserva;
                        response = await client.GetAsync($"http://localhost:5264/api/pagos/reserva/{numReserva}");
                    }
                    else if (cmBoxbuscarNumPago.Enabled && cmBoxbuscarNumPago.SelectedItem is ClasePagos pagoObj)
                    {
                        int numPago = pagoObj.NumPago;
                        response = await client.GetAsync($"http://localhost:5264/api/pagos/{numPago}");
                    }
                    else
                    {
                        MessageBox.Show("Selecciona primero una reserva o un pago para buscar.");
                        return;
                    }

                    if (!response.IsSuccessStatusCode)
                    {
                        MessageBox.Show("No se encontró ningún pago con los criterios indicados.");
                        return;
                    }

                    var json = await response.Content.ReadAsStringAsync();
                    pago = JsonConvert.DeserializeObject<ClasePagos>(json);

                    if (pago == null)
                    {
                        MessageBox.Show("No se encontró ningún pago con los criterios indicados.");
                        return;
                    }

                    // Rellenar los campos con la información obtenida
                    txtBuscarNumReserva.Text = pago.NumReserva.ToString();
                    txtBuscarNumPago.Text = pago.NumPago.ToString();
                    txtBuscarMontoPago.Text = pago.MontoPago.ToString("C2");
                    txtBuscarFechaPago.Text = pago.FechaPago.ToString("g");   // formato corto fecha+hora
                    txtBuscarMetodoDePago.Text = pago.MetodoPago;
                    txtBuscarEstadoPago.Text = pago.EstadoPago;
                    txtBuscarComentarioPago.Text = pago.ComentarioPago ?? "";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al buscar el pago: {ex.Message}");
            }
        }

        private void btnSalirBuscarCliente_Click(object sender, EventArgs e)
        {
            // Limpiar campos antes de buscar
            txtBuscarNumReserva.Clear();
            txtBuscarNumPago.Clear();
            txtBuscarMontoPago.Clear();
            txtBuscarFechaPago.Clear();
            txtBuscarMetodoDePago.Clear();
            txtBuscarEstadoPago.Clear();
            txtBuscarComentarioPago.Clear();

            gbBuscarPago.Visible = false;
        }

        private void btnModificarNumReserva_CheckedChanged(object sender, EventArgs e)
        {
            cmBoxModificarNumReserva.Enabled = true;
            cmBoxModificarNumPago.Enabled = false;
        }

        private void btnModificarNumPago_CheckedChanged(object sender, EventArgs e)
        {
            cmBoxModificarNumReserva.Enabled = false;
            cmBoxModificarNumPago.Enabled = true;
        }

        private void btnCancelarModificar_Click(object sender, EventArgs e)
        {
            txtModificarNumReserva.Clear();
            txtModificarNumPago.Clear();
            txtModificarMontoPago.Clear();
            txtModificarFechaPago.Clear();
            txtModificarMetodoPago.Clear();
            txtModificarEstadoPago.Clear();
            txtModificarComentarioPago.Clear();
        }

        private async void btnBuscarModificar_Click(object sender, EventArgs e)
        {
            try
            {
                ClasePagos pago = null;
                using (var client = new HttpClient())
                {
                    HttpResponseMessage response;

                    // ¿Buscar por reserva o por pago?
                    if (cmBoxModificarNumReserva.Enabled && cmBoxModificarNumReserva.SelectedItem is ClaseReservas resObj)
                    {
                        int numReserva = resObj.NumReserva;
                        response = await client.GetAsync($"http://localhost:5264/api/pagos/reserva/{numReserva}");
                    }
                    else if (cmBoxModificarNumPago.Enabled && cmBoxModificarNumPago.SelectedItem is ClasePagos pagoObj)
                    {
                        int numPago = pagoObj.NumPago;
                        response = await client.GetAsync($"http://localhost:5264/api/pagos/{numPago}");
                    }
                    else
                    {
                        MessageBox.Show("Selecciona primero una reserva o un pago para buscar.");
                        return;
                    }

                    if (!response.IsSuccessStatusCode)
                    {
                        MessageBox.Show("No se encontró ningún pago con los criterios indicados.");
                        return;
                    }

                    var json = await response.Content.ReadAsStringAsync();
                    pago = JsonConvert.DeserializeObject<ClasePagos>(json);
                    if (pago == null)
                    {
                        MessageBox.Show("No se encontró ningún pago con los criterios indicados.");
                        return;
                    }
                }

                // Rellenamos los campos de modificación
                txtModificarNumReserva.Text = pago.NumReserva.ToString();
                txtModificarNumPago.Text = pago.NumPago.ToString();
                txtModificarMontoPago.Text = pago.MontoPago.ToString("C2");
                txtModificarFechaPago.Text = pago.FechaPago.ToString("g");
                txtModificarMetodoPago.Text = pago.MetodoPago;
                txtModificarEstadoPago.Text = pago.EstadoPago;
                txtModificarComentarioPago.Text = pago.ComentarioPago ?? "";
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al buscar el pago para modificar: {ex.Message}");
            }
        }

        private async void btnModificarPagos_Click(object sender, EventArgs e)
        {
            // 1. Validaciones
            if (string.IsNullOrWhiteSpace(txtModificarNumPago.Text) ||
                string.IsNullOrWhiteSpace(txtModificarNumReserva.Text) ||
                string.IsNullOrWhiteSpace(txtModificarMontoPago.Text) ||
                string.IsNullOrWhiteSpace(txtModificarFechaPago.Text) ||
                string.IsNullOrWhiteSpace(txtModificarMetodoPago.Text) ||
                string.IsNullOrWhiteSpace(txtModificarEstadoPago.Text))
            {
                MessageBox.Show("Por favor completa todos los campos obligatorios antes de modificar.");
                return;
            }

            // 2. Leer y parsear valores
            if (!int.TryParse(txtModificarNumPago.Text.Trim(), out int numPago))
            {
                MessageBox.Show("Número de pago inválido.");
                return;
            }
            if (!int.TryParse(txtModificarNumReserva.Text.Trim(), out int numReserva))
            {
                MessageBox.Show("Número de reserva inválido.");
                return;
            }
            if (!decimal.TryParse(txtModificarMontoPago.Text.Trim().Replace("$", ""),
                                  System.Globalization.NumberStyles.Currency,
                                  null, out decimal montoPago))
            {
                MessageBox.Show("Monto de pago inválido.");
                return;
            }
            if (!DateTime.TryParse(txtModificarFechaPago.Text.Trim(), out DateTime fechaPago))
            {
                MessageBox.Show("Fecha de pago inválida.");
                return;
            }
            string metodoPago = txtModificarMetodoPago.Text.Trim();
            string estadoPago = txtModificarEstadoPago.Text.Trim();
            string comentarioPago = txtModificarComentarioPago.Text.Trim();

            // 3. Construir objeto de pago
            var pagoRequest = new ClasePagos
            {
                NumPago = numPago,
                NumReserva = numReserva,
                MontoPago = montoPago,
                FechaPago = fechaPago,
                MetodoPago = metodoPago,
                EstadoPago = estadoPago,
                ComentarioPago = string.IsNullOrEmpty(comentarioPago) ? null : comentarioPago
            };

            // 4. Ejecutar PUT hacia la API
            try
            {
                using (var client = new HttpClient())
                {
                    string url = $"http://localhost:5264/api/pagos/modificar/{numPago}";
                    var json = JsonConvert.SerializeObject(pagoRequest);
                    var content = new StringContent(json, Encoding.UTF8, "application/json");

                    var response = await client.PutAsync(url, content);
                    if (response.IsSuccessStatusCode)
                    {
                        MessageBox.Show("El pago ha sido modificado con éxito.");

                        // 5. Refrescar el grid o TableAdapter
                        this.pagosTableAdapter.Fill(this.dbMotelDataSet2.Pagos);

                        // 6. Limpiar campos
                        txtModificarNumPago.Clear();
                        txtModificarNumReserva.Clear();
                        txtModificarMontoPago.Clear();
                        txtModificarFechaPago.Clear();
                        txtModificarMetodoPago.Clear();
                        txtModificarEstadoPago.Clear();
                        txtModificarComentarioPago.Clear();

                        // 7. Cerrar el GroupBox de modificación
                        gbModificarPago.Visible = false;
                    }
                    else
                    {
                        var err = await response.Content.ReadAsStringAsync();
                        MessageBox.Show($"Error al modificar el pago: {err}");
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al modificar el pago: {ex.Message}");
            }
        }

        private void btnCancelarNumReserva_CheckedChanged(object sender, EventArgs e)
        {
            cmBoxCancelarNumReserva.Enabled = true;
            cmBoxCancelarNumPago.Enabled = false;
        }

        private void btnCancelarNumPago_CheckedChanged(object sender, EventArgs e)
        {
            cmBoxCancelarNumReserva.Enabled = false;
            cmBoxCancelarNumPago.Enabled = true;
        }

        private void btnCancelarEliminar_Click(object sender, EventArgs e)
        {
            cmBoxCancelarNumReserva.Enabled = true;
            cmBoxCancelarNumPago.Enabled = false;

            gbCancelarPago.Visible = false;
        }

        private async void btnEliminar_Click(object sender, EventArgs e)
        {
            int numPago;

            // 1) Determinar el pago a cancelar según qué combo esté habilitado
            if (cmBoxCancelarNumReserva.Enabled && cmBoxCancelarNumReserva.SelectedItem is ClaseReservas res)
            {
                // Obtener el pago asociado a esta reserva
                using (var client = new HttpClient())
                {
                    var resp = await client.GetAsync($"http://localhost:5264/api/pagos/reserva/{res.NumReserva}");
                    if (!resp.IsSuccessStatusCode)
                    {
                        MessageBox.Show("No se encontró ningún pago para la reserva seleccionada.");
                        return;
                    }
                    var json = await resp.Content.ReadAsStringAsync();
                    var pago = JsonConvert.DeserializeObject<ClasePagos>(json);
                    numPago = pago.NumPago;
                }
            }
            else if (cmBoxCancelarNumPago.Enabled && cmBoxCancelarNumPago.SelectedItem is ClasePagos p)
            {
                // Ya tenemos el pago
                numPago = p.NumPago;
            }
            else
            {
                MessageBox.Show("Selecciona primero una reserva o un pago para cancelar.");
                return;
            }

            // 2) Confirmación
            var result = MessageBox.Show(
                $"¿Estás seguro de que deseas cancelar el pago #{numPago}?",
                "Confirmar cancelación",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Warning
            );
            if (result != DialogResult.Yes) return;

            // 3) Llamada PUT al endpoint dedicado
            try
            {
                using (var client = new HttpClient())
                {
                    string url = $"http://localhost:5264/api/pagos/cancelar/{numPago}";
                    var response = await client.PutAsync(url, null);

                    if (!response.IsSuccessStatusCode)
                    {
                        var err = await response.Content.ReadAsStringAsync();
                        MessageBox.Show($"Error al cancelar el pago: {err}");
                        return;
                    }
                }

                // 4) Refrescar datos
                this.pagosTableAdapter.Fill(this.dbMotelDataSet2.Pagos);

                // 5) Limpiar y cerrar
                cmBoxCancelarNumReserva.SelectedIndex = -1;
                cmBoxCancelarNumPago.SelectedIndex = -1;
                gbCancelarPago.Visible = false;

                MessageBox.Show($"El pago #{numPago} ha sido cancelado con éxito.");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al cancelar el pago: {ex.Message}");
            }
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

        private void serviciosToolStripMenuItem_Click(object sender, EventArgs e)
        {
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
            var mainForm = new Reportes();
            mainForm.Show();
            this.Hide();
        }
    }
}
