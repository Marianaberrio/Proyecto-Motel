namespace Motel.Integracion.Servicios
{
    public class ServicioResponse
    {
        public int NumServicio { get; set; }
        public string NombreServicio { get; set; } = string.Empty;
        public string? DescripcionServicio { get; set; }
        public decimal PrecioServicio { get; set; }
    }
}
