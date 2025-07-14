namespace Motel.Integracion.Clientes
{
    public class ClienteRequest
    {
        public int NumCliente { get; set; }  // Solo se usará en PUT
        public string NombreCliente { get; set; }
        public string ApellidoCliente { get; set; }
        public string CorreoCliente { get; set; }
        public string TelefonoCliente { get; set; }
        public DateTime FechaNacimiento { get; set; }
    }
}
