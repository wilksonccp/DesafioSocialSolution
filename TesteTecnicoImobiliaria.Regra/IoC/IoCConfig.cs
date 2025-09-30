using System;
using Microsoft.Extensions.DependencyInjection;

namespace TesteTecnicoImobiliaria.Regra.IoC
{
    public static class IoCConfig
    {
        public static void Configure(IServiceCollection services)
        {
            Configure(services, Modulo.GetTypes());
            Configure(services, DAL.IoC.Modulo.GetTypes());
            services.AddHttpClient<IViaCepService, ViaCepService>(client =>
            {
                client.BaseAddress = new Uri("https://viacep.com.br");
            });
        }

        private static void Configure(IServiceCollection services, Dictionary<Type, Type> types)
        {
            foreach (var type in types)
            {
                services.AddScoped(type.Key, type.Value);
            }
        }
    }
}
