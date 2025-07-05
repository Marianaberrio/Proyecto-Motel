namespace Motel.Web.Models
{
    public class Reserva
    {
        public int NumReserva { get; set; }
        public int NumCliente { get; set; }
        public DateTime FechaReserva { get; set; }
        public DateTime FechaEntrada { get; set; }
        public DateTime FechaSalida { get; set; }
        public string EstadoReserva { get; set; }
        public decimal TotalReserva { get; set; }
        public string ComentarioReserva { get; set; }
        public Cliente Cliente { get; set; }
        public List<Habitacion> Habitaciones { get; set; }
        public List<Servicio> Servicios { get; set; }
    }
}