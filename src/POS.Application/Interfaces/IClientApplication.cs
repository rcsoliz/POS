using POS.Application.Commons.Bases.Request;
using POS.Application.Commons.Bases.Response;
using POS.Application.Dtos.Client.Request;
using POS.Application.Dtos.Client.Response;

namespace POS.Application.Interfaces
{
    public interface IClientApplication
    {
        Task<BaseResponse<IEnumerable<ClientResponseDto>>> ListClients(BaseFiltersRequest filters);
        Task<BaseResponse<ClientByIdResponseDto>> GetClientById(int clientId);

        Task<BaseResponse<bool>> RegisterClient(ClientRequestDto requestDto);
        Task<BaseResponse<bool>> EdidtClientAsync(int clientId, ClientRequestDto requestDto);
        Task<BaseResponse<bool>> RemoveClientAsync(int clientId);
    }
}
