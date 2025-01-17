﻿using Facillita.API.Interfaces.Services;
using Facillita.API.Models.Enum;
using Microsoft.AspNetCore.Mvc;

namespace Facillita.API.Controllers
{
    [ApiController]
    [Route("financial")]
    public class FinancialController : ControllerBase
    {
        private IFinancialService _financialService;

        public FinancialController(IFinancialService financialService)
        {
            _financialService = financialService;
        }

        [HttpGet("search")]
        public IActionResult MonthSummary(string userUId, int year, int month)
        {
            var summary = _financialService.MonthSummary(userUId, year, month);
            return Ok(summary);
        }

        [HttpGet("extract")]
        public IActionResult GetExtract(string userUId, DateTime startDate, DateTime endDate, ExtractTypeEnum typeEnum = 0)
        {
            var result = _financialService.GetExtract(userUId, startDate, endDate, typeEnum);
            return Ok(result);
        }

        [HttpGet("extract-by-month")]
        public IActionResult GetExtract(string userUId, int year, int month, ExtractTypeEnum typeEnum = 0)
        {
            var result = _financialService.GetExtracByMonth(userUId, year, month, typeEnum);
            return Ok(result);
        }
    }
}
