namespace Motel.Integracion.Pagos
{
    public class PagoRequest
    {
        public int NumReserva { get; set; }
        public decimal MontoPago { get; set; }
        public DateTime FechaPago { get; set; }
        public string MetodoPago { get; set; } = string.Empty;
        public string EstadoPago { get; set; } = string.Empty;
        public string? ComentarioPago { get; set; }
    }
}
