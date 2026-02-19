namespace Sistema_de_Reserva_de_Hoteles.Dtos.Clientes
{
    public class ClienteDTO
    {
        public int id { get; set; }
        public string Nombres { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Telefono { get; set; } = string.Empty;
        public string Identificacion { get; set; } = string.Empty;

    }
}
