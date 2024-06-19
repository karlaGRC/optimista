
namespace KGRC_Evaluacion.Models
{
    public enum StatusCode
    {
        Ok = 200,
        Status = 1,
        Fail = 500,
        NotFound = 404,
        BadRequest = 400,
        SinInformacion = 3,
        Cero = 0
    }

    public enum HttpMethodToUse
    {
        GET = 1,
        POST = 2,
        PUT = 3,
        DELETE = 4

    }
}
