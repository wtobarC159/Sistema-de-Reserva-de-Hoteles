using System.Security.Claims;
using System.Linq;

namespace Sistema_de_Reserva_de_Hoteles.Extensiones
{
    public static class ExtensionClaims
    {
        public static string GetGivenName(this ClaimsPrincipal user)
        {
            var claim = user.Claims.FirstOrDefault(x => x.Type == "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/givenname");
            return claim?.Value ?? string.Empty;
        }
    }
}
