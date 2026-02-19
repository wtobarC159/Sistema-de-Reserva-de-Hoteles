using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Sistema_de_Reserva_de_Hoteles.Data;
using Sistema_de_Reserva_de_Hoteles.Dtos.Clientes;
using Sistema_de_Reserva_de_Hoteles.Helpers;
using Sistema_de_Reserva_de_Hoteles.Interfaces;
using Sistema_de_Reserva_de_Hoteles.Mapas;

namespace Sistema_de_Reserva_de_Hoteles.Controllers
{
    [Route("api/cliente")]
    [ApiController]
    public class ControladorCliente : ControllerBase
    {
        private readonly ICliente _repositorioCliente;
        private readonly DataContext _context;  
        public ControladorCliente( ICliente repositorioCliente, DataContext context)
        {
            _repositorioCliente = repositorioCliente;
            _context = context;
        }

        [Authorize]
        [HttpGet("getcliente")]
        public async Task<IActionResult> GetClientes([FromRoute] QueryObjects query) 
        {
            if (!ModelState.IsValid) 
            {
                return BadRequest(ModelState);
            }

            var Clientes = await _repositorioCliente.GetClientes(query);
            var DTOClientes = Clientes.Select(s => s.ToClienteDTO()).ToList();

            return Ok(DTOClientes);
        }

        [Authorize]
        [HttpGet("getcliente/{clienteId:int}")]
        public async Task<IActionResult> GelClienteId([FromRoute] int clienteId) 
        {
            if (!ModelState.IsValid) 
            {
                return BadRequest(ModelState);
            }

            var Cliente = await _repositorioCliente.GetCliente(clienteId);
            if (Cliente == null) 
            {
                return NotFound($"El cliente con el id {clienteId} no fue encontrado.");
            }

            return Ok(Cliente.ToClienteDTO());
        }

        [Authorize]
        [HttpPost("crearcliente")]
        public async Task<IActionResult> CrearCliente([FromBody] CrearclienteDTO ClienteCrear)
        {
            if (!ModelState.IsValid) 
            {
                return BadRequest(ModelState);
            }
            var Cliente = ClienteCrear.ToCliente();
            await _repositorioCliente.CrearCliente(Cliente);

            return CreatedAtAction(nameof(GelClienteId),new { clienteId = Cliente.Id },Cliente.ToClienteDTO());
        }

        [Authorize]
        [HttpPut("actualizarcliente/{clienteId:int}")]
        public async Task<IActionResult> UpdateCliente([FromBody] ActualizarclienteDTO ActualizarNodo, [FromRoute] int clienteId) 
        {
            if (!ModelState.IsValid) 
            {
                return BadRequest(ModelState);
            }
            var Cliente = await _repositorioCliente.ActualizarCliente(clienteId, ActualizarNodo);
            if (Cliente == null) 
            {
                return NotFound($"El cliente con el id {clienteId} no fue encontrado.");
            }
            return Ok(Cliente.ToClienteDTO());
        }

        [Authorize]
        [HttpDelete("eliminarcliente/{clienteId:int}")]
        public async Task<IActionResult> DeleteCliente([FromRoute] int clienteId) 
        {
            if (!ModelState.IsValid) 
            {
                return BadRequest();
            }

            var Cliente = await _repositorioCliente.EliminarCliente(clienteId);

            if (Cliente == null) 
            {
                return NotFound();
            }

            return NoContent();
        }
    }
}
