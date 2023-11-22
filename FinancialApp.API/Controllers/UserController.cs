using Facillita.API.Data.Dtos.Income;
using Facillita.API.Interfaces.Services;
using FluentResults;
using Microsoft.AspNetCore.Mvc;
using System.Globalization;

namespace Facillita.API.Controllers
{
    [ApiController]
    [Route("User")]
    public class UserController : ControllerBase
    {
        private readonly IIncomeService _incomeService;

        public UserController(IIncomeService incomeService)
        {
            _incomeService = incomeService;
        }

        [HttpPost]
        public IActionResult AddIncome([FromBody] CreateIncomeDto incomeDto)
        {
            ReadIncomeDto readDto = _incomeService.AddIncome(incomeDto);

            //If there's already an income with same description in the same month, it'll return null from AddIncome method
            if (readDto != null)
                return Ok();

            return BadRequest($"Income with same name already exists in {CultureInfo.GetCultureInfo("en-Us").DateTimeFormat.GetMonthName(incomeDto.IncomeDate.Month)}");
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteIncome(int id)
        {
            Result result = _incomeService.DeleteIncome(id);
            if (result.IsFailed)
                return NotFound();

            return NoContent();
        }





    }
}
