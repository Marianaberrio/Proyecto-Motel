namespace proyecto_motel
{
    public class AsignarHabitacionesRequest
    {
        public int ReservaId { get; set; }
        public List<Habitacion> Habitaciones { get; set; } // Esta propiedad debe existir
    }
}
