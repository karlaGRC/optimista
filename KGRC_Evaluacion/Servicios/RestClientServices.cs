using Azure;
using Microsoft.Extensions.Options;
using KGRC_Evaluacion.Interface;
using Microsoft.AspNetCore.Mvc;
using System.Text;
using KGRC_Evaluacion.Models;
using System;
using KGRC_Evaluacion.Repositories;

namespace KGRC_Evaluacion.Servicios
{
    public class RestClientServices
    {
        private readonly IRestClient _client;
      
        public RestClientServices(IRestClient client)
        {
            _client = client;
        }

        public async Task<string> SendRequestNoInputParameters(string Controller, string RecursoService )
        {

            string Response;
            try
            {
                Response= await _client.SendRequestNoInputParameters(Controller, RecursoService);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Hubo un error al momento de consumir el servicio : {ex.Message}");
                return string.Empty;
            }
            return Response;

        }

        public async Task<string> SendRequestWithParameters(string Controller, string RecursoService,
           Models.HttpMethodToUse method, object Parameters)
        {
            string Response;
            try
            {
                Response = await _client.SendRequestWithParameters(Controller, RecursoService, method, Parameters);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Hubo un error al momento de consumir el servicio {ex.Message}");
                return string.Empty;
            }
            return Response;


        }

    }
}
