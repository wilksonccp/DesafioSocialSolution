using TesteTecnicoImobiliaria.Modelo.Interfaces.Regra;

namespace TesteTecnicoImobiliaria.Regra.IoC
{
    public static class Modulo
    {
        public static Dictionary<Type, Type> GetTypes()
        {
            var dic = new Dictionary<Type, Type>();

            dic.Add(typeof(IRnCliente), typeof(RnCliente));
            dic.Add(typeof(IRnImovel), typeof(RnImovel));

            return dic;
        }
    }
}
