﻿using AutoMapper;
using POS.Application.Commons.Bases;
using POS.Application.Dtos.Provider.Response;
using POS.Application.Interfaces;
using POS.Infraestructure.Commons.Bases.Request;
using POS.Infraestructure.Commons.Bases.Response;
using POS.Infraestructure.Persistences.Interfaces;
using POS.Utilities.Static;

namespace POS.Application.Services
{
    public class ProviderApplication : IProviderApplication
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public ProviderApplication(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<BaseResponse<BaseEntityResponse<ProviderResponseDto>>> ListProviders(BaseFiltersRequest filters)
        {
            var response = new BaseResponse<BaseEntityResponse<ProviderResponseDto>>();
            var providers = await _unitOfWork.Provider.ListProviders(filters);

            if(providers is not null)
            {
                response.IsSuccess = true;
                response.Data = _mapper.Map<BaseEntityResponse<ProviderResponseDto>>(providers);
                response.Message = ReplyMessage.MESSAGE_QUERY;
            }
            else
            {
                response.IsSuccess = false;
                response.Message = ReplyMessage.MESSAGE_QUERY_EMPTY;
            }

            return response;
        }

        public async Task<BaseResponse<ProviderResponseDto>> GetProviderById(int providerId)
        {
            var response = new BaseResponse<ProviderResponseDto>();
            var providers = await _unitOfWork.Provider.GetByIdAsync(providerId);
            
            if(providers is not null)
            {
                response.IsSuccess = true;
                response.Data = _mapper.Map<ProviderResponseDto>(providers);
                response.Message = ReplyMessage.MESSAGE_QUERY;
            }
            else
            {
                response.IsSuccess = false;
                response.Message = ReplyMessage.MESSAGE_QUERY_EMPTY;
            }

            return response;
        }


    }
}
