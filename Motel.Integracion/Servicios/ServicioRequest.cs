namespace Motel.Integracion.Servicios
{
    public class ServicioRequest
    {
        public string NombreServicio { get; set; } = string.Empty;
        public string? DescripcionServicio { get; set; }
        public decimal PrecioServicio { get; set; }
    }
}
