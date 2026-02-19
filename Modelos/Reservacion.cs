using System.ComponentModel.DataAnnotations.Schema;

namespace Sistema_de_Reserva_de_Hoteles.Modelos
{
    [Table("Reservaciones")]
    public class Reservacion
    {
        public int Id { get; set; }
        public string NombreCliente { get; set; } = string.Empty;
        public string HotelReservado { get; set; } = string.Empty;
        [Column(TypeName = "Date")]
        public  DateTime FechaReservaInicio { get; set; }
        [Column(TypeName = "Date")]
        public DateTime FechaReservaFin { get; set; }
        public int Dias { get; set; } 
        public int NumHabitaciones { get; set; }
        public double CostoTotal { get; set; }
        public DateTime CreacionSolicitud { get; set; } = DateTime.Now;


        public string? UsuarioAppId { get; set; }
        public UsuarioApp? UsuarioApp { get; set; }
        public int ClienteId { get; set; }
        public Cliente? Cliente { get; set; }
    }
}
