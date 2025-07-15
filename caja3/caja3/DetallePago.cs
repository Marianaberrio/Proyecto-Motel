using Newtonsoft.Json;
using QuestPDF.Fluent;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;

namespace caja3
{
    public partial class DetallePago : Form
    {

        private int _numPago;
       
        public DetallePago(int numPago = -1)
        {
            InitializeComponent();

            _numPago = numPago;

        }

        public class Pagos
        {
            public int NumPago { get; set; }
            public int NumReserva { get; set; }
            public decimal MontoPago { get; set; }
            public DateTime FechaPago { get; set; }
            public string MetodoPago { get; set; }
            public string EstadoPago { get; set; }
            public string ComentarioPago { get; set; }
        }

        private async Task<int> ObtenerUltimoPagoAsync()
        {
            string connectionString = "Data Source=LAPTOP-7C7NP3J4\\SQLEXPRESS;Initial Catalog=dbMotel;Integrated Security=True;";
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    await conn.OpenAsync();

                    string query = "SELECT TOP 1 NumPago FROM tblPago ORDER BY NumPago DESC";

                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        object result = await cmd.ExecuteScalarAsync();
                        return result != null ? Convert.ToInt32(result) : -1;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al obtener el último pago: {ex.Message}");
                return -1;
            }
        }

        private async Task CargarDetallesPagoAsync(int numPago)
        {
            string connectionString = "Data Source=LAPTOP-7C7NP3J4\\SQLEXPRESS;Initial Catalog=dbMotel;Integrated Security=True;";
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    await conn.OpenAsync();

                    string query = @"SELECT NumPago, NumReserva, MontoPago, FechaPago, MetodoPago, EstadoPago, ComentarioPago 
                                     FROM Pagos
                                     WHERE NumPago = @NumPago";

                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@NumPago", numPago);

                        using (SqlDataReader reader = await cmd.ExecuteReaderAsync())
                        {
                            if (await reader.ReadAsync())
                            {
                                numpagotxt.Text = reader["NumPago"].ToString();
                                numreservatxt.Text = reader["NumReserva"].ToString();
                                montopagadotxt.Text = Convert.ToDecimal(reader["MontoPago"]).ToString("C");
                                fechapagotxt.Text = Convert.ToDateTime(reader["FechaPago"]).ToString("yyyy-MM-dd");
                                metodopagotxt.Text = reader["MetodoPago"].ToString();
                               
                            }
                            else
                            {
                                MessageBox.Show("No se encontraron detalles para ese número de pago.");
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al cargar detalles del pago: {ex.Message}");
            }
        }


        private async void DetallePago_Load(object sender, EventArgs e)
        {
            await CargarDetallesPagoAsync(_numPago);
        }

        private void button1_Click(object sender, EventArgs e)
        {


            QuestPDF.Settings.License = QuestPDF.Infrastructure.LicenseType.Community;
            var document = Document.Create(container =>
            {
                container.Page(page =>
                {
                    page.Margin(30);
                    page.Content().Column(col =>
                    {
                        col.Item().Text("Recibo de Pago").FontSize(20).Bold();
                        col.Item().Text($"Pago #: {numpagotxt.Text}");
                        col.Item().Text($"Fecha: {fechapagotxt.Text}");
                        col.Item().Text($"Método: {metodopagotxt.Text}");
                        col.Item().Text($"Monto: {montopagadotxt.Text}");
                       
                    });
                });
            });

            // Crear el stream manualmente SIN usar var
            MemoryStream stream = new MemoryStream();
            document.GeneratePdf(stream);

            // Diálogo para guardar el archivo
            SaveFileDialog saveFileDialog = new SaveFileDialog
            {
                Filter = "PDF Files|*.pdf",
                Title = "Guardar Reporte de Pago",
                FileName = $"Pago_{numpagotxt.Text}.pdf"
            };

            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                File.WriteAllBytes(saveFileDialog.FileName, stream.ToArray());
                MessageBox.Show("PDF generado y guardado correctamente.");
            }

        }

        // Form Load event - Fetch payment details using static NumPago


        // Método que se ejecuta cuando el formulario se carga



    }
}
