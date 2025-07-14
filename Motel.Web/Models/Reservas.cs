using proyecto_motel;

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

        public List<Habitacion> Habitaciones { get; set; } = new();
        public List<Servicios> Servicios { get; set; } = new();

        public List<ReservaHabitacion> DetalleHabitaciones { get; set; } = new();
        public List<ReservaServicio> DetalleServicios { get; set; } = new();


    }
}