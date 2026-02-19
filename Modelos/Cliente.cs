using System.ComponentModel.DataAnnotations.Schema;

namespace Sistema_de_Reserva_de_Hoteles.Modelos
{
    [Table("Clientes")]
    public class Cliente
    {
        public int Id { get; set; }
        public string Nombres { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Telefono { get; set; } = string.Empty;
        public string Identificacion { get; set; } = string.Empty;

        public List<Reservacion> Reservaciones = new List<Reservacion> ();
    }
}
