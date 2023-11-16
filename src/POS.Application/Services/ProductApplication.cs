using AutoMapper;
using Microsoft.EntityFrameworkCore;
using POS.Application.Commons.Bases.Request;
using POS.Application.Commons.Bases.Response;
using POS.Application.Commons.Ordering;
using POS.Application.Dtos.Product.Request;
using POS.Application.Dtos.Product.Response;
using POS.Application.Interfaces;
using POS.Domain.Entities;
using POS.Infraestructure.Persistences.Interfaces;
using POS.Utilities.Static;
using WatchDog;

namespace POS.Application.Services
{
    public class ProductApplication : IProductApplication
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IOrderingQuery _orderingQuery;

        public ProductApplication(IUnitOfWork unitOfWork, IMapper mapper, IOrderingQuery orderingQuery)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _orderingQuery = orderingQuery;
        }

        public async Task<BaseResponse<IEnumerable<ProductResponseDto>>> ListProducts(BaseFiltersRequest filters)
        {
            var response = new BaseResponse<IEnumerable<ProductResponseDto>>();

            try
            {
                var products = _unitOfWork.Product
                    .GetAllQueryable()
                  //  .Include(x => x.Provider)
                    .Include(x => x.Category)
                    .AsQueryable();

                if(filters.NumFilter is not null && !string.IsNullOrEmpty(filters.TextFilter))
                {
                    switch(filters.NumFilter) 
                    {
                        case 1:
                            products = products.Where(x => x.Name.Contains(filters.TextFilter)); break;

                        case 2:
                            products = products.Where(x => x.Category.Name.Contains(filters.TextFilter)); break;

                      //  case 3:
                        //    products = products.Where(x => x.Provider.Name.Contains(filters.TextFilter)); break;
                    }
                }

                if(filters.StateFilter is not null)
                {
                    products = products.Where(x => x.State.Equals(filters.StateFilter));
                }

                if(filters.StartDate is not null && filters.EndDate is not null)
                {
                    products = products.Where(x =>
                        x.AuditCreateDate >= Convert.ToDateTime(filters.StartDate) &&
                        x.AuditCreateDate <= Convert.ToDateTime(filters.EndDate).AddDays(1));
                }

                if (filters.Sort is null) filters.Sort = "Id";

                var items = await _orderingQuery.Ordering(filters, products, !(bool)filters.Download!).ToListAsync();

                response.IsSuccess = true;
                response.TotalRecords = await products.CountAsync();
                response.Data = _mapper.Map<IEnumerable<ProductResponseDto>>(items);
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

        public async Task<BaseResponse<ProductByIdResponseDto>> GetProductById(int productId)
        {
            var response = new BaseResponse<ProductByIdResponseDto>();

            try
            {
                var products = await _unitOfWork.Product.GetByIdAsync(productId);

                if(products is not null)
                {
                    response.IsSuccess = true;
                    response.Data = _mapper.Map<ProductByIdResponseDto>(products);
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

        public async Task<BaseResponse<bool>> RegisterProduct(ProductRequestDto requestDto)
        {
            var response = new BaseResponse<bool>();

            try
            {
                var product = _mapper.Map<Product>(requestDto);

                response.Data = await _unitOfWork.Product.RegisterAsync(product);

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

        public async Task<BaseResponse<bool>> EditProductAsync(int productId, ProductRequestDto requestDto)
        {
            var response = new BaseResponse<bool>();

            try
            {
                var productById = await GetProductById(productId);

                if (productById.Data is null)
                {
                    response.IsSuccess = false;
                    response.Message = ReplyMessage.MESSAGE_QUERY_EMPTY;

                    return response;
                }

                var product = _mapper.Map<Product>(requestDto);
                product.Id = productId;

                response.Data = await _unitOfWork.Product.EditAsync(product);

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

        public async Task<BaseResponse<bool>> RemoveProductAsync(int productId)
        {
            var response = new BaseResponse<bool>();

            try
            {
                var productById = await GetProductById(productId);

                if (productById.Data is null)
                {
                    response.IsSuccess = false;
                    response.Message = ReplyMessage.MESSAGE_QUERY_EMPTY;

                    return response;
                }

                response.Data = await _unitOfWork.Product.RemoveAsync(productId);

                if (response.Data)
                {
                    response.IsSuccess = true;
                    response.Message = ReplyMessage.MESSAGE_DELETE;
                }
                else
                {
                    response.IsSuccess = false;
                    response.Message = ReplyMessage.MESSAGE_DELETE;
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
