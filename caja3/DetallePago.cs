using Newtonsoft.Json;
using QuestPDF.Fluent;
using QuestPDF.Infrastructure;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;
using System.Windows.Forms;

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

        // Clase Pagos que mapea los datos del pago
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

        // Método para obtener el último pago a través de la API
        private async Task<int?> ObtenerUltimoNumPagoAsync(int numReserva)
        {
            try
            {
                using (var client = new HttpClient())
                {
                    string url = $"https://localhost:7013/api/Pagos/reserva/{numReserva}";  // Llamada a la API de integración

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

        // Método que obtiene los detalles del pago desde la API
        private async Task CargarDetallesPagoAsync(int numPago)
        {
            try
            {
                using (var client = new HttpClient())
                {
                    string url = $"https://localhost:7013/api/Pagos/{numPago}";  // Llamada a la API de integración

                    var response = await client.GetAsync(url);  // Realiza la solicitud HTTP

                    if (response.IsSuccessStatusCode)
                    {
                        var json = await response.Content.ReadAsStringAsync();  // Obtiene la respuesta JSON

                        // Deserializa la respuesta en un objeto de tipo Pagos
                        var pago = JsonConvert.DeserializeObject<Pagos>(json);

                        if (pago != null)
                        {
                            // Muestra los detalles en los TextBoxes
                            numpagotxt.Text = pago.NumPago.ToString();
                            numreservatxt.Text = pago.NumReserva.ToString();
                            montopagadotxt.Text = pago.MontoPago.ToString("C");
                            fechapagotxt.Text = pago.FechaPago.ToString("yyyy-MM-dd");
                            metodopagotxt.Text = pago.MetodoPago;
                        }
                    }
                    else
                    {
                        MessageBox.Show("No se encontraron detalles para ese número de pago.");
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al cargar detalles del pago: {ex.Message}");
            }
        }

        // Método que se ejecuta cuando el formulario se carga
        private async void DetallePago_Load(object sender, EventArgs e)
        {
            await CargarDetallesPagoAsync(_numPago);
        }

        // Genera el recibo de pago en formato PDF
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
    }
}
