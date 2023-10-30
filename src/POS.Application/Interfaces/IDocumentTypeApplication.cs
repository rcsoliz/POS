using POS.Application.Commons.Bases.Request;
using POS.Application.Commons.Bases.Response;
using POS.Application.Dtos.DocumentType.Request;
using POS.Application.Dtos.DocumentType.Response;

namespace POS.Application.Interfaces
{
    public interface IDocumentTypeApplication
    {
        Task<BaseResponse<IEnumerable<DocumentTypeResponseDto>>> ListDocumentTypes(BaseFiltersRequest filters);
        Task<BaseResponse<IEnumerable<DocumentTypeSelectResponseDto>>> ListSelectDocumentTypes();

        Task<BaseResponse<DocumentTypeResponseDto>> DocumentTypeById(int documentTypeId);
        Task<BaseResponse<bool>> RegisterDocumentType(DocumentTypeRequestDto requestDto);
        Task<BaseResponse<bool>> EditDocumentType(int documentTypeId, DocumentTypeRequestDto requestDto);
        Task<BaseResponse<bool>> RemoveDocumentType(int documentTypeId);
    
    }
}
