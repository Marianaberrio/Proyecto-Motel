using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Motel.Web.Models
{
    public class Cliente
{
    public int NumCliente { get; set; }
    public string NombreCliente { get; set; }
    public string ApellidoCliente { get; set; }
    public string CorreoCliente { get; set; }
    public string TelefonoCliente { get; set; }
    public DateTime FechaNacimiento { get; set; }
    public DateTime FechaRegistro { get; set; }
}
}