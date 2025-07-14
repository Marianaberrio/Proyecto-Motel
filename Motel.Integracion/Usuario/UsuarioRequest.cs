namespace Motel.Integracion.Requests
{
    public class UsuarioRequest
    {
        public string Usuario { get; set; } = string.Empty;
        public string Contraseña { get; set; } = string.Empty;
        public string Rol { get; set; } = string.Empty;
    }
}
