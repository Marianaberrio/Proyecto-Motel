using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdminApp
{
    internal class ClaseReservas
    {
        public int NumReserva { get; set; }
        public int NumCliente { get; set; }
        public DateTime FechaReserva { get; set; } = DateTime.Now;  // Se establece por defecto la fecha actual
        public DateTime FechaEntrada { get; set; }
        public DateTime FechaSalida { get; set; }
        public string EstadoReserva { get; set; } = "Activa";  // Estado por defecto
        public decimal TotalReserva { get; set; }
        public string ComentarioReserva { get; set; }
        public string TipoHabitacion { get; set; }  // Para saber el tipo de habitación
    }
}
