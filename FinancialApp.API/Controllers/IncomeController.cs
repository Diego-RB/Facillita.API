using Facillita.API.Data.Dtos.Income;
using Facillita.API.Interfaces.Services;
using FluentResults;
using Microsoft.AspNetCore.Mvc;
using System.Globalization;

namespace Facillita.API.Controllers
{
    [ApiController]
    [Route("income")]
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

        [HttpPost("add-mobile")]
        public IActionResult AddIncome(
            [FromBody] string incomeName,
            [FromBody] double incomeAmount,
            [FromBody] DateTime incomeDate,
            [FromBody] string userUID)
        {
            var incomeDto = new CreateIncomeDto()
            {
                IncomeName = incomeName,
                IncomeAmount = incomeAmount,
                IncomeDate = incomeDate,
                UserUID = userUID
            };
            ReadIncomeDto readDto = _incomeService.AddIncome(incomeDto);

            //If there's already an income with same description in the same month, it'll return null from AddIncome method
            if (readDto != null)
                return CreatedAtAction(nameof(ListIncomeById), new { Id = readDto.IncomeId }, readDto);

            return BadRequest($"Income with same name already exists in {CultureInfo.GetCultureInfo("en-Us").DateTimeFormat.GetMonthName(incomeDto.IncomeDate.Month)}");
        }


        [HttpGet]
        public IActionResult ListIncomes(string userUId)
        {
            List<ReadIncomeDto> readDto = _incomeService.ListIncomes(userUId);
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

        [HttpGet("search/{description}")]
        public IActionResult ListIncomeByDescription(string userUId, string description)
        {
            List<ReadIncomeDto> readListDto = _incomeService.ListIncomeByDescription(userUId, description);
            if (readListDto != null)
                return Ok(readListDto);

            return NotFound();
        }

        [HttpGet("search/{year}/{month}")]
        public IActionResult ListIncomeByMonthOfYear(string userUId, int year, int month)
        {
            List<ReadIncomeDto> readListDto = _incomeService.ListIncomeByMonthOfYear(userUId, year, month);
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
