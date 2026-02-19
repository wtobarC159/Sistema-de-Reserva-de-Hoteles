using Sistema_de_Reserva_de_Hoteles.Dtos.HotelesDTO;
using Sistema_de_Reserva_de_Hoteles.Modelos;

namespace Sistema_de_Reserva_de_Hoteles.Mapas
{
    static public class MapasHotel
    {
        public static Hotel ToFromHotel(this Datum hotel)
        {
            return new Hotel
            {
                NombreHotel = hotel.name,
                HotelID = hotel.hotelId,
                CodigoCiudad = hotel.iataCode,
                Pais = hotel.address?.countryCode,
                PrecionHabNoche = Random.Shared.Next(50, 100),
            };
        }
    }
}
