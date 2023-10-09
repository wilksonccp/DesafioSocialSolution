using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System.Data;
using TesteTecnicoImobiliaria.Modelo.Interfaces.DAL;

namespace TesteTecnicoImobiliaria.DAL
{
    internal class ContextDAL : IContextDAL
    {
        private readonly IConfiguration _configuration;
        private readonly string _connectionString;

        public ContextDAL(IConfiguration configuration)
        {
            _configuration = configuration;
            _connectionString = _configuration.GetConnectionString("StringConexao");
        }

        public IDbConnection CreateConnection() => new SqlConnection(_connectionString);
    }
}
