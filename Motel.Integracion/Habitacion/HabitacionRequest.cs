namespace Motel.Integracion.Habitacion
{
    public class HabitacionRequest
    {

        public int IdHabitacion { get; set; }
        public string NumHabitacion { get; set; }
        public string TipoHabitacion { get; set; }
        public decimal PrecioHabitacion { get; set; }
        public int CapacidadHabitacion { get; set; }
        public string EstadoHabitacion { get; set; }
    }
}
