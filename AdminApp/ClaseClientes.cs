using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdminApp
{
    public class ClaseClientes
    {
        public int NumCliente { get; set; }
        public string NombreCliente { get; set; }
        public string ApellidoCliente { get; set; }
        public string CorreoCliente { get; set; }
        public string TelefonoCliente { get; set; }
        public DateTime FechaNacimiento { get; set; }

        public DateTime FechaRegistro { get; set; }
    }
}
