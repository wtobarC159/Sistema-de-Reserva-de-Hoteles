using Sistema_de_Reserva_de_Hoteles.Dtos.Clientes;
using Sistema_de_Reserva_de_Hoteles.Helpers;
using Sistema_de_Reserva_de_Hoteles.Modelos;

namespace Sistema_de_Reserva_de_Hoteles.Interfaces
{
    public interface ICliente
    {
        Task<List<Cliente>> GetClientes(QueryObjects query);
        Task<Cliente?> GetCliente(int clienteId);
        Task<Cliente?> CrearCliente(Cliente ClienteNuevo);
        Task<Cliente?> ActualizarCliente(int id, ActualizarclienteDTO ClienteAct);
        Task<Cliente?> EliminarCliente(int clienteId);
    }
}
