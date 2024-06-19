using System.Data;

namespace KGRC_Evaluacion.Conexion.Interface
{
    public interface IConexionContext
    {
        IDbConnection CreateConnection();
    }
}
