using AutoMapper;
using Microsoft.EntityFrameworkCore;
using POS.Application.Commons.Bases.Request;
using POS.Application.Commons.Bases.Response;
using POS.Application.Commons.Ordering;
using POS.Application.Dtos.DocumentType.Request;
using POS.Application.Dtos.DocumentType.Response;
using POS.Application.Interfaces;
using POS.Application.Validators.DocumentType;
using POS.Domain.Entities;
using POS.Infraestructure.Persistences.Interfaces;
using POS.Utilities.Static;
using System.Linq.Dynamic.Core;
using WatchDog;

namespace POS.Application.Services
{
    public class DocumentTypeApplication : IDocumentTypeApplication
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly DocumentTypeValidator _validationRules;
        private readonly IOrderingQuery _orderingQuery;

        public DocumentTypeApplication(IUnitOfWork unitOfWork, 
            IMapper mapper,
            DocumentTypeValidator validationRules,
            IOrderingQuery orderingQuery
            )
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _validationRules = validationRules;
            _orderingQuery = orderingQuery;
        }

        public async Task<BaseResponse<IEnumerable<DocumentTypeResponseDto>>> ListDocumentTypes(BaseFiltersRequest filters)
        {
            var response = new BaseResponse<IEnumerable<DocumentTypeResponseDto>>();

            try
            {
                var documentTypes = _unitOfWork.DocumentType.GetAllQueryable();

                if(filters.NumFilter is not null && !string.IsNullOrEmpty(filters.TextFilter))
                {
                    switch (filters.NumFilter)
                    {
                        case 1: 
                            documentTypes = documentTypes.Where(x => x.Name!.Contains(filters.TextFilter)); 
                            break;  
                       case 2:
                            documentTypes = documentTypes.Where(x => x.Abbreviation!.Contains(filters.TextFilter));
                            break;
                    }
                }

                if (filters.StateFilter is not null)
                {
                    documentTypes = documentTypes.Where(x => x.State.Equals(filters.StateFilter));
                }

                if(!string.IsNullOrEmpty(filters.StartDate) && !string.IsNullOrEmpty(filters.EndDate))
                {
                    documentTypes = documentTypes.Where(x => x.AuditCreateDate >= Convert.ToDateTime(filters.StartDate)
                        && x.AuditCreateDate <= Convert.ToDateTime(filters.EndDate).AddDays(1));
                }

                if (filters.Sort is null) filters.Sort = "Id";

                var items = await _orderingQuery.Ordering(filters, documentTypes, !(bool)filters.Download!).ToListAsync();

                response.IsSuccess = true;
                response.TotalRecords = await documentTypes.CountAsync();
                response.Data = _mapper.Map<IEnumerable<DocumentTypeResponseDto>>(items);
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

        public async Task<BaseResponse<IEnumerable<DocumentTypeSelectResponseDto>>> ListSelectDocumentTypes()
        {
            var response = new BaseResponse<IEnumerable<DocumentTypeSelectResponseDto>>();

            try
            {
                var documentTypes = await _unitOfWork.DocumentType.GetSelctAsync();
                
                if(documentTypes is not null)
                {
                    response.Data = _mapper.Map<IEnumerable<DocumentTypeSelectResponseDto>>(documentTypes);
                    response.IsSuccess = true;
                    response.Message = ReplyMessage.MESSAGE_QUERY;
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

        public async Task<BaseResponse<DocumentTypeResponseDto>> DocumentTypeById(int documentTypeId)
        {
            var response = new BaseResponse<DocumentTypeResponseDto>();

            try
            {
                var documentType = await _unitOfWork.DocumentType.GetByIdAsync(documentTypeId);

                if (documentType is not null)
                {
                    response.IsSuccess = true;
                    response.Data = _mapper.Map<DocumentTypeResponseDto>(documentType);
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

        public async Task<BaseResponse<bool>> RegisterDocumentType(DocumentTypeRequestDto requestDto)
        {
            var response = new BaseResponse<bool>();
            try
            {
                var validationResult = await _validationRules.ValidateAsync(requestDto);

                if (!validationResult.IsValid)
                {
                    response.IsSuccess = false;
                    response.Message = ReplyMessage.MESSAGE_VALIDATE;
                    response.Errors = validationResult.Errors;
                    return response;
                }

                var documentType = _mapper.Map<DocumentType>(requestDto);
                response.Data = await _unitOfWork.DocumentType.RegisterAsync(documentType);

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

        public async Task<BaseResponse<bool>> EditDocumentType(int documentTypeId, DocumentTypeRequestDto requestDto)
        {
            var response = new BaseResponse<bool>();

            try
            {
                var documentTypeEdit = await DocumentTypeById(documentTypeId);

                if (documentTypeEdit.Data is null)
                {
                    response.IsSuccess = false;
                    response.Message = ReplyMessage.MESSAGE_QUERY_EMPTY;
                    return response;
                }

                var documentType = _mapper.Map<DocumentType>(requestDto);
                documentType.Id = documentTypeId;
                response.Data = await _unitOfWork.DocumentType.EditAsync(documentType);

                if (response.Data)
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

        public async Task<BaseResponse<bool>> RemoveDocumentType(int documentTypeId)
        {
            var response = new BaseResponse<bool>();

            try
            {
                var documentType = await DocumentTypeById(documentTypeId);

                if (documentType.Data is null)
                {
                    response.IsSuccess = false;
                    response.Message = ReplyMessage.MESSAGE_QUERY_EMPTY;
                }

                response.Data = await _unitOfWork.DocumentType.RemoveAsync(documentTypeId);

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
