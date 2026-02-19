using Sistema_de_Reserva_de_Hoteles.Modelos;

namespace Sistema_de_Reserva_de_Hoteles.Interfaces
{
    public interface IJWToken
    {
        string GenerarToken(UsuarioApp Usuario, string rol);
    }
}
