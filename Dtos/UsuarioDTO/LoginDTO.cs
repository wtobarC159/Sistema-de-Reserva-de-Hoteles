using System.ComponentModel.DataAnnotations;

namespace Sistema_de_Reserva_de_Hoteles.Dtos.UsuarioDTO
{
    public class LoginDTO
    {
        [Required(ErrorMessage = "El campo Nombre de Usuario es obligatorio.")]
        [MaxLength(20, ErrorMessage = "El Nombre de Usuario no debe exceder los 20 caracteres.")]
        [MinLength(4, ErrorMessage = "El Nombre de Usuario debe tener al menos 4 caracteres.")]
        public string NombreUsuario { get; set; } = string.Empty;
        [Required(ErrorMessage = "Este campo es obligatorio")]
        public string Contraseña { get; set; } = string.Empty;
    }
}
