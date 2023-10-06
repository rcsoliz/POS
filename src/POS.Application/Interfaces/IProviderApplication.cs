using POS.Application.Commons.Bases;
using POS.Application.Dtos.Provider.Response;
using POS.Infraestructure.Commons.Bases.Request;
using POS.Infraestructure.Commons.Bases.Response;

namespace POS.Application.Interfaces
{
    public interface IProviderApplication
    {
        Task<BaseResponse<BaseEntityResponse<ProviderResponseDto>>> ListProviders(BaseFiltersRequest filters);
        Task<BaseResponse<ProviderResponseDto>> GetProviderById(int providerId);


    }
}
