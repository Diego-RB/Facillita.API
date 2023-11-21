using Facillita.API.Data.Dtos.Expense;
using Facillita.API.Interfaces.Services;
using FluentResults;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Globalization;

namespace Facillita.API.Controllers
{
    [AllowAnonymous]
    [ApiController]
    [Route("[controller]")]
    [Authorize(Roles = "regular,admin")]
    public class ExpenseController : ControllerBase
    {
        private IExpenseService _expenseService;

        public ExpenseController(IExpenseService expenseService)
        {
            _expenseService = expenseService;
        }

        [HttpPost]
        public IActionResult AddExpense([FromBody] CreateExpenseDto expenseDto)
        {
            ReadExpenseDto readDto = _expenseService.AddExpense(expenseDto);

            //If there's already an expense with same description in the same month, it'll return null from AddExpense method
            if (readDto != null)
                return CreatedAtAction(nameof(ListExpenseById), new { Id = readDto.ExpenseId }, readDto);

            return BadRequest($"Expense with same name already exists in {CultureInfo.GetCultureInfo("en-Us").DateTimeFormat.GetMonthName(expenseDto.ExpenseDate.Month)}");
        }


        [HttpGet]
        public IActionResult ListExpenses()
        {
            List<ReadExpenseDto> readDto = _expenseService.ListExpenses();
            if (readDto != null)
                return Ok(readDto);

            return NotFound();
        }

        [HttpGet("{id}")]
        public IActionResult ListExpenseById(int id)
        {
            ReadExpenseDto readDto = _expenseService.ListExpenseById(id);
            if (readDto != null)
                return Ok(readDto);

            return NotFound();
        }

        [HttpGet("Search/{description}")]
        public IActionResult ListExpenseByDescription(string description)
        {
            List<ReadExpenseDto> readListDto = _expenseService.ListExpenseByDescription(description);
            if (readListDto != null)
                return Ok(readListDto);

            return NotFound();
        }

        [HttpGet("Search/{year}/{month}")]
        public IActionResult ListExpenseByMonthOfYear(int year, int month)
        {
            List<ReadExpenseDto> readListDto = _expenseService.ListExpenseByMonthOfYear(year, month);
            if (readListDto != null)
                return Ok(readListDto);

            return NotFound();
        }

        [HttpPut("{id}")]
        public IActionResult UpdateExpense(int id, [FromBody] UpdateExpenseDto updatedExpenseDto)
        {
            Result result = _expenseService.UpdateExpense(id, updatedExpenseDto);
            if (result.IsFailed)
                return BadRequest(result);

            return NoContent();

        }

        [HttpDelete("{id}")]
        public IActionResult DeleteExpense(int id)
        {
            Result result = _expenseService.DeleteExpense(id);
            if (result.IsFailed)
                return NotFound();

            return NoContent();
        }

    }
}