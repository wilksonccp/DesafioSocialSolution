using AutoMapper;
using System.Globalization;

namespace TesteTecnicoImobiliaria.Regra
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Modelo.ViewModels.ClienteViewModel, Modelo.Models.ClienteModel>()
                .ForMember(m => m.CD_CLIENTE, vm => vm.MapFrom(v => v.Id))
                .ForMember(m => m.NM_CLIENTE, vm => vm.MapFrom(v => v.Nome))
                .ForMember(m => m.DS_EMAIL, vm => vm.MapFrom(v => v.Email))
                .ForMember(m => m.NR_CPF, vm => vm.MapFrom(v => v.CPF.LimparMascara()))
                .ForMember(m => m.NR_CNPJ, vm => vm.MapFrom(v => v.CNPJ.LimparMascara()))
                .ForMember(m => m.FL_ATIVO, vm => vm.MapFrom(v => v.Ativo))
                .ReverseMap()
                .ForMember(m => m.CPF, vm => vm.MapFrom(v => v.NR_CPF.Mascarar("###.###.###-##")))
                .ForMember(m => m.CNPJ, vm => vm.MapFrom(v => v.NR_CNPJ.Mascarar("##.###.###/####-##")));

            CreateMap<Modelo.ViewModels.ImovelViewModel, Modelo.Models.ImovelModel>()
                .ForMember(m => m.CD_IMOVEL, vm => vm.MapFrom(v => v.Id))
                .ForMember(m => m.CD_CLIENTE, vm => vm.MapFrom(v => v.IdCliente))
                .ForMember(m => m.CD_TIPO_IMOVEL, vm => vm.MapFrom(v => v.IdTipoImovel))
                .ForMember(m => m.VL_IMOVEL, vm => vm.MapFrom(v => Decimal.Parse(v.ValorImovel, CultureInfo.CreateSpecificCulture("pt-BR"))))
                .ForMember(m => m.DT_PUBLICACAO, vm => vm.MapFrom(v => DateTime.Parse(v.DataPublicacao, CultureInfo.CreateSpecificCulture("pt-BR"))))
                .ForMember(m => m.DS_IMOVEL, vm => vm.MapFrom(v => v.Descricao))
                .ForMember(m => m.FL_ATIVO, vm => vm.MapFrom(v => v.Ativo))
                .ForMember(m => m.NR_CEP, vm => vm.MapFrom(v => v.Cep.LimparMascara()))
                .ForMember(m => m.NM_LOGRADOURO, vm => vm.MapFrom(v => v.Logradouro))
                .ForMember(m => m.DS_COMPLEMENTO, vm => vm.MapFrom(v => v.Complemento))
                .ForMember(m => m.NM_BAIRRO, vm => vm.MapFrom(v => v.Bairro))
                .ForMember(m => m.NM_LOCALIDADE, vm => vm.MapFrom(v => v.Localidade))
                .ForMember(m => m.SG_UF, vm => vm.MapFrom(v => v.Uf))
                .ReverseMap()
                .ForMember(m => m.ValorImovel, vm => vm.MapFrom(v => v.VL_IMOVEL.ToString(CultureInfo.CreateSpecificCulture("pt-BR"))))
                .ForMember(m => m.DataPublicacao, vm => vm.MapFrom(v => v.DT_PUBLICACAO.ToString("dd/MM/yyyy")))
                .ForMember(m => m.Cep, vm => vm.MapFrom(v => v.NR_CEP.Mascarar("#####-###")))
                .ForMember(m => m.Logradouro, vm => vm.MapFrom(v => v.NM_LOGRADOURO))
                .ForMember(m => m.Complemento, vm => vm.MapFrom(v => v.DS_COMPLEMENTO))
                .ForMember(m => m.Bairro, vm => vm.MapFrom(v => v.NM_BAIRRO))
                .ForMember(m => m.Localidade, vm => vm.MapFrom(v => v.NM_LOCALIDADE))
                .ForMember(m => m.Uf, vm => vm.MapFrom(v => v.SG_UF));
        }
    }
}
