using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using POS.Application.Commons.Bases.Request;
using POS.Application.Dtos.DocumentType.Request;
using POS.Application.Interfaces;
using POS.Utilities.Static;

namespace POS.Api.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class DocumentTypeController : ControllerBase
    {
        private readonly IDocumentTypeApplication _documentTypeApplication;
        private readonly IGenerateExcelApplication _generateExcelApplication;

        public DocumentTypeController(IDocumentTypeApplication documentTypeApplication, IGenerateExcelApplication generateExcelApplication)
        {
            _documentTypeApplication = documentTypeApplication;
            _generateExcelApplication = generateExcelApplication;
        }

        [HttpGet]
        public async Task<IActionResult> ListDocumentTypes([FromQuery] BaseFiltersRequest filters)
        {
            var response = await _documentTypeApplication.ListDocumentTypes(filters);

            if ((bool)filters.Download!)
            {
                var columnNames = ExcelColumnNames.GetColumnsDocumentTypes();
                var fileBytes = _generateExcelApplication.GenerateToExcel(
                        response.Data!, columnNames);

                return File(fileBytes, ContentType.ContentTypeExcel);
            }

            return Ok(response);
        }


        [HttpGet("Select")]
        public async Task<IActionResult> ListSelectDocumentTypes()// ListDocumentTypes()
        {
            var response = await _documentTypeApplication.ListSelectDocumentTypes();
            return Ok(response);
        }

        [HttpGet("{documentTypeId:int}")]
        public async Task<IActionResult> DocumentTypeById(int documentTypeId)
        {
            var response = await _documentTypeApplication.DocumentTypeById(documentTypeId);
            return Ok(response);
        }

        [HttpPost("Register")]
        public async Task<IActionResult> RegisterDocumentType([FromBody] DocumentTypeRequestDto requestDto)
        {
            var response = await _documentTypeApplication.RegisterDocumentType(requestDto);
            return Ok(response);
        }

        [HttpPut("Edit/{documentTypeId:int}")]
        public async Task<IActionResult> EditDocumentType(int documentTypeId,
            [FromBody] DocumentTypeRequestDto requestDto)
        {
            var response = await _documentTypeApplication.EditDocumentType(documentTypeId, requestDto);
            return Ok(response);
        }

        [HttpPut("Remove/{documentTypeId:int}")]
        public async Task<IActionResult> RemoveDocumentType(int documentTypeId)
        {
            var response = await _documentTypeApplication.RemoveDocumentType(documentTypeId);
            return Ok(response);
        }

    }
}
