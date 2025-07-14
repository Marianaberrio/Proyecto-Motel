namespace Motel.Integracion.Reservas
{
    public class ReservaResponse
    {
        public int NumReserva { get; set; }
        public int NumCliente { get; set; }
        public DateTime FechaReserva { get; set; }
        public DateTime FechaEntrada { get; set; }
        public DateTime FechaSalida { get; set; }
        public string EstadoReserva { get; set; } = string.Empty;
        public decimal TotalReserva { get; set; }
        public string? ComentarioReserva { get; set; }
    }
}
