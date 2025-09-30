using TesteTecnicoImobiliaria.Modelo.Models;

namespace TesteTecnicoImobiliaria.Modelo.Interfaces
{
    public interface IImovelDAL
    {
        void CadastrarImovel(ImovelModel Imovel);
        void AtualizarImovel(ImovelModel Imovel);
        List<ImovelModel> ListarImoveis(decimal? valorMaximo = null, DateTime? dataPublicacao = null, int? tipoNegocio = null);
        void DesativarImovel(int id);
        void AtivarImovel(int id);
        ImovelModel SelecionarImovel(int id);
        bool ClientePossuiImovelAtivo(int idCliente);
    }
}
