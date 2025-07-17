using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace caja3
{
    public partial class Facturar : Form
    {
        public Facturar()
        {
            InitializeComponent();
        }

        private async void Facturar_Load(object sender, EventArgs e)
        {
            await CargarNumReservasAsync();

            // Cargar métodos de pago al iniciar el formulario
            metodopagoCombo.Items.Clear();
            metodopagoCombo.Items.Add("Efectivo");
            metodopagoCombo.Items.Add("Tarjeta");
            metodopagoCombo.Items.Add("Transferencia");
            metodopagoCombo.SelectedIndex = 0; // Selecciona el primero por defecto
        }

        private async void numreservacombo_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (numreservacombo.SelectedItem != null)
            {
                int numReserva = Convert.ToInt32(numreservacombo.SelectedItem);
                await ObtenerMontoReservaAsync(numReserva);

            }
        }

        private async Task CargarNumReservasAsync()
        {
            try
            {
                using (var client = new HttpClient())
                {
                    string url = "https://localhost:7013/api/Reservas/ids"; // URL de la API para obtener los IDs de las reservas

                    var response = await client.GetAsync(url);
                    if (response.IsSuccessStatusCode)
                    {
                        // Obtener respuesta de IDs de reservas
                        var jsonResponse = await response.Content.ReadAsStringAsync();

                        // Deserializar la respuesta
                        var idsReservas = JsonConvert.DeserializeObject<List<int>>(jsonResponse);

                        // Cargar los IDs de las reservas en el ComboBox
                        numreservacombo.DataSource = idsReservas;

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

        private void metodopagoCombo_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
        public class Reservas
        {
            public int NumReserva { get; set; }          // ID de la reserva (autoincremental)
            public int NumCliente { get; set; }          // ID del cliente relacionado con la reserva
            public DateTime FechaReserva { get; set; }  // Fecha de la reserva
            public DateTime FechaEntrada { get; set; }  // Fecha de entrada para la reserva
            public DateTime FechaSalida { get; set; }   // Fecha de salida para la reserva
            public string EstadoReserva { get; set; }   // Estado de la reserva (ej. 'Reservado', 'Cancelado', etc.)
            public decimal TotalReserva { get; set; }   // Total del monto de la reserva
            public string ComentarioReserva { get; set; } // Comentarios de la reserva (opcional)
        }

        private async Task ObtenerMontoReservaAsync(int numReserva)
        {
            try
            {
                using (var client = new HttpClient())
                {
                    string url = $"https://localhost:7013/api/Reservas/{numReserva}";

                    var response = await client.GetAsync(url);
                    if (response.IsSuccessStatusCode)
                    {
                        var json = await response.Content.ReadAsStringAsync();
                        // MessageBox.Show(json);  // Verifica la respuesta JSON aquí

                        // Deserializar la respuesta JSON en un objeto de tipo Reservas
                        var reserva = JsonConvert.DeserializeObject<Reservas>(json);

                        if (reserva != null)
                        {
                            // Mostrar el monto de la reserva con formato de moneda
                            montototaltxt.Text = reserva.TotalReserva.ToString("C"); // Formato RD$
                        }
                    }
                    else
                    {
                        MessageBox.Show("No se pudo obtener el monto de la reserva.");
                        montototaltxt.Text = "";
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al consultar el monto: {ex.Message}");
            }
        }


        private void montototaltxt_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }
        private async Task<bool> RealizarPagoAsync(int numReserva, decimal montoPago, string metodoPago)
        {
            try
            {
                using (var client = new HttpClient())
                {
                    var pagoRequest = new
                    {
                        NumReserva = numReserva,
                        MontoPago = montoPago,
                        FechaPago = DateTime.Now,
                        MetodoPago = metodoPago,
                        EstadoPago = "Activo",
                        ComentarioPago = "" 
                    };

                    var response = await client.PostAsJsonAsync("https://localhost:7013/api/Pagos", pagoRequest);
                    return response.IsSuccessStatusCode;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al registrar el pago: {ex.Message}");
                return false;
            }
        }
        public class PagoResponse
        {
            public int NumPago { get; set; }
            public int NumReserva { get; set; }
            public decimal MontoPago { get; set; }
            public DateTime FechaPago { get; set; }
            public string MetodoPago { get; set; }
            public string EstadoPago { get; set; }
            public string ComentarioPago { get; set; }
        }

        private async Task<int?> ObtenerUltimoNumPagoAsync(int numReserva)
        {
            try
            {
                using (var client = new HttpClient())
                {
                    string url = $"https://localhost:7013/api/Pagos/reserva/{numReserva}";

                    var response = await client.GetAsync(url);
                    if (response.IsSuccessStatusCode)
                    {
                        var json = await response.Content.ReadAsStringAsync();
                        var pago = JsonConvert.DeserializeObject<PagoResponse>(json);

                        return pago?.NumPago;
                    }
                    else
                    {
                        return null;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al obtener el número de pago: {ex.Message}");
                return null;
            }
        }



        private async void facturarbtn_Click(object sender, EventArgs e)
        {
            int numReserva = Convert.ToInt32(numreservacombo.SelectedItem);
            string textoMonto = montototaltxt.Text;

            string montoLimpio = new string(textoMonto.Where(c => char.IsDigit(c) || c == '.' || c == ',').ToArray());

            // Reemplazar coma por punto si es necesario (por configuración regional)
            montoLimpio = montoLimpio.Replace(',', '.');

            decimal montoPago;
            bool esDecimal = decimal.TryParse(montoLimpio, System.Globalization.NumberStyles.Any, System.Globalization.CultureInfo.InvariantCulture, out montoPago);

            if (!esDecimal)
            {
                MessageBox.Show("Por favor, ingresa un monto válido (número sin letras).", "Monto inválido", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }



            string metodoPago = metodopagoCombo.SelectedItem.ToString();

            bool pagoRealizado = await RealizarPagoAsync(numReserva, montoPago, metodoPago);



            if (pagoRealizado)
            {
                int? numPago = await ObtenerUltimoNumPagoAsync(numReserva);

                if (numPago.HasValue)
                {
                    MessageBox.Show($"Pago realizado correctamente. Número de pago: {numPago.Value}");

                    // Aquí puedes abrir el formulario de DetallePago y pasarle el numPago
                    var detalle = new DetallePago(numPago.Value);
                    detalle.Show();
                }
                else
                {
                    MessageBox.Show("Pago realizado, pero no se pudo obtener el número de pago.");
                }
            }
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }
    }
}
