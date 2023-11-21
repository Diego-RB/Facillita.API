using Facillita.API.Interfaces.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace Facillita.API.Controllers
{
    [AllowAnonymous]
    [ApiController]
    [Route("[controller]")]
    [Authorize(Roles = "regular,admin")]
    public class FinancialController : ControllerBase
    {
        private IFinancialService _financialService;

        public FinancialController(IFinancialService financialService)
        {
            _financialService = financialService;
        }

        [HttpGet("Search/{year}/{month}")]
        public IActionResult MonthSummary(int year, int month)
        {
            var summary = _financialService.MonthSummary(year, month);
            string json = JsonConvert.SerializeObject(summary, Formatting.Indented);
            return Ok(json);
        }

    }
}
