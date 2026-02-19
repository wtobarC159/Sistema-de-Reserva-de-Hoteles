using Newtonsoft.Json;
using Sistema_de_Reserva_de_Hoteles.Dtos.HotelesDTO;

namespace Sistema_de_Reserva_de_Hoteles.Servicios
{
    public class OauthTokenService
    {
        private readonly HttpClient _httpClient;
        private readonly IConfiguration _configuration;

        public OauthTokenService(HttpClient httpClient, IConfiguration configuration) 
        {
            _httpClient = httpClient;
            _configuration = configuration;
        }
        public async Task<string?> ObtenerToken()
        {
            var clientId = _configuration["OAuth:ApiKeyHotel"];
            var clientSecret = _configuration["OAuth:ApiSecretHotel"];

            var parametros = new Dictionary<string, string>
            {
                { "grant_type", "client_credentials" },
                { "client_id", clientId! },
                { "client_secret", clientSecret! }
            };

            var contenido = new FormUrlEncodedContent(parametros);
            var respuesta = await _httpClient.PostAsync("https://test.api.amadeus.com/v1/security/oauth2/token", contenido);

            if (respuesta.IsSuccessStatusCode)
            {
                var respuestaContenido = await respuesta.Content.ReadAsStringAsync();
                var tokenResponse = System.Text.Json.JsonSerializer.Deserialize<TokenResponse>(respuestaContenido);
                var accessToken = tokenResponse?.access_token;
                return accessToken;
            }
            else
            {
                throw new Exception("No se pudo obtener el token de acceso.");
            }
        }
    }

    public class TokenResponse
    {
        public string type { get; set; }
        public string username { get; set; }
        public string application_name { get; set; }
        public string client_id { get; set; }
        public string token_type { get; set; }
        public string access_token { get; set; }
        public object expires_in { get; set; }
        public string state { get; set; }
        public string scope { get; set; }
    }
}
