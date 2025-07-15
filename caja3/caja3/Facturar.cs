using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Net.Http;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Newtonsoft.Json;


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
                    string url = "http://localhost:5264/api/Reservas/ids"; // URL de la API para obtener los IDs de las reservas

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
                    string url = $"http://localhost:5264/api/Reservas/{numReserva}";

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
            string connectionString = "Data Source=LAPTOP-7C7NP3J4\\SQLEXPRESS;Initial Catalog=dbMotel;Integrated Security=True;";

            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    await conn.OpenAsync();

                    using (SqlCommand cmd = new SqlCommand("RealizarPago", conn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@NumReserva", numReserva);
                        cmd.Parameters.AddWithValue("@MontoPago", montoPago);
                        cmd.Parameters.AddWithValue("@MetodoPago", metodoPago);
                        cmd.Parameters.AddWithValue("@EstadoPago", "Activo");

                        await cmd.ExecuteNonQueryAsync();
                        return true;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al realizar el pago: {ex.Message}");
                return false;
            }
        }

        private async Task<int?> ObtenerUltimoNumPagoAsync(int numReserva)
        {
            string connectionString = "Data Source=LAPTOP-7C7NP3J4\\SQLEXPRESS;Initial Catalog=dbMotel;Integrated Security=True;";

            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    await conn.OpenAsync();

                    string query = @"SELECT TOP 1 NumPago 
                             FROM Pagos 
                             WHERE NumReserva = @NumReserva 
                             ORDER BY FechaPago DESC";

                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@NumReserva", numReserva);

                        var result = await cmd.ExecuteScalarAsync();

                        if (result != null)
                        {
                            return Convert.ToInt32(result);
                        }
                        else
                        {
                            return null;
                        }
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
           

            // Limpieza del texto: elimina todo lo que no sea número o punto decimal
            string montoLimpio = new string(textoMonto.Where(c => char.IsDigit(c) || c == '.').ToArray());
            
            decimal montoPago = Convert.ToDecimal(montoLimpio);




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
