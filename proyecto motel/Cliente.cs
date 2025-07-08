namespace proyecto_motel
{
    public class Cliente
    {
        public int NumCliente { get; set; }
        public string NombreCliente { get; set; }
        public string ApellidoCliente { get; set; }
        public string CorreoCliente { get; set; }
        public string TelefonoCliente { get; set; }
        public DateTime FechaNacimiento { get; set; }
        public DateTime FechaRegistro { get; set; }  // Se añadió la propiedad FechaRegistro
    }
}
