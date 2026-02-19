using Microsoft.AspNetCore.Identity;

namespace Sistema_de_Reserva_de_Hoteles.Modelos
{
    public class UsuarioApp : IdentityUser
    {
       public List<Reservacion> Reservaciones = new List<Reservacion>();
    }
}
