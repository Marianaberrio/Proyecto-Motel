using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AdminApp
{
    public partial class Reservas : Form
    {
        public Reservas()
        {
            InitializeComponent();
        }

        private async Task CargarClientesIdsYCorreosAsync()
        {
            try
            {
                using (var client = new HttpClient())
                {
                    // Cargar IDs de los clientes
                    string urlIds = "http://localhost:5264/api/clientes/ids"; // URL correcta de tu API para obtener los IDs

                    var responseIds = await client.GetAsync(urlIds);
                    if (responseIds.IsSuccessStatusCode)
                    {
                        // Obtener respuesta de IDs
                        var jsonResponseIds = await responseIds.Content.ReadAsStringAsync();

                        // Deserializar la respuesta de IDs
                        var clienteIds = JsonConvert.DeserializeObject<List<int>>(jsonResponseIds);

                        cmBoxNumCliente.DataSource = clienteIds;
                        // No asignamos DisplayMember ni ValueMember
                        // Muestra los IDs como una lista simple
                    }
                    else
                    {
                        MessageBox.Show("Error al cargar los IDs de los clientes.");
                    }

                    // Cargar correos de los clientes
                    string urlCorreos = "http://localhost:5264/api/clientes/correos"; // URL correcta de tu API para obtener los correos

                    var responseCorreos = await client.GetAsync(urlCorreos);
                    if (responseCorreos.IsSuccessStatusCode)
                    {
                        // Obtener respuesta de correos
                        var jsonResponseCorreos = await responseCorreos.Content.ReadAsStringAsync();

                        // Verificar que la respuesta no esté vacía
                        if (string.IsNullOrWhiteSpace(jsonResponseCorreos))
                        {
                            MessageBox.Show("No se encontraron correos de clientes.");
                            return;
                        }

                        // Deserializar la respuesta de correos
                        var clienteCorreos = JsonConvert.DeserializeObject<List<string>>(jsonResponseCorreos);

                        // Verificar que la lista no esté vacía
                        if (clienteCorreos != null && clienteCorreos.Count > 0)
                        {
                            // Asignar los correos directamente al ComboBox sin DisplayMember ni ValueMember
                            
                            // El ComboBox automáticamente maneja la lista de strings de manera simple
                        }
                        else
                        {
                            MessageBox.Show("No se encontraron correos de clientes.");
                        }
                    }
                    else
                    {
                        MessageBox.Show("Error al cargar los correos de los clientes.");
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al cargar los clientes: {ex.Message}");
            }
        }

        private async Task<int> GetNumCliente()
        {
            // Verificamos si el ComboBox de ID está habilitado (cuando se selecciona por ID)
            if (cmBoxNumCliente.Enabled)
            {
                return (int)cmBoxNumCliente.SelectedItem; // Si seleccionamos por ID
            }
            // Si no, significa que el cliente está seleccionado por correo
            

            return -1; // Si no se seleccionó ni ID ni correo
        }

        private async Task CargarIdsReservasAsync()
        {
            try
            {
                using (var client = new HttpClient())
                {
                    string url = "http://localhost:5264/api/reservas/ids"; // URL de la API para obtener los IDs de las reservas

                    var response = await client.GetAsync(url);
                    if (response.IsSuccessStatusCode)
                    {
                        // Obtener respuesta de IDs de reservas
                        var jsonResponse = await response.Content.ReadAsStringAsync();

                        // Deserializar la respuesta
                        var idsReservas = JsonConvert.DeserializeObject<List<int>>(jsonResponse);

                        // Cargar los IDs de las reservas en el ComboBox
                        cmBoxIdsReserva.DataSource = idsReservas;
                        cmBoxModificarIdReserva.DataSource = idsReservas;
                    }
                    else
                    {
                        MessageBox.Show("Error al cargar los IDs de las reservas.");
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al cargar los IDs de las reservas: {ex.Message}");
            }
        }


        // Método para obtener el cliente por correo (realizar un GET a la API)
        private async Task<ClaseClientes> ObtenerClientePorCorreo(string correo)
        {
            try
            {
                // Verificar el correo antes de enviarlo
                MessageBox.Show($"Correo que se está enviando: {correo}");

                using (var client = new HttpClient())
                {
                    string url = $"http://localhost:5264/api/clientes/buscarPorEmail?email={correo}";
                    var response = await client.GetAsync(url);  // Hacemos la llamada asincrónica

                    if (response.IsSuccessStatusCode)
                    {
                        var jsonResponse = await response.Content.ReadAsStringAsync();  // También se hace de forma asincrónica
                        var cliente = JsonConvert.DeserializeObject<ClaseClientes>(jsonResponse);
                        return cliente;
                    }
                    else
                    {
                        MessageBox.Show($"No se pudo obtener el cliente. Estado: {response.StatusCode}");
                        return null;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al obtener cliente: {ex.Message}");
                return null;
            }
        }

        private async Task<bool> CrearReservaApi(int numHabitaciones, TipoHabitacion tipoSeleccionado, decimal totalReserva, int numCliente)
        {
            try
            {
                var reserva = new
                {
                    NumCliente = numCliente,  // Aquí se obtiene el cliente seleccionado, por id o correo
                    FechaReserva = DateTime.Now,  // Fecha actual
                    FechaEntrada = dateTimePickerFechaEntrada.Value,
                    FechaSalida = dateTimePickerFechaSalida.Value,
                    EstadoReserva = "Activa",  // Estado por defecto
                    TotalReserva = totalReserva,
                    ComentarioReserva = txtComentarioReserva.Text
                };

                using (var client = new HttpClient())
                {
                    var url = "http://localhost:5264/api/reservas"; // Asegúrate de que esta URL sea la correcta
                    var content = new StringContent(JsonConvert.SerializeObject(reserva), Encoding.UTF8, "application/json");
                    var response = await client.PostAsync(url, content);

                    return response.IsSuccessStatusCode;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al crear la reserva: {ex.Message}");
                return false;
            }
        }

        private async Task ObtenerReservaPorId(int numReserva)
        {
            try
            {
                using (var client = new HttpClient())
                {
                    string url = $"http://localhost:5264/api/reservas/{numReserva}"; // URL para obtener la reserva por ID

                    var response = await client.GetAsync(url);
                    if (response.IsSuccessStatusCode)
                    {
                        // Leer la respuesta
                        var jsonResponse = await response.Content.ReadAsStringAsync();

                        // Deserializar la respuesta en un objeto de tipo ClaseReservas
                        var reserva = JsonConvert.DeserializeObject<ClaseReservas>(jsonResponse);

                        // Asignar los valores a los campos del formulario
                        dtPickerFechaEntrada.Value = reserva.FechaEntrada;
                        dtPickerFechaSalida.Value = reserva.FechaSalida;
                        txtFechaCreada.Text = reserva.FechaReserva.ToString("yyyy-MM-dd");
                        txtBoxBuscarIDCliente.Text = reserva.NumCliente.ToString();
                        txtEstadoReserva.Text = reserva.EstadoReserva;
                        txtBuscarTotalReserva.Text = reserva.TotalReserva.ToString("C2"); // Mostrar con formato monetario
                        txtBuscarComentarioReserva.Text = reserva.ComentarioReserva;
                    }
                    else
                    {
                        MessageBox.Show("No se encontró la reserva.");
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al obtener la reserva: {ex.Message}");
            }
        }

        private async Task ObtenerReservaParaModificar(int numReserva)
        {
            try
            {
                using (var client = new HttpClient())
                {
                    string url = $"http://localhost:5264/api/reservas/{numReserva}"; // URL para obtener la reserva por ID

                    var response = await client.GetAsync(url);
                    if (response.IsSuccessStatusCode)
                    {
                        var jsonResponse = await response.Content.ReadAsStringAsync();
                        var reserva = JsonConvert.DeserializeObject<ClaseReservas>(jsonResponse);

                        // Asignar los valores a los campos de modificación
                        txtModificarIdCliente.Text = reserva.NumCliente.ToString();
                        dtPickerModificarFechaEntrada.Value = reserva.FechaEntrada;
                        dtPickerModificarFechaSalida.Value = reserva.FechaSalida;
                        txtModificarFechaCreada.Text = reserva.FechaReserva.ToString("yyyy-MM-dd");
                        txtModificarEstadoReserva.Text = reserva.EstadoReserva;
                        txtTotalReserva.Text = reserva.TotalReserva.ToString("C2");
                        txtModificarComentarioReserva.Text = reserva.ComentarioReserva;
                    }
                    else
                    {
                        MessageBox.Show("No se encontró la reserva.");
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al obtener la reserva: {ex.Message}");
            }
        }

        private async Task<bool> ActualizarReservaApi(int numReserva, ClaseReservas reservaModificada)
        {
            try
            {
                using (var client = new HttpClient())
                {
                    var url = $"http://localhost:5264/api/reservas/{numReserva}"; // URL para la API que actualiza la reserva

                    var content = new StringContent(JsonConvert.SerializeObject(reservaModificada), Encoding.UTF8, "application/json");

                    var response = await client.PutAsync(url, content);

                    return response.IsSuccessStatusCode;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al actualizar la reserva: {ex.Message}");
                return false;
            }
        }

        private void ActualizarTotalReserva()
        {
            int numHabitaciones;
            if (int.TryParse(txtNumHabitaciones.Text, out numHabitaciones) && numHabitaciones > 0)
            {
                // Obtener el tipo de habitación seleccionado
                var tipoSeleccionado = (TipoHabitacion)cmBoxTipoHabitacion.SelectedItem;
                decimal precioHabitacion = tipoSeleccionado.Precio;

                // Calcular el total de la reserva
                decimal totalReserva = numHabitaciones * precioHabitacion;

                // Mostrar el total en el campo correspondiente
                txtBoxTotalReserva.Text = totalReserva.ToString("C2"); // Mostramos el total con formato monetario
            }
            else
            {
                txtBoxTotalReserva.Clear();
            }
        }

        // Método para cargar los tipos de habitación
        private void CargarTiposDeHabitacion()
        {
            // Lista de tipos de habitación con sus precios
            var tiposHabitacion = new List<TipoHabitacion>
    {
        new TipoHabitacion { Tipo = "Single", Precio = 100 },
        new TipoHabitacion { Tipo = "Double", Precio = 150 },
        new TipoHabitacion { Tipo = "Triple", Precio = 200 },
        new TipoHabitacion { Tipo = "Suite", Precio = 250 }
    };

            // Asignar la lista al ComboBox
            cmBoxTipoHabitacion.DataSource = tiposHabitacion;
            cmBoxTipoHabitacion.DisplayMember = "Tipo";  // Mostrar el nombre del tipo de habitación
            cmBoxTipoHabitacion.ValueMember = "Precio";  // Usar el precio como el valor del ComboBox
        }

        private void CerrarApp_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private async void Reservas_Load(object sender, EventArgs e)
        {
            // TODO: This line of code loads data into the 'dbMotelDataSet1.Reservas' table. You can move, or remove it, as needed.
            this.reservasTableAdapter.Fill(this.dbMotelDataSet1.Reservas);
            CargarTiposDeHabitacion();
            await CargarClientesIdsYCorreosAsync();
            await CargarIdsReservasAsync();

        }

        private void btnAgregarReserva_Click(object sender, EventArgs e)
        {
            gbAgregarReserva.Visible = true;
        }

        private void btnBuscarReserva_Click(object sender, EventArgs e)
        {
            gbBuscarReserva.Visible = true;
        }

        private void btnModificarReserva_Click(object sender, EventArgs e)
        {
            gbModificarReserva.Visible = true;
        }

        private void btnEliminarReserva_Click(object sender, EventArgs e)
        {
            gbEliminarReserva.Visible = true;
        }

        private void btnCancelarAgregar_Click(object sender, EventArgs e)
        {
            // Limpiar los campos
            cmBoxNumCliente.SelectedIndex = -1;
            dateTimePickerFechaEntrada.Value = DateTime.Now;
            dateTimePickerFechaSalida.Value = DateTime.Now;
            txtNumHabitaciones.Clear();
            txtComentarioReserva.Clear();

            // Cerrar el GroupBox
            gbAgregarReserva.Visible = false;
        }

        private void btnAgregarNumCliente_CheckedChanged(object sender, EventArgs e)
        {
            if (btnAgregarNumCliente.Checked)
            {
                cmBoxNumCliente.Enabled = true;  // Habilitamos el ComboBox de ID de cliente

                btnAgregarNumCliente.Enabled = false;  // Deshabilitamos el botón de ID
            }
        }

       
        private async void btnAgregar_Click(object sender, EventArgs e)
        {
            // Validación de número de habitaciones y tipo de habitación seleccionada
            if (string.IsNullOrWhiteSpace(txtNumHabitaciones.Text) || cmBoxTipoHabitacion.SelectedItem == null)
            {
                MessageBox.Show("Por favor, ingresa el número de habitaciones y selecciona el tipo de habitación.");
                return;
            }

            // Obtener el número de habitaciones
            int numHabitaciones = int.Parse(txtNumHabitaciones.Text);

            // Obtener el tipo de habitación seleccionado y su precio
            var tipoSeleccionado = (TipoHabitacion)cmBoxTipoHabitacion.SelectedItem;
            decimal precioHabitacion = tipoSeleccionado.Precio;

            // Calcular el total de la reserva
            decimal totalReserva = numHabitaciones * precioHabitacion;

            // Obtener el ID del cliente usando GetNumCliente (ahora es asincrónico)
            int numCliente = await GetNumCliente();
            if (numCliente == -1)
            {
                MessageBox.Show("No se pudo obtener el ID del cliente.");
                return;
            }

            // Crear la reserva usando la API
            bool resultado = await CrearReservaApi(numHabitaciones, tipoSeleccionado, totalReserva, numCliente);

            if (resultado)
            {
                // Limpiar los campos
                cmBoxNumCliente.SelectedIndex = -1;
                dateTimePickerFechaEntrada.Value = DateTime.Now;
                dateTimePickerFechaSalida.Value = DateTime.Now;
                txtNumHabitaciones.Clear();
                txtComentarioReserva.Clear();

                MessageBox.Show("Reserva creada exitosamente!");

                // Cerrar el GroupBox
                gbAgregarReserva.Visible = false;
                this.reservasTableAdapter.Fill(this.dbMotelDataSet1.Reservas);
            }
            else
            {
                MessageBox.Show("Hubo un error al crear la reserva.");
            }
        }

        private void txtNumHabitaciones_TextChanged(object sender, EventArgs e)
        {
            ActualizarTotalReserva();
        }

        private void cmBoxTipoHabitacion_SelectedIndexChanged(object sender, EventArgs e)
        {
            ActualizarTotalReserva();
        }

        private async void btnBuscarReserv_Click(object sender, EventArgs e)
        {
            // Verificar si se ha seleccionado un ID de reserva
            if (cmBoxIdsReserva.SelectedItem == null)
            {
                MessageBox.Show("Por favor, selecciona una reserva.");
                return;
            }

            // Obtener el ID de la reserva seleccionada
            int numReserva = (int)cmBoxIdsReserva.SelectedItem;

            // Llamar al método para obtener los detalles de la reserva
            await ObtenerReservaPorId(numReserva);
        }

        private void btnSalirBuscarReserva_Click(object sender, EventArgs e)
        {
            // Limpiar los campos
            cmBoxIdsReserva.SelectedIndex = -1;
            dtPickerFechaEntrada.Value = DateTime.Now;
            dtPickerFechaSalida.Value = DateTime.Now;
            txtFechaCreada.Clear();
            txtBoxBuscarIDCliente.Clear();
            txtEstadoReserva.Clear();
            txtBuscarTotalReserva.Clear();
            txtBuscarComentarioReserva.Clear();

            // Cerrar el GroupBox
            gbBuscarReserva.Visible = false;
        }

        private async void btnBuscarModificarReserva_Click(object sender, EventArgs e)
        {
            // Verificar si se ha seleccionado un ID de reserva
            if (cmBoxModificarIdReserva.SelectedItem == null)
            {
                MessageBox.Show("Por favor, selecciona una reserva.");
                return;
            }

            // Obtener el ID de la reserva seleccionada
            int numReserva = (int)cmBoxModificarIdReserva.SelectedItem;

            // Llamar al método para obtener los detalles de la reserva
            await ObtenerReservaParaModificar(numReserva);
        }

        private async void btnModificarUser_Click(object sender, EventArgs e)
        {
            // Verificar que los campos no estén vacíos
            if (string.IsNullOrWhiteSpace(txtModificarIdCliente.Text) ||
                string.IsNullOrWhiteSpace(txtTotalReserva.Text) ||
                cmBoxModificarIdReserva.SelectedItem == null)
            {
                MessageBox.Show("Por favor, completa todos los campos.");
                return;
            }

            // Validar que el ID del cliente es un número válido
            int numCliente;
            if (!int.TryParse(txtModificarIdCliente.Text, out numCliente))
            {
                MessageBox.Show("El ID del cliente no es válido.");
                return;
            }

            // Eliminar el símbolo de $ y cualquier otro símbolo de moneda del total de la reserva
            string totalReservaTexto = txtTotalReserva.Text.Replace("$", "").Replace(",", "").Trim();

            // Validar que el total de la reserva es un número decimal válido
            decimal totalReserva;
            if (!decimal.TryParse(totalReservaTexto, out totalReserva))
            {
                MessageBox.Show("El total de la reserva no es válido.");
                return;
            }

            // Crear el objeto de la reserva con los datos modificados
            var reservaModificada = new ClaseReservas
            {
                NumCliente = numCliente,
                FechaEntrada = dtPickerModificarFechaEntrada.Value,
                FechaSalida = dtPickerModificarFechaSalida.Value,
                EstadoReserva = txtModificarEstadoReserva.Text,
                TotalReserva = totalReserva,
                ComentarioReserva = txtModificarComentarioReserva.Text
            };

            // Obtener el ID de la reserva seleccionada
            int numReserva = (int)cmBoxModificarIdReserva.SelectedItem;

            // Llamar al método para actualizar la reserva
            bool resultado = await ActualizarReservaApi(numReserva, reservaModificada);

            if (resultado)
            {
                // Limpiar los campos y actualizar el DataGrid
                MessageBox.Show("Reserva modificada exitosamente!");

                cmBoxModificarIdReserva.SelectedIndex = -1;
                txtModificarIdCliente.Clear();
                dtPickerModificarFechaEntrada.Value = DateTime.Now;
                dtPickerModificarFechaSalida.Value = DateTime.Now;
                txtModificarFechaCreada.Clear();
                txtModificarEstadoReserva.Clear();
                txtTotalReserva.Clear();
                txtModificarComentarioReserva.Clear();

                // Cerrar el GroupBox
                gbModificarReserva.Visible = false;
                this.reservasTableAdapter.Fill(this.dbMotelDataSet1.Reservas); // Actualizar el DataGrid con los nuevos datos
            }
            else
            {
                MessageBox.Show("Hubo un error al modificar la reserva.");
            }
        }

        private void btnCancelarModificar_Click(object sender, EventArgs e)
        {
            // Limpiar los campos
            cmBoxModificarIdReserva.SelectedIndex = -1;
            txtModificarIdCliente.Clear();
            dtPickerModificarFechaEntrada.Value = DateTime.Now;
            dtPickerModificarFechaSalida.Value = DateTime.Now;
            txtModificarFechaCreada.Clear();
            txtModificarEstadoReserva.Clear();
            txtTotalReserva.Clear();
            txtModificarComentarioReserva.Clear();

            // Cerrar el GroupBox
            gbModificarReserva.Visible = false;
        }
    }
}
