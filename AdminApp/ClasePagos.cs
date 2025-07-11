using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdminApp
{
    public class ClasePagos
    {
        public int NumPago { get; set; }

        public int NumReserva { get; set; }

        public decimal MontoPago { get; set; }

        public DateTime FechaPago { get; set; }

        public string MetodoPago { get; set; }

        public string EstadoPago { get; set; }

        public string ComentarioPago { get; set; }
    }
}
