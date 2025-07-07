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
using System.Threading.Tasks;
using System.Windows.Forms;
namespace AdminApp
{
    public partial class Habitaciones : Form
    {
        public Habitaciones()
        {
            InitializeComponent();
        }

        public async Task<bool> AgregarHabitacionAsync(ClaseHabitacion habitacion)
        {
            using (var client = new HttpClient())
            {
                try
                {
                    // Asignar el estado "Disponible" a la habitación
                    habitacion.EstadoHabitacion = "Disponible"; // Agregar estado

                    // URL de la API que maneja el agregar habitación
                    string url = "http://localhost:5264/api/habitaciones"; // Puerto correcto

                    // Convertir el objeto ClaseHabitacion a JSON
                    var content = new StringContent(JsonConvert.SerializeObject(habitacion), Encoding.UTF8, "application/json");

                    // Hacer una solicitud POST a la API
                    var response = await client.PostAsync(url, content);

                    // Verificar si la solicitud fue exitosa (código 2xx)
                    if (response.IsSuccessStatusCode)
                    {
                        MessageBox.Show("Habitación agregada correctamente.");
                        return true;
                    }
                    else
                    {
                        // Obtener el contenido de la respuesta en caso de error
                        var errorContent = await response.Content.ReadAsStringAsync();
                        MessageBox.Show($"Error al agregar la habitación. Código: {response.StatusCode}, Detalle: {errorContent}");
                        return false;
                    }
                }
                catch (Exception ex)
                {
                    // Capturar cualquier error en la solicitud
                    MessageBox.Show($"Error: {ex.Message}");
                    return false;
                }
            }
        }

        public async Task<bool> VerificarNumeroHabitacionAsync(string numeroHabitacion)
        {
            using (var client = new HttpClient())
            {
                try
                {
                    // URL para verificar si la habitación existe (usando el GET existente)
                    string url = $"http://localhost:5264/api/habitaciones/{numeroHabitacion}"; // Cambia esta URL según tu configuración

                    // Hacer la solicitud GET a la API
                    var response = await client.GetAsync(url);

                    // Si la respuesta es exitosa, significa que la habitación existe
                    if (response.IsSuccessStatusCode)
                    {
                        // Si la habitación existe, la API devolverá los datos de la habitación
                        var resultado = await response.Content.ReadAsStringAsync();
                        return true;  // La habitación ya existe
                    }
                    else
                    {
                        return false;  // La habitación no existe, puede ser agregada
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error en la solicitud HTTP: {ex.Message}");
                    return false;
                }
            }
        }

        public async Task<bool> ActualizarHabitacionAsync(ClaseHabitacion habitacion)
        {
            using (var client = new HttpClient())
            {
                try
                {
                    string url = $"http://localhost:5264/api/habitaciones/{habitacion.NumHabitacion}";
                    var content = new StringContent(JsonConvert.SerializeObject(habitacion), Encoding.UTF8, "application/json");

                    var response = await client.PutAsync(url, content);

                    return response.IsSuccessStatusCode;  // Verificar que la respuesta sea exitosa
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error al modificar la habitación: {ex.Message}");
                    return false;
                }
            }
        }

        // Método para buscar la habitación por número
        private async Task<ClaseHabitacion> BuscarHabitacionPorNumero(string numHabitacion)
        {
            using (var client = new HttpClient())
            {
                string url = $"http://localhost:5264/api/habitaciones/{numHabitacion}";
                var response = await client.GetAsync(url);

                if (response.IsSuccessStatusCode)
                {
                    var resultado = await response.Content.ReadAsStringAsync();
                    return JsonConvert.DeserializeObject<ClaseHabitacion>(resultado);  // Deserializa la habitación desde la respuesta
                }

                return null;  // Si no se encuentra
            }
        }

        // Método para buscar la habitación por ID
        // Método para buscar por ID
        private async Task<ClaseHabitacion> BuscarHabitacionPorID(string idHabitacion)
        {
            using (var client = new HttpClient())
            {
                try
                {
                    // Asegúrate de que la URL sea correcta
                    string url = $"http://localhost:5264/api/habitaciones/byid/{idHabitacion}";
                    var response = await client.GetAsync(url);

                    if (response.IsSuccessStatusCode)
                    {
                        var resultado = await response.Content.ReadAsStringAsync();
                        return JsonConvert.DeserializeObject<ClaseHabitacion>(resultado); // Deserializa la habitación
                    }

                    return null;  // Si no se encuentra
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error al buscar la habitación: {ex.Message}");
                    return null;
                }
            }
        }



        public async Task<ClaseHabitacion> BuscarHabitacionAsync(string numHabitacion, int? idHabitacion)
        {
            using (var client = new HttpClient())
            {
                try
                {
                    // Construir la URL para la API con los parámetros de búsqueda
                    string url = $"http://localhost:5264/api/habitaciones?numHabitacion={numHabitacion}&idHabitacion={idHabitacion}";

                    // Hacer la solicitud GET a la API
                    var response = await client.GetAsync(url);

                    // Si la respuesta es exitosa, deserializamos los datos
                    if (response.IsSuccessStatusCode)
                    {
                        var result = await response.Content.ReadAsStringAsync();
                        return JsonConvert.DeserializeObject<ClaseHabitacion>(result);
                    }

                    // Si no se encuentra, devolvemos null
                    return null;
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error al buscar la habitación: {ex.Message}");
                    return null;
                }
            }
        }


        public async Task<bool> EliminarHabitacionAsync(string numHabitacion)
        {
            using (var client = new HttpClient())
            {
                try
                {
                    // URL para eliminar la habitación
                    string url = $"http://localhost:5264/api/habitaciones/{numHabitacion}"; // Cambiar según tu configuración

                    // Hacer una solicitud DELETE a la API
                    var response = await client.DeleteAsync(url);

                    // Verificar si la respuesta fue exitosa
                    return response.IsSuccessStatusCode;
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error al eliminar la habitación: {ex.Message}");
                    return false;
                }
            }
        }


        private void Habitaciones_Load(object sender, EventArgs e)
        {
            // TODO: This line of code loads data into the 'dbMotelHabitacionesDataSet.Habitaciones' table. You can move, or remove it, as needed.
            this.habitacionesTableAdapter.Fill(this.dbMotelHabitacionesDataSet.Habitaciones);

        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            gbAgregarHabitacion.Visible = false;
        }

        private void btnAgregar_Click(object sender, EventArgs e)
        {
            gbAgregarHabitacion.Visible = false;
        }

        private void btnAgregarHabitacion_Click(object sender, EventArgs e)
        {
            gbAgregarHabitacion.Visible = true;
        }

        private void btnModificarHabitacion_Click(object sender, EventArgs e)
        {
            gbModificarHabitacion.Visible = true;
        }

        private void btnEliminarHabitacion_Click(object sender, EventArgs e)
        {

            gbEliminarHabitación.Visible = true;
        }

        private void btnCancelar_Click_1(object sender, EventArgs e)
        {
            gbAgregarHabitacion.Visible = false;

        }

        private async void btnAgregar_Click_1(object sender, EventArgs e)
        {
            // Validar que los campos no estén vacíos
            if (string.IsNullOrWhiteSpace(txtBoxnumhabitacion.Text) ||
                string.IsNullOrWhiteSpace(txtBoxtipohabitacion.Text) ||
                string.IsNullOrWhiteSpace(txtBoxPreciohabitacion.Text) ||
                string.IsNullOrWhiteSpace(txtBoxCapacidadHabitacion.Text))  // Verificar que la capacidad no esté vacía
            {
                MessageBox.Show("Por favor llene todos los campos");
                return;
            }

            // Validar que la capacidad sea un número válido
            if (!int.TryParse(txtBoxCapacidadHabitacion.Text, out int capacidad))
            {
                MessageBox.Show("La capacidad debe ser un número válido.");
                return;
            }

            // Verificar si el número de habitación ya existe
            bool existe = await VerificarNumeroHabitacionAsync(txtBoxnumhabitacion.Text);
            if (existe)
            {
                MessageBox.Show("Este número de habitación ya existe. Por favor ingrese un número diferente.");
                return;
            }

            // Crear un nuevo objeto de Habitacion con los valores de los campos
            var habitacion = new ClaseHabitacion
            {
                NumHabitacion = txtBoxnumhabitacion.Text,
                TipoHabitacion = txtBoxtipohabitacion.Text,
                PrecioHabitacion = decimal.Parse(txtBoxPreciohabitacion.Text),  // Asegúrate de que el campo sea decimal
                CapacidadHabitacion = capacidad  // Usar la capacidad validada
            };

            // Llamar al método para agregar la habitación a la base de datos a través de la API
            bool resultado = await AgregarHabitacionAsync(habitacion);

            // Si el resultado es exitoso, puedes limpiar los campos o hacer otras acciones
            if (resultado)
            {
                // Opcional: Limpiar los campos después de agregar
                txtBoxnumhabitacion.Clear();
                txtBoxtipohabitacion.Clear();
                txtBoxPreciohabitacion.Clear();
                txtBoxCapacidadHabitacion.Clear();  // Limpiar el campo de capacidad
                gbAgregarHabitacion.Visible = false;  // Cerrar el formulario de agregar habitación
                this.habitacionesTableAdapter.Fill(this.dbMotelHabitacionesDataSet.Habitaciones);

            }



        }

        private async void btnModificar_Click(object sender, EventArgs e)
        {
            // Confirmar con el usuario antes de modificar
            var confirmResult = MessageBox.Show($"¿Está seguro de que quiere hacerle los cambios a la habitación {txtModificarNum.Text}?",
                                                "Confirmar modificación",
                                                MessageBoxButtons.YesNo);
            if (confirmResult == DialogResult.No)
            {
                return;  // No hacer nada si el usuario selecciona No
            }

            // Crear un objeto de la habitación con los nuevos valores
            var habitacionModificada = new ClaseHabitacion
            {
                NumHabitacion = txtModificarNum.Text,
                TipoHabitacion = txtModificarTipo.Text,
                PrecioHabitacion = decimal.Parse(txtModificarPrecio.Text),
                CapacidadHabitacion = int.Parse(txtCapacidadHabitacion.Text),
                EstadoHabitacion = txtModificarEstado.Text
            };

            // Llamar al método para actualizar la habitación en la API
            bool resultado = await ActualizarHabitacionAsync(habitacionModificada);

            if (resultado)
            {
                // Recargar los datos y ocultar el formulario de modificar
                this.habitacionesTableAdapter.Fill(this.dbMotelHabitacionesDataSet.Habitaciones);
                gbModificarHabitacion.Visible = false;
                MessageBox.Show("Habitación actualizada correctamente.");

                txtModificarNum.Clear();
                txtModificarTipo.Clear();
                txtModificarPrecio.Clear();
                txtCapacidadHabitacion.Clear();
                txtModificarEstado.Clear();

                // Limpiar el campo de búsqueda (si es necesario)
                TxtBoxBuscarNumHab.Clear();
                txtBoxBuscarIDhabitacion.Clear();

                btnNumHab.Checked = false;
                btnIdHab.Checked = false;
            }
            else
            {
                MessageBox.Show("Error al actualizar la habitación.");
            }
        }

        private void btnCancelarModificar_Click(object sender, EventArgs e)
        {
            gbModificarHabitacion.Visible = false;
            txtModificarNum.Clear();
            txtModificarTipo.Clear();
            txtModificarPrecio.Clear();
            txtCapacidadHabitacion.Clear();
            txtModificarEstado.Clear();

            // Limpiar el campo de búsqueda (si es necesario)
            TxtBoxBuscarNumHab.Clear();
            txtBoxBuscarIDhabitacion.Clear();

            btnNumHab.Checked = false;
            btnIdHab.Checked = false;
        }

        private void btnNumHab_CheckedChanged(object sender, EventArgs e)
        {
            if (btnNumHab.Checked)
            {
                txtBoxBuscarIDhabitacion.Enabled = false; // Deshabilitar campo ID
                TxtBoxBuscarNumHab.Enabled = true; // Habilitar campo Número
            }
        }

        private void btnIdHab_CheckedChanged(object sender, EventArgs e)
        {
            if (btnIdHab.Checked)
            {
                TxtBoxBuscarNumHab.Enabled = false; // Deshabilitar campo Número
                txtBoxBuscarIDhabitacion.Enabled = true; // Habilitar campo ID
            }
        }

        private async void buscarNum_Click(object sender, EventArgs e)
        {
            // Verificar que el campo de búsqueda no esté vacío
            if (string.IsNullOrWhiteSpace(TxtBoxBuscarNumHab.Text) && string.IsNullOrWhiteSpace(txtBoxBuscarIDhabitacion.Text))
            {
                MessageBox.Show("Por favor, ingrese un número de habitación o un ID.");
                return;
            }

            ClaseHabitacion habitacion = null;

            // Buscar según el tipo de selección (número o ID)
            if (btnNumHab.Checked)
            {
                string numHabitacion = TxtBoxBuscarNumHab.Text;
                habitacion = await BuscarHabitacionPorNumero(numHabitacion); // Llamar a un método para buscar por número
            }
            else if (btnIdHab.Checked)
            {
                string idHabitacion = txtBoxBuscarIDhabitacion.Text;
                habitacion = await BuscarHabitacionPorID(idHabitacion); // Llamar a un método para buscar por ID
            }

            // Si no encontramos la habitación, mostrar un mensaje
            if (habitacion == null)
            {
                MessageBox.Show("No se encontró la habitación solicitada.");
                return;
            }

            // Cargar los datos de la habitación encontrada en los campos correspondientes
            txtModificarNum.Text = habitacion.NumHabitacion;
            txtModificarTipo.Text = habitacion.TipoHabitacion;
            txtModificarPrecio.Text = habitacion.PrecioHabitacion.ToString();
            txtCapacidadHabitacion.Text = habitacion.CapacidadHabitacion.ToString();
            txtModificarEstado.Text = habitacion.EstadoHabitacion;
        }

        private void btneliminarnumhab_CheckedChanged(object sender, EventArgs e)
        {
            if (btneliminarnumhab.Checked)
            {
                txtEliminarhabID.Enabled = false;  // Deshabilitar el campo para ID
                txteliminarhabnum.Enabled = true;  // Habilitar el campo para número
            }
        }

        private void btneliminarIDhab_CheckedChanged(object sender, EventArgs e)
        {
            if (btneliminarIDhab.Checked)
            {
                txteliminarhabnum.Enabled = false;  // Deshabilitar el campo para número
                txtEliminarhabID.Enabled = true;  // Habilitar el campo para ID
            }
        }

        private void btnCancelarEliminar_Click(object sender, EventArgs e)
        {
            // Limpiar los campos de búsqueda
            txteliminarhabnum.Clear();
            txtEliminarhabID.Clear();

            // Resetear los botones de búsqueda
            btneliminarnumhab.Checked = false;
            btneliminarIDhab.Checked = false;

            // Cerrar el GroupBox de eliminación
            gbEliminarHabitación.Visible = false;

        }

        private async void btnEliminar_Click(object sender, EventArgs e)
        {
            // Verificar que al menos uno de los campos de búsqueda tenga un valor
            if (string.IsNullOrWhiteSpace(txteliminarhabnum.Text) && string.IsNullOrWhiteSpace(txtEliminarhabID.Text))
            {
                MessageBox.Show("Por favor, ingrese un número de habitación o un ID.");
                return;
            }

            ClaseHabitacion habitacion = null;

            // Buscar según el tipo de selección (número o ID)
            if (btneliminarnumhab.Checked)
            {
                string numHabitacion = txteliminarhabnum.Text;
                habitacion = await BuscarHabitacionPorNumero(numHabitacion); // Llamar al método para buscar por número
            }
            else if (btneliminarIDhab.Checked)
            {
                string idHabitacion = txtEliminarhabID.Text;
                habitacion = await BuscarHabitacionPorID(idHabitacion); // Llamar al método para buscar por ID
            }

            // Si no encontramos la habitación, mostrar un mensaje
            if (habitacion == null)
            {
                MessageBox.Show("No se encontró la habitación.");
                return;
            }

            // Confirmación de eliminación
            var confirmResult = MessageBox.Show($"¿Estás seguro de que deseas eliminar la habitación {habitacion.NumHabitacion}?",
                                                "Confirmar eliminación",
                                                MessageBoxButtons.YesNo);
            if (confirmResult == DialogResult.No)
            {
                return;  // Si el usuario selecciona No, no eliminamos nada
            }

            // Llamar al método para eliminar la habitación de la base de datos
            bool resultado = await EliminarHabitacionAsync(habitacion.NumHabitacion);

            if (resultado)
            {
                MessageBox.Show("Habitación eliminada correctamente.");

                // Limpiar los campos de búsqueda
                txteliminarhabnum.Clear();
                txtEliminarhabID.Clear();

                // Resetear los botones de búsqueda
                btneliminarnumhab.Checked = false;
                btneliminarIDhab.Checked = false;

                // Cerrar el GroupBox de eliminación
                gbEliminarHabitación.Visible = false;

                this.habitacionesTableAdapter.Fill(this.dbMotelHabitacionesDataSet.Habitaciones);

            }
            else
            {
                MessageBox.Show("Error al eliminar la habitación.");
            }

        }

        private void btnBuscarHabitacion_Click(object sender, EventArgs e)
        {
            gbBuscarHabitacion.Visible = true;
        }

        private void btnSalir_Click(object sender, EventArgs e)
        {
            // Limpiar los campos de búsqueda
            txtBuscarNumHab.Clear();
            txtBuscarIDHab.Clear();
            txtBuscarNumeroHabitacion.Clear();
            txtBuscarTipoHabitacion.Clear();
            txtBuscarPrecioHabitacion.Clear();
            txtBuscarCapacidadHabitacion.Clear();
            txtBuscarEstadoHabitacion.Clear();

            // Deseleccionar los botones de búsqueda
            btnBucarNumHab.Checked = false;
            btnBuscarIDHab.Checked = false;

            // Cerrar el GroupBox
            gbBuscarHabitacion.Visible = false;
        }

        private void btnBucarNumHab_CheckedChanged(object sender, EventArgs e)
        {
            if (btnBucarNumHab.Checked)
            {
                txtBuscarIDHab.Enabled = false;  // Deshabilitar el campo para ID
                txtBuscarNumHab.Enabled = true;  // Habilitar el campo para número
            }
        }

        private void btnBuscarIDHab_CheckedChanged(object sender, EventArgs e)
        {
            if (btnBuscarIDHab.Checked)
            {
                txtBuscarNumHab.Enabled = false;  // Deshabilitar el campo para número
                txtBuscarIDHab.Enabled = true;  // Habilitar el campo para ID
            }
        }

        private async void btnBuscarHab_Click(object sender, EventArgs e)
        {
            // Verificar que al menos uno de los campos de búsqueda tenga un valor
            if (string.IsNullOrWhiteSpace(txtBuscarNumHab.Text) && string.IsNullOrWhiteSpace(txtBuscarIDHab.Text))
            {
                MessageBox.Show("Por favor, ingrese un número de habitación o un ID.");
                return;
            }

            ClaseHabitacion habitacion = null;

            // Buscar según el tipo de selección (número o ID)
            string numHabitacion = txtBuscarNumHab.Text;
            int? idHabitacion = null;

            if (btnBucarNumHab.Checked)
            {
                // Buscar por número de habitación
                habitacion = await BuscarHabitacionPorNumero(numHabitacion);
            }
            else if (btnBuscarIDHab.Checked)
            {
                // Buscar por ID de habitación
                if (!int.TryParse(txtBuscarIDHab.Text, out int id))
                {
                    MessageBox.Show("Por favor, ingrese un ID válido.");
                    return; // Si el ID no es válido, mostramos un mensaje y salimos
                }
                idHabitacion = id;
                habitacion = await BuscarHabitacionPorID(idHabitacion.ToString()); // Convertimos el ID a string si es necesario
            }

            // Si no encontramos la habitación, mostrar un mensaje
            if (habitacion == null)
            {
                MessageBox.Show("No se encontró la habitación.");
                return;
            }

            // Mostrar los datos en los TextBoxes correspondientes
            txtBuscarNumeroHabitacion.Text = habitacion.NumHabitacion;
            txtBuscarTipoHabitacion.Text = habitacion.TipoHabitacion;
            txtBuscarPrecioHabitacion.Text = habitacion.PrecioHabitacion.ToString();
            txtBuscarCapacidadHabitacion.Text = habitacion.CapacidadHabitacion.ToString();
            txtBuscarEstadoHabitacion.Text = habitacion.EstadoHabitacion;
        }

        private void habitacionesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // Redirigir al dashboard de habitaciones
            var mainForm = new DashboardForm();
            mainForm.Show();
            this.Hide();
        }

        private void clientesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // Redirigir al dashboard de habitaciones
            var mainForm = new Clientes();
            mainForm.Show();
            this.Hide();
        }

        private void reservasToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // Redirigir al dashboard de habitaciones
            var mainForm = new Reservas();
            mainForm.Show();
            this.Hide();
        }

        private void serviciosToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // Redirigir al dashboard de habitaciones
            var mainForm = new Servicios();
            mainForm.Show();
            this.Hide();
        }

        private void usuariosToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // Redirigir al dashboard de habitaciones
            var mainForm = new Usuarios();
            mainForm.Show();
            this.Hide();
        }

        private void pagosToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // Redirigir al dashboard de habitaciones
            var mainForm = new Pagos();
            mainForm.Show();
            this.Hide();
        }

        private void reportesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // Redirigir al dashboard de habitaciones
            var mainForm = new Reportes();
            mainForm.Show();
            this.Hide();
        }

        private void CerrarApp_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}
