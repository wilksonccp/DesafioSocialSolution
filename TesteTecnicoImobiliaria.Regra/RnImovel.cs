using System;
using System.Globalization;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
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
        private readonly IViaCepService viaCepService;

        public RnImovel(IMapper mapper, IImovelDAL imovelDAL, IViaCepService viaCepService)
        {
            this.mapper = mapper;
            this.imovelDAL = imovelDAL;
            this.viaCepService = viaCepService;
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

        public List<ImovelViewModel> ListarImoveis(string? valorMaximo = null, string? dataPublicacao = null, int? tipoNegocio = null)
        {
            var cultura = CultureInfo.CreateSpecificCulture("pt-BR");

            decimal? valorMaximoFiltrado = null;
            if (!string.IsNullOrWhiteSpace(valorMaximo))
            {
                if (!decimal.TryParse(valorMaximo, NumberStyles.Number, cultura, out var valor))
                {
                    throw new ArgumentException("Valor maximo invalido.");
                }

                valorMaximoFiltrado = valor;
            }

            DateTime? dataPublicacaoFiltrada = null;
            if (!string.IsNullOrWhiteSpace(dataPublicacao))
            {
                if (!DateTime.TryParse(dataPublicacao, cultura, DateTimeStyles.None, out var dataFiltro))
                {
                    throw new ArgumentException("Data de publicacao invalida.");
                }

                dataPublicacaoFiltrada = dataFiltro.Date;
            }

            var imoveis = imovelDAL.ListarImoveis(valorMaximoFiltrado, dataPublicacaoFiltrada, tipoNegocio);
            return mapper.Map<List<ImovelViewModel>>(imoveis);
        }

        public void SalvarImovel(ImovelViewModel Imovel)
        {
            ValidarImovel(Imovel);
            PreencherEndereco(Imovel);

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

        private void PreencherEndereco(ImovelViewModel imovel)
        {
            if (string.IsNullOrWhiteSpace(imovel.Cep))
            {
                throw new ArgumentException("Informe o CEP do imovel.");
            }

            var cepLimpo = imovel.Cep.LimparMascara();

            if (cepLimpo.Length != 8 || !cepLimpo.All(char.IsDigit))
            {
                throw new ArgumentException("CEP invalido.");
            }

            try
            {
                var endereco = viaCepService.ObterEnderecoPorCepAsync(cepLimpo).GetAwaiter().GetResult();

                if (endereco == null)
                {
                    throw new ArgumentException("CEP nao encontrado.");
                }

                imovel.Cep = cepLimpo;
                imovel.Logradouro = endereco.Logradouro ?? string.Empty;
                imovel.Complemento = endereco.Complemento ?? string.Empty;
                imovel.Bairro = endereco.Bairro ?? string.Empty;
                imovel.Localidade = endereco.Localidade ?? string.Empty;
                imovel.Uf = endereco.Uf ?? string.Empty;
            }
            catch (HttpRequestException ex)
            {
                throw new InvalidOperationException("Falha ao consultar endereco no ViaCEP.", ex);
            }
            catch (TaskCanceledException ex)
            {
                throw new InvalidOperationException("Falha ao consultar endereco no ViaCEP.", ex);
            }
        }

        private static void ValidarImovel(ImovelViewModel Imovel)
        {
            if (Imovel == null)
            {
                throw new ArgumentNullException(nameof(Imovel));
            }

            if (string.IsNullOrWhiteSpace(Imovel.DataPublicacao))
            {
                throw new ArgumentException("Informe a data de publicacao do imovel.");
            }

            var cultura = CultureInfo.CreateSpecificCulture("pt-BR");
            if (!DateTime.TryParse(Imovel.DataPublicacao, cultura, DateTimeStyles.None, out var dataPublicacao))
            {
                throw new ArgumentException("Data de publicacao invalida.");
            }

            if (dataPublicacao.Date < DateTime.Today)
            {
                throw new ArgumentException("Data de publicacao nao pode ser anterior a hoje.");
            }
        }
    }
}
