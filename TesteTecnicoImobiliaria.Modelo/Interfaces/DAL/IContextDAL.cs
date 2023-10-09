using System.Data;

namespace TesteTecnicoImobiliaria.Modelo.Interfaces.DAL
{
    public interface IContextDAL
    {
        IDbConnection CreateConnection();
    }
}
