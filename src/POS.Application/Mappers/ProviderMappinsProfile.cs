using AutoMapper;
using POS.Application.Dtos.Provider.Request;
using POS.Application.Dtos.Provider.Response;
using POS.Domain.Entities;
using POS.Infraestructure.Commons.Bases.Response;
using POS.Utilities.Static;

namespace POS.Application.Mappers
{
    public class ProviderMappinsProfile : Profile
    {
        public ProviderMappinsProfile()
        {
            CreateMap<Provider, ProviderResponseDto>()
                .ForMember(x => x.ProviderId, x => x.MapFrom(y => y.Id))
                .ForMember(x => x.DocumentType, x => x.MapFrom(y => y.DocumentType.Abbreviation)) 
                .ForMember(x => x.StateProvider,
                    x => x.MapFrom(y => y.State.Equals((int)StateTypes.Active) ? "Activo" : "Inactivo"))
                .ReverseMap();

            CreateMap<BaseEntityResponse<Provider>, BaseEntityResponse<ProviderResponseDto>>()
                .ReverseMap();

            CreateMap<ProviderRequestDto, Provider>();
        }
    }
}
