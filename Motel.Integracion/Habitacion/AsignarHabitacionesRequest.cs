using System.Collections.Generic;

namespace Motel.Integracion.Habitacion
{
    public class AsignarHabitacionesRequest
    {
        public int ReservaId { get; set; }
        public List<HabitacionAsignada> Habitaciones { get; set; }
    }

    public class HabitacionAsignada
    {
        public int IdHabitacion { get; set; }
        public decimal PrecioHabitacion { get; set; }
    }
}
