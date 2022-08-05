using FinancialAppAPI.Models;
using Microsoft.AspNetCore.Mvc;
using FinancialAppAPI.Data;
using System.Globalization;

namespace FinancialAppAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class IncomeController : ControllerBase
    {
        private FinancialContext _context;

        public IncomeController(FinancialContext context)
        {
            _context = context;
        }

        [HttpPost]
        public IActionResult AddIncome([FromBody] Income income)
        {
            IQueryable<Income> searchSameName = SameName(income);

            //If there isn't an income with same in the same month, it'll be allowed to be add
            if (searchSameName.Count() == 0)
            {
                _context.Incomes.Add(income);
                _context.SaveChanges();
                return CreatedAtAction(nameof(ListIncomeById), new { Id = income.incomeId }, income);
            }

            return BadRequest($"Income with same name already exists in {CultureInfo.GetCultureInfo("en-Us").DateTimeFormat.GetMonthName(income.incomeDate.Month)}");

        }

        [HttpGet]
        public IEnumerable<Income> ListIncomes()
        {
            return _context.Incomes;
        }

        [HttpGet("{id}")]
        public IActionResult ListIncomeById(int id)
        {
            Income income = _context.Incomes.FirstOrDefault(income => income.incomeId == id);
            if (income != null)
            {
                return Ok(income);
            }
            return NotFound();
        }

        [HttpPut("{id}")]
        public IActionResult UpdateIncome(int id, [FromBody] Income updatedIncome)
        {
            Income income = _context.Incomes.FirstOrDefault(income => income.incomeId == id);

            IQueryable<Income> searchSameName = SameName(updatedIncome);

            if (income != null)
            {
                //If there isn't other income with same name except for the one being changed, it'll be allowed to be updated
                if (searchSameName.Count() == 0 || searchSameName.Select(inc => inc.incomeName).Contains(income.incomeName))
                {
                    income.incomeName = updatedIncome.incomeName;
                    income.incomeAmount = updatedIncome.incomeAmount;
                    income.incomeDate = updatedIncome.incomeDate;
                    _context.SaveChanges();
                    return NoContent();
                }
                else
                {
                    return BadRequest($"Income with same name already exists in {CultureInfo.GetCultureInfo("en-Us").DateTimeFormat.GetMonthName(updatedIncome.incomeDate.Month)}");
                }
            }
            return NotFound();
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteIncome(int id)
        {
            Income income = _context.Incomes.FirstOrDefault(income => income.incomeId == id);
            if (income != null)
            {
                _context.Remove(income);
                _context.SaveChanges();
                return NoContent();
            }
            return NotFound();
        }

        //Verifies if an income with same name exists in the same month 
        private IQueryable<Income> SameName(Income income)
        {
            return from inc in _context.Incomes
                   where inc.incomeName == income.incomeName && inc.incomeDate.Month == income.incomeDate.Month
                   select inc;
        }


    }
}
