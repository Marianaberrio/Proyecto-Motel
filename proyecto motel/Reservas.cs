namespace proyecto_motel
{
    public class Reservas
    {
        public int NumReserva { get; set; }          // ID de la reserva (autoincremental)
        public int NumCliente { get; set; }          // ID del cliente relacionado con la reserva
        public DateTime FechaReserva { get; set; }  // Fecha de la reserva
        public DateTime FechaEntrada { get; set; }  // Fecha de entrada para la reserva
        public DateTime FechaSalida { get; set; }   // Fecha de salida para la reserva
        public string EstadoReserva { get; set; }   // Estado de la reserva (ej. 'Reservado', 'Cancelado', etc.)
        public decimal TotalReserva { get; set; }   // Total del monto de la reserva
        public string ComentarioReserva { get; set; } // Comentarios de la reserva (opcional)
    }
}
