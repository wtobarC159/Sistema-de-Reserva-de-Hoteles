using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Sistema_de_Reserva_de_Hoteles.Data;
using Sistema_de_Reserva_de_Hoteles.Dtos.ReservaDTO;
using Sistema_de_Reserva_de_Hoteles.Helpers;
using Sistema_de_Reserva_de_Hoteles.Interfaces;
using Sistema_de_Reserva_de_Hoteles.Modelos;
using Sistema_de_Reserva_de_Hoteles.Servicios;

namespace Sistema_de_Reserva_de_Hoteles.Respositorio
{
    public class RepositorioReservacion : IReservacion
    {
        private readonly DataContext _content;
        private IApiHotel _apiExternaHoteles;

        public RepositorioReservacion(DataContext content, IApiHotel apiExternaHoteles) 
        {
            _content = content;
            _apiExternaHoteles = apiExternaHoteles;
        }

        public async Task<Reservacion?> ActualizarReserva(int id, ActualizarReservaDTO ReservaAct)
        {
            var ReservacionDB = await _content.Reservaciones.FirstOrDefaultAsync(s => s.Id == id);

            if(ReservacionDB == null) return null;

            ReservacionDB.NombreCliente = ReservaAct.NombreCliente;
            ReservacionDB.HotelReservado = ReservaAct.HotelReservado;
            ReservacionDB.FechaReservaInicio = ConvertidorFecha.ConvertirStringAFecha(ReservaAct.FechaReservaInicio);
            ReservacionDB.FechaReservaFin = ConvertidorFecha.ConvertirStringAFecha(ReservaAct.FechaReservaFin);
            ReservacionDB.NumHabitaciones = ReservaAct.NumHabitaciones;
            ReservacionDB.Dias = (ConvertidorFecha.ConvertirStringAFecha(ReservaAct.FechaReservaFin) - ConvertidorFecha.ConvertirStringAFecha(ReservaAct.FechaReservaInicio)).Days;
            ReservacionDB.CostoTotal = CalculoCostoTotal.CalculoCosto(Random.Shared.Next(50,100), ReservaAct.NumHabitaciones, (ConvertidorFecha.ConvertirStringAFecha(ReservaAct.FechaReservaFin) - ConvertidorFecha.ConvertirStringAFecha(ReservaAct.FechaReservaInicio)).Days);

            await _content.SaveChangesAsync();
            return ReservacionDB;
        }
      
        public async Task<Reservacion> CrearReserva(Reservacion ReservaNueva)
        {
            var ClienteRes = await _content.Clientes.FirstOrDefaultAsync(s => s.Nombres.Contains(ReservaNueva.NombreCliente));
            ReservaNueva.ClienteId = ClienteRes != null ? ClienteRes.Id : 0;

            await _content.Reservaciones.AddAsync(ReservaNueva);
            await _content.SaveChangesAsync();
            return ReservaNueva;
        }

        public async Task<Reservacion?> EliminarReserva(int reservacionId)
        {
            var ReservacionDB = await _content.Reservaciones.FirstOrDefaultAsync(s => s.Id == reservacionId);
            if(ReservacionDB == null) return null;

            _content.Reservaciones.Remove(ReservacionDB);
            await _content.SaveChangesAsync();
            return ReservacionDB;
        }

        public async Task<Reservacion?> GetReservacion(int reservacionId)
        {
            var ReservacionDB = await _content.Reservaciones.FirstOrDefaultAsync(s => s.Id == reservacionId);
            if(ReservacionDB == null) return null;

            return ReservacionDB;
        }

        public async Task<List<Reservacion>> GetReservaciones(QueryReserva query)
        {
            var Reservas = _content.Reservaciones.AsQueryable();

            if (!string.IsNullOrWhiteSpace(query.UsuarioAppId)) 
            {
                Reservas = Reservas.Where(s => s.UsuarioAppId == query.UsuarioAppId);
            }

            if (query.ClienteId != null) 
            {
                Reservas = Reservas.Where(s => s.ClienteId == query.ClienteId);
            }

            return await Reservas.ToListAsync();
        }
    }
}
