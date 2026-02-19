using Microsoft.IdentityModel.Tokens;
using Sistema_de_Reserva_de_Hoteles.Interfaces;
using Sistema_de_Reserva_de_Hoteles.Modelos;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace Sistema_de_Reserva_de_Hoteles.Servicios
{
    public class JWTokenService : IJWToken
    {
        private readonly IConfiguration _configuracion;
        private readonly SymmetricSecurityKey _llaveSeguridad;
        public JWTokenService( IConfiguration configuracion)
        {
            _configuracion = configuracion;
            _llaveSeguridad = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(_configuracion["JWT:Key"]));
        }
        public string GenerarToken(UsuarioApp Usuario, string rol)
        {
            var Claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Email,Usuario.Email),
                new Claim(JwtRegisteredClaimNames.GivenName,Usuario.UserName),
                new Claim(ClaimTypes.Role,rol)
            };

            var Credenciales = new SigningCredentials(_llaveSeguridad, SecurityAlgorithms.HmacSha256);

            var TokenDescriptor = new SecurityTokenDescriptor 
            {
                Subject = new ClaimsIdentity(Claims),
                Expires = DateTime.UtcNow.AddHours(2),
                SigningCredentials = Credenciales,
                Issuer = _configuracion["JWT:Issuer"],
                Audience = _configuracion["JWT:Audience"]
            };

            var TokenHandler = new JwtSecurityTokenHandler();
            var Token = TokenHandler.CreateToken(TokenDescriptor);
            return TokenHandler.WriteToken(Token);
        }
    }
}
