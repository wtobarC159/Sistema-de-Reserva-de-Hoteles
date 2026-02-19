using Sistema_de_Reserva_de_Hoteles.Modelos;
using System.ComponentModel.DataAnnotations;

namespace Sistema_de_Reserva_de_Hoteles.Dtos.ReservaDTO
{
    public class CreacionReservaDTO
    {
        [Required(ErrorMessage = "Este campo es obligatorio")]
        [MaxLength(100, ErrorMessage ="El nombre no puede superar los 100 caracteres")]
        public string NombreCliente { get; set;} = string.Empty;
        [Required(ErrorMessage = "Este campo es obligatorio")]
        [MaxLength(30, ErrorMessage = "El nombre no puede superar los 30 caracteres")]
        public string HotelReservado { get; set;} = string.Empty;
        [Required(ErrorMessage = "Este campo es obligatorio")]
        public string FechaReservaInicio { get; set;} = string.Empty;
        [Required(ErrorMessage = "Este campo es obligatorio")]
        public string FechaReservaFin { get; set;} = string.Empty;
        [Required(ErrorMessage = "Este campo es obligatorio")]
        [Range(1,10,ErrorMessage ="solo puede elegir hasta 10 habitaciones")]
        public int NumHabitaciones { get; set;}
    }
}
