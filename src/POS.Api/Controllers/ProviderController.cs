﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using POS.Application.Interfaces;
using POS.Infraestructure.Commons.Bases.Request;

namespace POS.Api.Controllers
{
 //   [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class ProviderController : ControllerBase
    {
        private readonly IProviderApplication _providerApplication;

        public ProviderController(IProviderApplication providerApplication)
        {
            _providerApplication = providerApplication;
        }

        [HttpPost]
        public async Task<IActionResult> ListProviders([FromBody] BaseFiltersRequest filters)
        {
            var response = await _providerApplication.ListProviders(filters);
            return Ok(response);
        }

        [HttpGet("{providerId:int}")]
        public async Task<IActionResult> ProviderById(int providerId)
        {
            var response = await _providerApplication.GetProviderById(providerId);
            return Ok(response);
        }
    }
}
