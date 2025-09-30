using System.Threading;
using System.Threading.Tasks;

namespace TesteTecnicoImobiliaria.Regra
{
    internal interface IViaCepService
    {
        Task<ViaCepEndereco?> ObterEnderecoPorCepAsync(string cep, CancellationToken cancellationToken = default);
    }
}
