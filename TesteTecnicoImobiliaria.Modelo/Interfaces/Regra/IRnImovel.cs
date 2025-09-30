using TesteTecnicoImobiliaria.Modelo.ViewModels;

namespace TesteTecnicoImobiliaria.Modelo.Interfaces.Regra
{
    public interface IRnImovel
    {
        ImovelViewModel SelecionarImovel(int id);
        void SalvarImovel(ImovelViewModel Imovel);
        List<ImovelViewModel> ListarImoveis(string? valorMaximo = null, string? dataPublicacao = null, int? tipoNegocio = null);
        void DesativarImovel(int id);
        void AtivarImovel(int id);
    }
}
