using KGRC_Evaluacion.Conexion.Interface;
using Microsoft.Data.SqlClient;
using System.Data;

namespace KGRC_Evaluacion.Conexion
{
    public class ConexionContext : IConexionContext
    {
        private readonly IConfiguration _iConfiguration;
        private readonly string _connString;
        public ConexionContext(IConfiguration iConfiguration)
        {
            _iConfiguration = iConfiguration;
            _connString = _iConfiguration.GetConnectionString("CoctelConnection") ?? throw new InvalidOperationException("Connection string 'CoctelConnection' not found.");
            
        }
        public IDbConnection CreateConnection() => new SqlConnection(_connString);
    }
}
