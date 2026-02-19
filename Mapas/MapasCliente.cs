using Sistema_de_Reserva_de_Hoteles.Dtos.Clientes;
using Sistema_de_Reserva_de_Hoteles.Modelos;

namespace Sistema_de_Reserva_de_Hoteles.Mapas
{
    static public class MapasCliente
    {

        static public ClienteDTO? ToClienteDTO(this Cliente ClienteNodo) 
        {
            return new ClienteDTO 
            {
                id = ClienteNodo.Id,
                Nombres = ClienteNodo.Nombres,
                Email = ClienteNodo.Email,
                Telefono = ClienteNodo.Telefono,
                Identificacion = ClienteNodo.Identificacion,
            };
        }

        static public Cliente? ToCliente(this CrearclienteDTO CrearNodo) 
        {
            return new Cliente 
            {
                Nombres = CrearNodo.Nombres,
                Email = CrearNodo.Email,
                Telefono = CrearNodo.Telefono,
                Identificacion = CrearNodo.Identificacion,
            };
        }
    }
}
