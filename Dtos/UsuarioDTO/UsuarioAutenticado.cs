namespace Sistema_de_Reserva_de_Hoteles.Dtos.UsuarioDTO
{
    public class UsuarioAutenticado
    {
        public string NombreUsuario { get; set;} = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Token { get; set; } = string.Empty;
    }
}
