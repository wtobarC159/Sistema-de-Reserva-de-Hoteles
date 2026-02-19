using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using Sistema_de_Reserva_de_Hoteles.Dtos.HotelesDTO;
using Sistema_de_Reserva_de_Hoteles.Helpers;
using Sistema_de_Reserva_de_Hoteles.Interfaces;
using Sistema_de_Reserva_de_Hoteles.Mapas;
using Sistema_de_Reserva_de_Hoteles.Modelos;
using System.Net.Http;

namespace Sistema_de_Reserva_de_Hoteles.Servicios
{
    public class ApiExternaHoteles : IApiHotel
    {
        private readonly HttpClient _httpClient;
        private readonly IConfiguration _configuration;
        private readonly OauthTokenService _oauthTokenService;
        public ApiExternaHoteles(HttpClient httpClient, IConfiguration configuration, OauthTokenService oauthTokenService)
        {
            _httpClient = httpClient;
            _configuration = configuration;
            _oauthTokenService = oauthTokenService;
        }
        public async Task<List<Hotel>> ListaHotel(string CodigoIata, QueryObjects query)
        {
            try
            {
                var Token = await _oauthTokenService.ObtenerToken();
                _httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", Token);
                var ConsultaUrl = await _httpClient.GetAsync($"https://test.api.amadeus.com/v1/reference-data/locations/hotels/by-city?cityCode={CodigoIata}&radius=5&radiusUnit=KM&hotelSource=ALL");

                if (ConsultaUrl.IsSuccessStatusCode)
                {
                    var Contenido = await ConsultaUrl.Content.ReadAsStringAsync();
                    var Resultado = JsonConvert.DeserializeObject<Root>(Contenido);
                    var Hotels = Resultado?.data;

                    if (Hotels != null)
                    {
                        var ListData = Hotels.Select(s => s.ToFromHotel()).AsQueryable();

                        if (query.PageSize != 0 || query.PageNumber != 0)
                        {
                            if(query.PageSize==0) query.PageSize=20;
                            var SkipNumber = (query.PageNumber - 1) * query.PageSize;
                            return ListData.Skip(SkipNumber).Take(query.PageSize).ToList();
                        }
                        return ListData.ToList();
                    }
                }
                else
                {
                    return null;
                }

            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
            return null;
        }
    }

}
