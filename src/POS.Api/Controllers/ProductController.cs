using Microsoft.AspNetCore.Mvc;
using POS.Application.Commons.Bases.Request;
using POS.Application.Dtos.Product.Request;
using POS.Application.Interfaces;
using POS.Utilities.Static;

namespace POS.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductApplication _productApplication;
        private readonly IGenerateExcelApplication _generateExcelApplication;

        public ProductController(IProductApplication productApplication, IGenerateExcelApplication generateExcelApplication)
        {
            _productApplication = productApplication;
            _generateExcelApplication = generateExcelApplication;
        }

        [HttpGet]
        public async Task<IActionResult> ListProducts([FromQuery] BaseFiltersRequest filters)
        {
            var response = await _productApplication.ListProducts(filters);

            if ((bool)filters.Download!)
            {
                var columnsNames = ExcelColumnNames.GetColumnsProduts();
                var fileBytes = _generateExcelApplication.GenerateToExcel(
                    response.Data!, columnsNames
                    );
                return File(fileBytes, ContentType.ContentTypeExcel);
            }

            return Ok(response);
        }

        [HttpGet("{productId:int}")]
        public async Task<IActionResult> ProductById(int productId)
        {
            var response = await _productApplication.GetProductById(productId);
            return Ok(response);
        }

        [HttpPost("Register")]
        public async Task<IActionResult> RegisterProduct([FromBody] ProductRequestDto requestDto)
        {
            var response = await _productApplication.RegisterProduct(requestDto);
            return Ok(response);
        }

        [HttpPut("Edit/{productId:int}")]
        public async Task<IActionResult> EditProduct(int productId, [FromBody] ProductRequestDto requestDto)
    {
        var response = await _productApplication.EditProductAsync(productId, requestDto);
        return Ok(response);
    }

    [HttpPut("Remove/{productId:int}")]
        public async Task<IActionResult> RemoveProduct(int productId)
        {
            var response = await _productApplication.RemoveProductAsync(productId);
            return Ok(response);
        }
    }
}
