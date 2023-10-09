using AutoMapper;
using TesteTecnicoImobiliaria.Modelo.Interfaces;
using TesteTecnicoImobiliaria.Modelo.Interfaces.Regra;
using TesteTecnicoImobiliaria.Modelo.Models;
using TesteTecnicoImobiliaria.Modelo.ViewModels;

namespace TesteTecnicoImobiliaria.Regra
{
    internal class RnImovel : IRnImovel
    {
        private readonly IMapper mapper;
        private readonly IImovelDAL imovelDAL;

        public RnImovel(IMapper mapper, IImovelDAL imovelDAL)
        {
            this.mapper = mapper;
            this.imovelDAL = imovelDAL;
        }

        public ImovelViewModel SelecionarImovel(int id)
        {
            ImovelModel ImovelModel = imovelDAL.SelecionarImovel(id);
            var Imovel = mapper.Map<ImovelViewModel>(ImovelModel);

            return Imovel;
        }

        public void AtivarImovel(int id)
        {
            imovelDAL.AtivarImovel(id);
        }

        public void DesativarImovel(int id)
        {
            imovelDAL.DesativarImovel(id);
        }

        public List<ImovelViewModel> ListarImoveis()
        {
            var retorno = new List<ImovelViewModel>();
            var Imovels = imovelDAL.ListarImoveis();
            retorno = mapper.Map<List<ImovelViewModel>>(Imovels);

            return retorno;
        }

        public void SalvarImovel(ImovelViewModel Imovel)
        {
            ImovelModel ImovelModel = mapper.Map<ImovelModel>(Imovel);
            if (Imovel.Id == 0)
            {
                imovelDAL.CadastrarImovel(ImovelModel);
            }
            else
            {
                imovelDAL.AtualizarImovel(ImovelModel);
            }
        }
    }
}
