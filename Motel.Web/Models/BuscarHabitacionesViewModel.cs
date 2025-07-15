namespace Motel.Web.Models
{
    public class TipoCantidad
    {
        public string Tipo { get; set; } = "";
        public int Cantidad { get; set; } = 1;
    }

    public class BuscarHabitacionesViewModel
    {
        // Lista de pares Tipo+Cantidad
        public List<TipoCantidad> TiposCantidad { get; set; } = new();

        // Para poblar el <select> desde la API
        public List<string> TodosLosTipos { get; set; } = new();

        public DateTime FechaEntrada { get; set; } = DateTime.Now;
        public DateTime FechaSalida { get; set; } = DateTime.Now.AddHours(1);
    }
}