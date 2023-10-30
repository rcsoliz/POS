using AutoMapper;
using POS.Application.Dtos.DocumentType.Request;
using POS.Application.Dtos.DocumentType.Response;
using POS.Domain.Entities;
using POS.Utilities.Static;

namespace POS.Application.Mappers
{
    public class DocumentTypeMappingsProfile: Profile
    {
        public DocumentTypeMappingsProfile()
        {

            CreateMap<DocumentType, DocumentTypeResponseDto>()
                .ForMember(x => x.DocumentTypeId, x => x.MapFrom(y => y.Id))
                .ForMember(x => x.StateDocumentType, x => x.MapFrom(
                    y => y.State.Equals((int)StateTypes.Active) ? "Activo" : "Inactivo"))
                .ReverseMap();

            CreateMap<DocumentTypeRequestDto, DocumentType>();


            CreateMap<DocumentType, DocumentTypeSelectResponseDto>()
                .ForMember(x => x.DocumentTypeId, x => x.MapFrom(y => y.Id))
                .ReverseMap();

        }
    }
}
