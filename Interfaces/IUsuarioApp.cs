using Sistema_de_Reserva_de_Hoteles.Helpers;
using Sistema_de_Reserva_de_Hoteles.Modelos;

namespace Sistema_de_Reserva_de_Hoteles.Interfaces
{
    public interface IUsuarioApp
    {
        Task<List<UsuarioApp>> GetUsuariosApp(QueryObjects query);
        Task<UsuarioApp?> GetUsuarioApp(string usuarioAppId);
    }
}
