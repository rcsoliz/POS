using AutoMapper;
using Microsoft.Extensions.Configuration;
using POS.Application.Commons.Bases.Response;
using POS.Application.Dtos.User.Request;
using POS.Application.Interfaces;
using POS.Domain.Entities;
using POS.Infraestructure.FileStorage;
using POS.Infraestructure.Persistences.Interfaces;
using POS.Utilities.Static;
using WatchDog;
using BC = BCrypt.Net.BCrypt;

namespace POS.Application.Services
{
    public class UserApplication : IUserApplication
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IConfiguration _configuration;
        private readonly IAzureStorage _storage;
        public UserApplication(IUnitOfWork unitOfWork,
               IMapper mapper,
               IConfiguration configuration,
               IAzureStorage storage)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _configuration = configuration;
            _storage = storage;
        }

        public async Task<BaseResponse<bool>> RegisterUser(UserRequestDto requestDto)
        {
            var  response = new BaseResponse<bool>();

            try
            {
                var account = _mapper.Map<User>(requestDto);
                account.Password = BC.HashPassword(account.Password);

                if (requestDto.Image is not null)
                {
                    account.Image = await _storage.SaveFile(
                            AzureContainers.USERS,
                            requestDto.Image
                        );
                }

                response.Data = await _unitOfWork.User.RegisterAsync(account);

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



    }
}
