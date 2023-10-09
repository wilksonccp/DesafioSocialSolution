using TesteTecnicoImobiliaria.Modelo.Interfaces;
using TesteTecnicoImobiliaria.Modelo.Interfaces.DAL;

namespace TesteTecnicoImobiliaria.DAL.IoC
{
    public static class Modulo
    {
        public static Dictionary<Type, Type> GetTypes()
        {
            var dic = new Dictionary<Type, Type>();

            dic.Add(typeof(IContextDAL), typeof(ContextDAL));
            dic.Add(typeof(IClienteDAL), typeof(ClienteDAL));
            dic.Add(typeof(IImovelDAL), typeof(ImovelDAL));

            return dic;
        }
    }
}