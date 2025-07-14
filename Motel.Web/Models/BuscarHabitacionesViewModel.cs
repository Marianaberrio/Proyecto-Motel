namespace Motel.Web.Models
{
    public class BuscarHabitacionesViewModel
    {
        public string TipoHabitacion { get; set; }
        public int Cantidad { get; set; }

        public DateTime FechaEntrada { get; set; }
        public DateTime FechaSalida { get; set; }

    }
}