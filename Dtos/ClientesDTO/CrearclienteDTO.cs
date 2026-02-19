using System.ComponentModel.DataAnnotations;

namespace Sistema_de_Reserva_de_Hoteles.Dtos.Clientes
{
    public class CrearclienteDTO
    {
        [Required(ErrorMessage = "Este campo es Obligatorio")]
        [MaxLength(100, ErrorMessage = "El nombre no puede superar los 100 caracteres")]
        public string Nombres { get; set; } = string.Empty;
        [Required(ErrorMessage = "Este campo es Obligatorio")]
        [MaxLength(50, ErrorMessage ="Ingrese un Email valido")]
        public string Email { get; set; } = string.Empty;
        [Required(ErrorMessage ="Este campo es obligatorio")]
        [MaxLength(10, ErrorMessage = "El telefono no puede superar los 10 dígitos")]
        public string Telefono { get; set; } = string.Empty;
        [Required(ErrorMessage = "Este campo es Obligatorio")]
        [MaxLength(10, ErrorMessage = "La identificacion no puede superar los 10 dígitos")]
        public string Identificacion { get; set; } = string.Empty;
    }
}
