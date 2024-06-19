using KGRC_Evaluacion.Models;

namespace KGRC_Evaluacion.Interface
{
    public interface IRestClient
    {
        Task<string> SendRequestNoInputParameters(string controller, string recursoService);
        Task<string> SendRequestWithParameters(string controller, string recursoService, HttpMethodToUse method, object parameters);
    }
}
