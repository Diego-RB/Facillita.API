using Facillita.API.Interfaces.Services;
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
        public IActionResult GetExtract(string userUId)
        {
            var summary = _financialService.GetExtract(userUId);
            string json = JsonConvert.SerializeObject(summary, Formatting.Indented);
            return Ok(json);
        }

    }
}
