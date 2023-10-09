using AutoMapper;

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
        }
    }
}