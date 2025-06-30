namespace proyecto_motel
{
    public class Pagos
    {
        public int NumPago { get; set; }           
        public int NumReserva { get; set; }       
        public decimal MontoPago { get; set; }    
        public DateTime FechaPago { get; set; }   
        public string MetodoPago { get; set; }     
        public string EstadoPago { get; set; }     
        public string ComentarioPago { get; set; } 
    }
}
