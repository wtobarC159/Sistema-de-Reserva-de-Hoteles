using Sistema_de_Reserva_de_Hoteles.Dtos.ReservaDTO;
using Sistema_de_Reserva_de_Hoteles.Helpers;
using Sistema_de_Reserva_de_Hoteles.Modelos;

namespace Sistema_de_Reserva_de_Hoteles.Interfaces
{
    public interface IReservacion
    {
        Task<List<Reservacion>> GetReservaciones(QueryReserva query);
        Task<Reservacion?> GetReservacion(int reservacionId);
        Task<Reservacion> CrearReserva(Reservacion ReservaNueva);
        Task<Reservacion?> ActualizarReserva(int id, ActualizarReservaDTO ReservaAct);
        Task<Reservacion?> EliminarReserva(int reservacionId);
    }
}
