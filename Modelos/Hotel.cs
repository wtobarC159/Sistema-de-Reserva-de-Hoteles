using System.ComponentModel.DataAnnotations.Schema;

namespace Sistema_de_Reserva_de_Hoteles.Modelos
{
    public class Hotel
    {
        public string NombreHotel { get; set; } = string.Empty;
        public string HotelID { get; set; } = string.Empty;
        public string CodigoCiudad { get; set; } = string.Empty;
        public string? Pais { get; set; } = string.Empty;
        public double PrecionHabNoche { get; set; }
    }
}
