using Facillita.API.Interfaces.Services;
using Facillita.API.Models.Enum;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

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

        [HttpGet("search/{year}/{month}")]
        public IActionResult MonthSummary(string userUId, int year, int month)
        {
            var summary = _financialService.MonthSummary(userUId, year, month);
            string json = JsonConvert.SerializeObject(summary, Formatting.Indented);
            return Ok(json);
        }

        [HttpGet("extract")]
        public IActionResult GetExtract(string userUId, DateTime startDate, DateTime endDate, ExtractTypeEnum typeEnum = 0)
        {
            var result = _financialService.GetExtract(userUId, startDate, endDate, typeEnum);
            string json = JsonConvert.SerializeObject(result, Formatting.Indented);
            return Ok(json);
        }

        [HttpGet("extract-by-month")]
        public IActionResult GetExtract(string userUId, int year, int month, ExtractTypeEnum typeEnum = 0)
        {
            var result = _financialService.GetExtracByMonth(userUId, year, month, typeEnum);
            string json = JsonConvert.SerializeObject(result, Formatting.Indented);
            return Ok(json);
        }
    }
}
