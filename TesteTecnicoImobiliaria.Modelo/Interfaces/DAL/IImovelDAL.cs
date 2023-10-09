using TesteTecnicoImobiliaria.Modelo.Models;

namespace TesteTecnicoImobiliaria.Modelo.Interfaces
{
    public interface IImovelDAL
    {
        void CadastrarImovel(ImovelModel Imovel);
        void AtualizarImovel(ImovelModel Imovel);
        List<ImovelModel> ListarImoveis();
        void DesativarImovel(int id);
        void AtivarImovel(int id);
        ImovelModel SelecionarImovel(int id);
    }
}
