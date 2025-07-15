// Models/ConfirmacionReservaViewModel.cs
using System.Collections.Generic;

namespace Motel.Web.Models
{
    public class ConfirmacionReservaViewModel
    {
        public Reserva Reserva { get; set; } = default!;
        public List<ReservaHabitacion> DetalleHabitaciones { get; set; } = new();
        public List<ReservaServicio> DetalleServicios { get; set; } = new();
        public List<Habitacion> Habitaciones { get; set; } = new();
        public List<Servicios> Servicios { get; set; } = new();
    }
}