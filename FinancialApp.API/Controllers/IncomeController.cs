using Facillita.API.Data.Dtos.Income;
using Facillita.API.Interfaces.Services;
using FluentResults;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Globalization;

namespace Facillita.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [AllowAnonymous]
    //[Authorize(Roles = "regular,admin")]

    public class IncomeController : ControllerBase
    {
        private readonly IIncomeService _incomeService;

        public IncomeController(IIncomeService incomeService)
        {
            _incomeService = incomeService;
        }

        [HttpPost]
        public IActionResult AddIncome([FromBody] CreateIncomeDto incomeDto)
        {
            ReadIncomeDto readDto = _incomeService.AddIncome(incomeDto);

            //If there's already an income with same description in the same month, it'll return null from AddIncome method
            if (readDto != null)
                return CreatedAtAction(nameof(ListIncomeById), new { Id = readDto.IncomeId }, readDto);

            return BadRequest($"Income with same name already exists in {CultureInfo.GetCultureInfo("en-Us").DateTimeFormat.GetMonthName(incomeDto.IncomeDate.Month)}");
        }


        [HttpGet]
        public IActionResult ListIncomes()
        {
            List<ReadIncomeDto> readDto = _incomeService.ListIncomes();
            if (readDto != null)
                return Ok(readDto);

            return NotFound();
        }

        [HttpGet("{id}")]
        public IActionResult ListIncomeById(int id)
        {
            ReadIncomeDto readDto = _incomeService.ListIncomeById(id);
            if (readDto != null)
                return Ok(readDto);

            return NotFound();
        }

        [HttpGet("Search/{description}")]
        public IActionResult ListIncomeByDescription(string description)
        {
            List<ReadIncomeDto> readListDto = _incomeService.ListIncomeByDescription(description);
            if (readListDto != null)
                return Ok(readListDto);

            return NotFound();
        }

        [HttpGet("Search/{year}/{month}")]
        public IActionResult ListIncomeByMonthOfYear(int year, int month)
        {
            List<ReadIncomeDto> readListDto = _incomeService.ListIncomeByMonthOfYear(year, month);
            if (readListDto != null)
                return Ok(readListDto);

            return NotFound();
        }

        [HttpPut("{id}")]
        public IActionResult UpdateIncome(int id, [FromBody] UpdateIncomeDto updatedIncomeDto)
        {
            Result result = _incomeService.UpdateIncome(id, updatedIncomeDto);
            if (result.IsFailed)
                return BadRequest(result);

            return NoContent();

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
