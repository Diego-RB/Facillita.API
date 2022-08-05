using FinancialAppAPI.Models;
using FinancialAppAPI.Data;
using Microsoft.AspNetCore.Mvc;
using System.Globalization;

namespace FinancialAppAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ExpenseController : ControllerBase
    {
        private FinancialContext _context;

        public ExpenseController(FinancialContext context)
        {
            _context = context;
        }

        [HttpPost]
        public IActionResult AddExpense([FromBody] Expense expense)
        {
            IQueryable<Expense> searchSameName = SameName(expense);

            //If there isn't an expense with same in the same month, it'll be allowed to be add
            if (searchSameName.Count() == 0)
            {
                _context.Expenses.Add(expense);
                _context.SaveChanges();
                return CreatedAtAction(nameof(ListExpensesById), new { Id = expense.expenseId }, expense);
            }

            return BadRequest($"Expense with same name already exists in {CultureInfo.GetCultureInfo("en-Us").DateTimeFormat.GetMonthName(expense.expenseDate.Month)}");

        }



        [HttpGet]
        public IEnumerable<Expense> ListExpenses()
        {
            return _context.Expenses;
        }

        [HttpGet("{id}")]
        public IActionResult ListExpensesById(int id)
        {
            Expense expense = _context.Expenses.FirstOrDefault(expense => expense.expenseId == id);
            if (expense != null)
            {
                return Ok(expense);
            }
            return NotFound();
        }

        [HttpPut("{id}")]
        public IActionResult UpdateExpense(int id, [FromBody] Expense updatedExpense)
        {
            Expense expense = _context.Expenses.FirstOrDefault(expense => expense.expenseId == id);

            IQueryable<Expense> searchSameName = SameName(updatedExpense);

            if (expense != null)
            {
                //If there isn't other expense with same name except for the one being changed, it'll be allowed to be updated
                if (searchSameName.Count() == 0 || searchSameName.Select(exp => exp.expenseName).Contains(expense.expenseName))
                {
                    expense.expenseName = updatedExpense.expenseName;
                    expense.expenseAmount = updatedExpense.expenseAmount;
                    expense.expenseDate = updatedExpense.expenseDate;
                    _context.SaveChanges();
                    return NoContent();
                }
                else
                {
                    return BadRequest($"Expense with same name already exists in {CultureInfo.GetCultureInfo("en-Us").DateTimeFormat.GetMonthName(updatedExpense.expenseDate.Month)}");
                }

            }
            return NotFound();
        }


        [HttpDelete("{id}")]
        public IActionResult DeleteExpense(int id)
        {
            Expense expense = _context.Expenses.FirstOrDefault(expense => expense.expenseId == id);
            if (expense != null)
            {
                _context.Remove(expense);
                _context.SaveChanges();
                return NoContent();
            }
            return NotFound();
        }

        //Verifies if an expense with same name exists in the same month 
        private IQueryable<Expense> SameName(Expense expense)
        {
            return from exp in _context.Expenses
                   where exp.expenseName == expense.expenseName && exp.expenseDate.Month == expense.expenseDate.Month
                   select exp;
        }

    }
}
