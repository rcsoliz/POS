using AutoMapper;
using Microsoft.EntityFrameworkCore;
using POS.Application.Commons.Bases.Request;
using POS.Application.Commons.Bases.Response;
using POS.Application.Commons.Ordering;
using POS.Application.Dtos.Client.Request;
using POS.Application.Dtos.Client.Response;
using POS.Application.Interfaces;
using POS.Domain.Entities;
using POS.Infraestructure.Persistences.Interfaces;
using POS.Utilities.Static;
using WatchDog;

namespace POS.Application.Services
{
    public class ClientApplication: IClientApplication
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IOrderingQuery _orderingQuery;

        public ClientApplication(IUnitOfWork unitOfWork, IMapper mapper, IOrderingQuery orderingQuery)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _orderingQuery = orderingQuery;
        }
        public async Task<BaseResponse<IEnumerable<ClientResponseDto>>> ListClients(BaseFiltersRequest filters)
        {
            var response = new BaseResponse<IEnumerable<ClientResponseDto>>(); 

            try
            {
                var clients = _unitOfWork.Client
                    .GetAllQueryable()
                    .Include(x => x.DocumentType)
                    .AsQueryable();

                if (filters.NumFilter is not null && !string.IsNullOrEmpty(filters.TextFilter))
                {
                    switch (filters.NumFilter)
                    {
                        case 1:
                            clients = clients.Where(x => x.Name!.Contains(filters.TextFilter)); break;
                        
                        case 2:
                            clients = clients.Where(x => x.Email!.Contains(filters.TextFilter)); break; 

                        case 3:
                            clients = clients.Where(x => x.DocumentNumber!.Contains(filters.TextFilter));break;

                    }
                }

                if (filters.StateFilter is not null)
                {
                    clients = clients.Where(x => x.State.Equals(filters.StateFilter));
                }

                if (filters.StartDate is not null && filters.EndDate is not null)
                {
                    clients = clients.Where(x =>
                        x.AuditCreateDate >= Convert.ToDateTime(filters.StartDate) &&
                        x.AuditCreateDate <= Convert.ToDateTime(filters.EndDate).AddDays(1));
                }
                if (filters.Sort is null) filters.Sort = "Id";

                var items = await _orderingQuery.Ordering(filters, clients, !(bool)filters.Download!).ToListAsync();

                response.IsSuccess = true;
                response.TotalRecords = await clients.CountAsync();
                response.Data = _mapper.Map<IEnumerable<ClientResponseDto>>(items);
                response.Message = ReplyMessage.MESSAGE_QUERY;

            }
            catch (Exception ex)
            {
                response.IsSuccess = false;
                response.Message = ReplyMessage.MESSAGE_EXCEPTION;
                WatchLogger.Log(ex.Message);
            }

            return response;

        }
        public async Task<BaseResponse<ClientByIdResponseDto>> GetClientById(int clientId)
        {
            var response = new BaseResponse<ClientByIdResponseDto>();

            try
            {
                var clients = await _unitOfWork.Client.GetByIdAsync(clientId);

                if(clients is not null)
                {
                    response.IsSuccess = true;
                    response.Data = _mapper.Map<ClientByIdResponseDto>(clients);
                    response.Message = ReplyMessage.MESSAGE_QUERY;
                }
                else
                {
                    response.IsSuccess = false;
                    response.Message = ReplyMessage.MESSAGE_QUERY_EMPTY;
                }
            }
            catch (Exception ex)
            {
                response.IsSuccess = false;
                response.Message = ReplyMessage.MESSAGE_EXCEPTION;
                WatchLogger.Log(ex.Message);
            }

            return response;
        }
        public async Task<BaseResponse<bool>> RegisterClient(ClientRequestDto requestDto)
        {
            var response = new BaseResponse<bool>();

            try
            {
                var clients = _mapper.Map<Client>(requestDto);

                response.Data = await _unitOfWork.Client.RegisterAsync(clients);

                if (response.Data)
                {
                    response.IsSuccess = true;
                    response.Message = ReplyMessage.MESSAGE_SAVE;
                }
                else
                {
                    response.IsSuccess = false;
                    response.Message = ReplyMessage.MESSAGE_FAILED;
                }
            }
            catch (Exception ex)
            {
                response.IsSuccess = false;
                response.Message = ReplyMessage.MESSAGE_EXCEPTION;
                WatchLogger.Log(ex.Message);
            }

            return response;
        }
        public async Task<BaseResponse<bool>> EdidtClientAsync(int clientId, ClientRequestDto requestDto)
        {
            var response = new BaseResponse<bool>();

            try
            {
                var clientById = await GetClientById(clientId);

                if(clientById.Data is null)
                {
                    response.IsSuccess = false;
                    response.Message = ReplyMessage.MESSAGE_QUERY_EMPTY;

                    return response;
                }

                var client = _mapper.Map<Client>(requestDto);
                client.Id = clientId;

                response.Data = await _unitOfWork.Client.EditAsync(client);

                if(response.Data)
                {
                    response.IsSuccess = true;
                    response.Message = ReplyMessage.MESSAGE_UPDATE;
                }
                else
                {
                    response.IsSuccess = false;
                    response.Message = ReplyMessage.MESSAGE_FAILED;
                }

            }
            catch (Exception ex)
            {
                response.IsSuccess = false;
                response.Message = ReplyMessage.MESSAGE_EXCEPTION;
                WatchLogger.Log(ex.Message);
            }
            return response;
        }
        public async Task<BaseResponse<bool>> RemoveClientAsync(int clientId)
        {
            var response = new BaseResponse<bool>();

            try
            {
                var clientById = await GetClientById(clientId);

                if (clientById.Data is null)
                {
                    response.IsSuccess = false;
                    response.Message = ReplyMessage.MESSAGE_QUERY_EMPTY;

                    return response;
                }

                response.Data = await _unitOfWork.Client.RemoveAsync(clientId);

                if (response.Data)
                {
                    response.IsSuccess = true;
                    response.Message = ReplyMessage.MESSAGE_DELETE;
                }
                else
                {
                    response.IsSuccess = false;
                    response.Message = ReplyMessage.MESSAGE_FAILED;
                }

            }
            catch (Exception ex)
            {
                response.IsSuccess = false;
                response.Message = ReplyMessage.MESSAGE_EXCEPTION;
                WatchLogger.Log(ex.Message);
            }
            return response;
        }
    }
}
