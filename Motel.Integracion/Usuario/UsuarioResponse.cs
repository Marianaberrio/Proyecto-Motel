namespace Motel.Integracion.Responses
{
    public class UsuarioResponse
    {
        public int Id { get; set; }
        public string Usuario { get; set; } = string.Empty;
        public string Contraseña { get; set; } = string.Empty;
        public string Rol { get; set; } = string.Empty;
    }
}
