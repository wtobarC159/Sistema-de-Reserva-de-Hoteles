using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Sistema_de_Reserva_de_Hoteles.Data;
using Sistema_de_Reserva_de_Hoteles.Dtos.UsuarioDTO;
using Sistema_de_Reserva_de_Hoteles.Helpers;
using Sistema_de_Reserva_de_Hoteles.Interfaces;
using Sistema_de_Reserva_de_Hoteles.Mapas;
using Sistema_de_Reserva_de_Hoteles.Modelos;

namespace Sistema_de_Reserva_de_Hoteles.Controllers
{
    [Route("api/usuario")]
    [ApiController]
    public class ControladorUsuario : ControllerBase
    {
        private readonly IUsuarioApp _usuarioApp;
        private readonly UserManager<UsuarioApp> _userManager;
        private readonly SignInManager<UsuarioApp> _signInManager;
        private readonly IJWToken _token;
        public ControladorUsuario(IUsuarioApp usuarioApp, UserManager<UsuarioApp> userManager, SignInManager<UsuarioApp> signManager, IJWToken token)
        {
            _usuarioApp = usuarioApp;
            _userManager = userManager;
            _signInManager = signManager;
            _token = token;
        }

        [HttpPost("registro")]
        public async Task<IActionResult> Registro([FromBody] RegistroDTO Registro)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var Usuario = new UsuarioApp
                {
                    UserName = Registro.NombreUsuario,
                    Email = Registro.Email,
                };

                var UsuarioCreado = await _userManager.CreateAsync(Usuario, Registro.Contraseña);

                if (UsuarioCreado.Succeeded)
                {
                    var RoleAsignado = await _userManager.AddToRoleAsync(Usuario, "User");
                    if (RoleAsignado.Succeeded)
                    {
                        return Ok(new UsuarioAutenticado
                        {

                            NombreUsuario = Usuario.UserName,
                            Email = Usuario.Email,
                            Token = _token.GenerarToken(Usuario, "User")
                        });
                    }
                    else
                    {
                        return StatusCode(500, "Error al asignar el rol al usuario.");
                    }

                }
                else
                {
                    return StatusCode(500, "Error al intentar crear el Usuario, intente nuevamente");
                }


            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error interno del servidor: {ex.Message}");
            }
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginDTO LoginUsuario) 
        {
            if (!ModelState.IsValid) 
            {
                return BadRequest(ModelState);
            }

            var Usuario = await _userManager.Users.FirstOrDefaultAsync(s => s.UserName == LoginUsuario.NombreUsuario.ToLower());
            if (Usuario == null) return NotFound("Este Usuario no se encuentra Registrado");
            var Validacion = await _signInManager.CheckPasswordSignInAsync(Usuario, LoginUsuario.Contraseña, false);
            if (Validacion.Succeeded)
            {
                return Ok(new UsuarioAutenticado
                {
                    NombreUsuario = Usuario.UserName,
                    Email = Usuario.Email,
                    Token = _token.GenerarToken(Usuario, "User")
                });
            }
            else 
            {
                return StatusCode(401, "Usuario o Contraseña incorrecta");
            }
        }

        [HttpGet("getUsuarios")]
        public async Task<IActionResult> ObtenerUsuarios([FromQuery] QueryObjects query)
        {
            var Usuarios = await _usuarioApp.GetUsuariosApp(query);
            var UsuariosDTOs = Usuarios.Select(s => s.ToUsuarioDTO(_userManager)).ToList();
            return Ok(UsuariosDTOs);
        }

        [HttpGet("getUsuario/{usuarioAppId}")]
        public async Task<IActionResult> ObtenerUsuarioId([FromQuery] QueryObjects query, [FromRoute] string usuarioAppId) 
        {
            if (!ModelState.IsValid) 
            {
                return BadRequest();
            }

            var Usuario = await _usuarioApp.GetUsuarioApp(usuarioAppId);

            if (Usuario == null) 
            {
                return NotFound();
            }

            return Ok(Usuario.ToUsuarioDTO(_userManager));
        }

    }
}
