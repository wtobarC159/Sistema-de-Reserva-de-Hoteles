using Microsoft.EntityFrameworkCore;
using Sistema_de_Reserva_de_Hoteles.Data;
using Sistema_de_Reserva_de_Hoteles.Dtos.Clientes;
using Sistema_de_Reserva_de_Hoteles.Helpers;
using Sistema_de_Reserva_de_Hoteles.Interfaces;
using Sistema_de_Reserva_de_Hoteles.Modelos;

namespace Sistema_de_Reserva_de_Hoteles.Respositorio
{
    public class RepositorioCliente : ICliente
    {
        private readonly DataContext _content;

        public RepositorioCliente(DataContext content) 
        {
            _content = content;
        }

        public async Task<Cliente?> ActualizarCliente(int id, ActualizarclienteDTO ClienteAct)
        {
            var ClienteDB = await _content.Clientes.FirstOrDefaultAsync(s => s.Id == id);

            if (ClienteDB == null) return null;

            ClienteDB.Nombres = ClienteAct.Nombres;
            ClienteDB.Email = ClienteAct.Email;
            ClienteDB.Telefono = ClienteAct.Telefono;
            ClienteDB.Identificacion = ClienteAct.Identificacion;

            await _content.SaveChangesAsync();
            return ClienteDB;
        }

        public async Task<Cliente?> CrearCliente(Cliente ClienteNuevo)
        {
            var ClienteDB = await _content.Clientes.FirstOrDefaultAsync(s => s.Identificacion == ClienteNuevo.Identificacion);
            if (ClienteDB != null) return null;

            await _content.Clientes.AddAsync(ClienteNuevo);
            await _content.SaveChangesAsync();
            return ClienteNuevo;
        }

        public async Task<Cliente?> EliminarCliente(int clienteId)
        {
            var ClienteDB = await _content.Clientes.FirstOrDefaultAsync(s => s.Id == clienteId);
            if (ClienteDB == null) return null;

            _content.Clientes.Remove(ClienteDB);
            await _content.SaveChangesAsync();
            return ClienteDB;
        }

        public async Task<Cliente?> GetCliente(int clienteId)
        {
            var ClienteDB = await _content.Clientes.FirstOrDefaultAsync(s => s.Id == clienteId);
            if(ClienteDB == null) return null;

            return ClienteDB;
        }

        public async Task<List<Cliente>> GetClientes(QueryObjects query)
        {
            var ClientesDB =  _content.Clientes.AsQueryable();

            if (query.PageSize != 0 || query.PageNumber != 0) 
            {
                if (query.PageSize == 0) query.PageSize = 10;
                return await ClientesDB.Skip((query.PageNumber - 1) * query.PageSize).Take(query.PageSize).ToListAsync();
            }
            return ClientesDB.ToList();
        }
    }
}
