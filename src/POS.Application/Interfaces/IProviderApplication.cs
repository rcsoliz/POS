using POS.Application.Commons.Bases.Request;
using POS.Application.Commons.Bases.Response;
using POS.Application.Dtos.Provider.Request;
using POS.Application.Dtos.Provider.Response;

namespace POS.Application.Interfaces
{
    public interface IProviderApplication
    {
        Task<BaseResponse<IEnumerable<ProductResponseDto>>> ListProviders(BaseFiltersRequest filters);
        Task<BaseResponse<ProviderByIdResponseDto>> GetProviderById(int providerId);

        Task<BaseResponse<bool>> RegisterProvider(ProviderRequestDto requestDto);
        Task<BaseResponse<bool>> EditProviderAsync(int providerId, ProviderRequestDto requestDto);
        Task<BaseResponse<bool>> RemoveProviderAsync(int providerId);
    }
}
