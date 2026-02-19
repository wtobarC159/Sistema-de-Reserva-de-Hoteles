using Sistema_de_Reserva_de_Hoteles.Helpers;
using Sistema_de_Reserva_de_Hoteles.Modelos;

namespace Sistema_de_Reserva_de_Hoteles.Interfaces
{
    public interface IApiHotel
    {
        Task<List<Hotel>> ListaHotel(string CodigoIata, QueryObjects query);
    }
}
