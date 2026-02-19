using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Sistema_de_Reserva_de_Hoteles.Helpers;
using Sistema_de_Reserva_de_Hoteles.Interfaces;
using Sistema_de_Reserva_de_Hoteles.Modelos;
using Sistema_de_Reserva_de_Hoteles.Servicios;
using System.Linq;

namespace Sistema_de_Reserva_de_Hoteles.Controllers
{
    [Route("api/hotels")]
    [ApiController]
    public class ControladorHotel : ControllerBase
    {
        private readonly IApiHotel _apiExternaHoteles;
        private readonly OauthTokenService _oauthTokenService;
        
        public ControladorHotel(IApiHotel apiExternaHoteles, OauthTokenService oauthTokenService)
        {
            _apiExternaHoteles = apiExternaHoteles;
            _oauthTokenService = oauthTokenService;
        }

        [Authorize]
        [HttpGet("hotel/{CodigoIata}")]
        public async Task<IActionResult> GetHoteles([FromRoute] string CodigoIata, [FromQuery] QueryObjects query)
        {
            if (string.IsNullOrEmpty(CodigoIata))
            {
                return BadRequest("El codigo IATA es obligatorio.");
            }
            var hoteles = await _apiExternaHoteles.ListaHotel(CodigoIata,query);
            if (hoteles == null || !hoteles.Any())
            {
                return NotFound("No se encontraron hoteles para el codigo IATA proporcionado.");
            }
            return Ok(hoteles);
        }
    }
}
