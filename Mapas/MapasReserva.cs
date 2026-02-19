using Sistema_de_Reserva_de_Hoteles.Dtos.Reserva;
using Sistema_de_Reserva_de_Hoteles.Dtos.ReservaDTO;
using Sistema_de_Reserva_de_Hoteles.Modelos;
using Sistema_de_Reserva_de_Hoteles.Servicios;

namespace Sistema_de_Reserva_de_Hoteles.Mapas
{
    static public class MapasReserva
    {
        static public ReservacionDTO? ToReservaDTO(this Reservacion Reserva) 
        {
            return new ReservacionDTO 
            {
                Id = Reserva.Id,
                NombreCliente = Reserva.NombreCliente,
                HotelReservado = Reserva.HotelReservado,
                FechaReservaInicio = Reserva.FechaReservaInicio,
                FechaReservaFin = Reserva.FechaReservaFin,
                Dias = Reserva.Dias,
                NumHabitaciones = Reserva.NumHabitaciones,
                CostoTotal = Reserva.CostoTotal,
                CreaciondeReserva = Reserva.CreacionSolicitud,
            };
        }

        static public Reservacion? ToReserva(this CreacionReservaDTO CrearNodo,string AppUserId) 
        {
            return new Reservacion
            {
                NombreCliente = CrearNodo.NombreCliente,
                HotelReservado = CrearNodo.HotelReservado,
                FechaReservaInicio = ConvertidorFecha.ConvertirStringAFecha(CrearNodo.FechaReservaInicio),
                FechaReservaFin = ConvertidorFecha.ConvertirStringAFecha(CrearNodo.FechaReservaFin),
                Dias = (ConvertidorFecha.ConvertirStringAFecha(CrearNodo.FechaReservaFin) - ConvertidorFecha.ConvertirStringAFecha(CrearNodo.FechaReservaFin)).Days,
                NumHabitaciones = CrearNodo.NumHabitaciones,
                CostoTotal = CalculoCostoTotal.CalculoCosto(Random.Shared.Next(50,100),CrearNodo.NumHabitaciones, (ConvertidorFecha.ConvertirStringAFecha(CrearNodo.FechaReservaFin) - ConvertidorFecha.ConvertirStringAFecha(CrearNodo.FechaReservaFin)).Days),
                UsuarioAppId = AppUserId
            };
        }
    }
}
