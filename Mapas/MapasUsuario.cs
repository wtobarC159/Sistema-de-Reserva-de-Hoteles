using Microsoft.AspNetCore.Identity;
using Sistema_de_Reserva_de_Hoteles.Dtos.UsuarioDTO;
using Sistema_de_Reserva_de_Hoteles.Modelos;

namespace Sistema_de_Reserva_de_Hoteles.Mapas
{
    static public class MapasUsuario
    {
        // Mapea el modelo Usuario al DTO UsuarioDTO
        static public UsuarioDTO ToUsuarioDTO(this UsuarioApp UsuarioNodo,UserManager<UsuarioApp> UsuarioN) 
        {
            return new UsuarioDTO
            {
                id = UsuarioNodo.Id,
                NombreUsuario = UsuarioNodo.UserName,
                Rol = UsuarioN.GetRolesAsync(UsuarioNodo).Result.FirstOrDefault(),
                Email = UsuarioNodo.Email,
            };
        }
    }
}
