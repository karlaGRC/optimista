using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using KGRC_Evaluacion.Conexion;
using KGRC_Evaluacion.Interface;
using KGRC_Evaluacion.Models;
using Microsoft.Extensions.Options;

namespace KGRC_Evaluacion.Repositories
{
    public class RestClientRepository : IRestClient
    {
        private readonly HttpClient _client;
        private readonly string _baseUrl;
        public RestClientRepository(HttpClient client, IOptions<OptionsSettings> options)
        {
            _client = client;
            _baseUrl = options.Value.Baseurl; // Accede a la propiedad Baseurl de OptionsSettings
            _client.BaseAddress = new Uri(_baseUrl);
            _client.Timeout = TimeSpan.FromMinutes(options.Value.TimeSpanMinutes); // Accede a la propiedad TimeSpanMinutes de OptionsSettings
        }

        public async Task<string> SendRequestNoInputParameters(string Controller, string RecursoService)
        {
            try
            {
                var result = await _client.PostAsync(_client.BaseAddress + RecursoService, null);

                string resultContentJason = await result.Content.ReadAsStringAsync();
                return resultContentJason;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Hubo un error al momento de consumir el servicio: {ex.Message}");
                return string.Empty;
            }
        }

        public async Task<string> SendRequestWithParameters(string Controller, string RecursoService,
            HttpMethodToUse method, object Parameters)
        {
            try
            {
                
                HttpResponseMessage result ;
                string  UrlArmada = _client.BaseAddress + RecursoService + Parameters.ToString();
                switch (method)
                {
                    case HttpMethodToUse.GET:
                        result = await _client.GetAsync(UrlArmada);
                        break;
                   // case HttpMethodToUse.POST:
                     //   result = await _client.PostAsync(_client.BaseAddress+RecursoService+ Parameters.ToString());
                       // break;
                    // Agrega más casos según necesites para otros métodos HTTP como PUT, DELETE, etc.
                    default:
                        throw new ArgumentException("Método HTTP no soportado");
                }
                if (result.IsSuccessStatusCode)
                {
                    string resultContentJason = await result.Content.ReadAsStringAsync();
                     return resultContentJason;
                }
                else
                {
                    return $"Error: {result.StatusCode} - {result.ReasonPhrase}";
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Hubo un error al momento de generar el request: {ex.Message}");
                return string.Empty;
            }
        }

    }
}
