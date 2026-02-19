using Sistema_de_Reserva_de_Hoteles.Modelos;

namespace Sistema_de_Reserva_de_Hoteles.Dtos.Reserva
{
    public class ReservacionDTO
    {
        public int Id { get; set; }
        public string NombreCliente { get; set; } = string.Empty;
        public string? HotelReservado { get; set; }
        public DateTime FechaReservaInicio { get; set; }
        public DateTime FechaReservaFin { get; set; }
        public int Dias { get; set;}
        public int NumHabitaciones { get; set; }
        public double CostoTotal { get; set; }
        public DateTime CreaciondeReserva { get; set; } = DateTime.Now;
    }
}
