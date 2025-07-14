namespace Motel.Integracion.Reservas
{
    public class ReservaRequest
    {
        public int NumCliente { get; set; }
        public DateTime FechaEntrada { get; set; }
        public DateTime FechaSalida { get; set; }
        public string EstadoReserva { get; set; } = string.Empty;
        public decimal TotalReserva { get; set; }
        public string? ComentarioReserva { get; set; }
    }
}
