using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Sistema_de_Reserva_de_Hoteles.Data;
using Sistema_de_Reserva_de_Hoteles.Dtos.ReservaDTO;
using Sistema_de_Reserva_de_Hoteles.Extensiones;
using Sistema_de_Reserva_de_Hoteles.Helpers;
using Sistema_de_Reserva_de_Hoteles.Interfaces;
using Sistema_de_Reserva_de_Hoteles.Mapas;
using Sistema_de_Reserva_de_Hoteles.Modelos;

namespace Sistema_de_Reserva_de_Hoteles.Controllers
{
    [Route("api/reservacion")]
    [ApiController]
    public class ControladorReserva : ControllerBase
    {
        private readonly IReservacion _reservacion;
        private readonly DataContext _data;
        private readonly UserManager<UsuarioApp> _userManager;

        public ControladorReserva(IReservacion reservacion, DataContext data, UserManager<UsuarioApp> userManager)
        {
            _reservacion = reservacion;
            _data = data;
            _userManager = userManager;
        }

        [Authorize]
        [HttpGet("reservaciones")]
        public async Task<IActionResult> GerReservaciones([FromQuery] QueryReserva query)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var Reservaciones = await _reservacion.GetReservaciones(query);
            var ReservacionesDTO = Reservaciones.Select(s => s.ToReservaDTO()).ToList();
            return Ok(ReservacionesDTO);
        }

        [Authorize]
        [HttpGet("reservacion/{id:int}")]
        public async Task<IActionResult> GetReservacionID([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var Reservacion = await _reservacion.GetReservacion(id);
            if (Reservacion == null)
            {
                return NotFound();
            }

            return Ok(Reservacion.ToReservaDTO());
        }

        [Authorize]
        [HttpPost("crear-reserva")]
        public async Task<IActionResult> CrearReserva([FromBody]CreacionReservaDTO Creacion) 
        {
            if (!ModelState.IsValid)    
            {
                return BadRequest(ModelState);
            }
            var Usuario = User.GetGivenName();
            var AppUser = await _userManager.FindByNameAsync(Usuario);
            var NuevaReserva = Creacion.ToReserva(AppUser.Id);
            var ReservaCreada = await _reservacion.CrearReserva(NuevaReserva);
            if(ReservaCreada == null)
            {
                return BadRequest("No se pudo crear la reserva.");
            }
            return CreatedAtAction(nameof(GetReservacionID), new { id = ReservaCreada.Id }, ReservaCreada.ToReservaDTO());
        }

        [Authorize]
        [HttpPut("actualizar-reserva/{id:int}")]
        public async Task<IActionResult> ActualizarReserva([FromRoute] int id, [FromBody] ActualizarReservaDTO ReservaActualizada) 
        {
            if (!ModelState.IsValid) 
            {
                return BadRequest(ModelState);
            }

            var ActualizarReserva = await _reservacion.ActualizarReserva(id, ReservaActualizada);
            if (ActualizarReserva==null)
            {
                return NotFound("La Reserva no fue encontrada"); 
            }

            return Ok(ActualizarReserva.ToReservaDTO());
        }

        [Authorize]
        [HttpDelete("eliminar-reserva/{id:int}")]
        public async Task<IActionResult> EliminarReserva([FromRoute] int id) 
        {
            if(!ModelState.IsValid) 
            {
                return BadRequest(ModelState);
            }

            var ReservaEliminada = await _reservacion.EliminarReserva(id);
            if (ReservaEliminada == null) 
            {
                return NotFound("La Reserva no fue encontrada");
            }

            return NoContent();
        }
    }
}
