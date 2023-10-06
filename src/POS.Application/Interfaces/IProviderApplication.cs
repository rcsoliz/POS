using POS.Application.Commons.Bases;
using POS.Application.Dtos.Provider.Request;
using POS.Application.Dtos.Provider.Response;
using POS.Infraestructure.Commons.Bases.Request;
using POS.Infraestructure.Commons.Bases.Response;

namespace POS.Application.Interfaces
{
    public interface IProviderApplication
    {
        Task<BaseResponse<BaseEntityResponse<ProviderResponseDto>>> ListProviders(BaseFiltersRequest filters);
        Task<BaseResponse<ProviderResponseDto>> GetProviderById(int providerId);

        Task<BaseResponse<bool>> RegisterProvider(ProviderRequestDto requestDto);
        Task<BaseResponse<bool>> EdidtProviderAsync(int providerId, ProviderRequestDto requestDto);
    }
}
