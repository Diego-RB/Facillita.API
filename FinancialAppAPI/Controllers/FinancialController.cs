using FinancialAppAPI.Data;
using FinancialAppAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using FinancialAppAPI.Services;
using FinancialAppAPI.Interfaces.Services;
using Microsoft.AspNetCore.Authorization;

namespace FinancialAppAPI.Controllers
{
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
