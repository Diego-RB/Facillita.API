using FinancialApp.API.Data;
using FinancialApp.API.Data.Dtos.Income;
using FinancialApp.API.Interfaces.Repositories;
using FinancialApp.API.Models;

namespace FinancialApp.API.Repository
{
    public class IncomeRepository : IIncomeRepository
    {
        private readonly FinancialContext _context;
        public IncomeRepository(FinancialContext context)
        {
            _context = context;
        }

        public IQueryable<Income> SearchSameName(CreateIncomeDto incomeDto)
        {
            return from inc in _context.Incomes
                   where inc.IncomeName == incomeDto.IncomeName && inc.IncomeDate.Month == incomeDto.IncomeDate.Month
                   select inc;
        }


        public Income GetIncomeById(int id)
        {
            return _context.Incomes.FirstOrDefault(income => income.IncomeId == id);
        }

        public IQueryable<Income> SearchSameDescription(string description)
        {
            return _context.Incomes
                .Where(income => income.IncomeName.ToLower()
                .Contains(description.ToLower()));
        }

        public IQueryable<Income> SearchMonthOfYear(int year, int month)
        {
            return _context.Incomes
                .Where(income => income.IncomeDate.Year == year && income.IncomeDate.Month == month);
        }

        public IQueryable<Income> SearchSameName(UpdateIncomeDto updatedIncomeDto)
        {
            return from inc in _context.Incomes
                   where inc.IncomeName == updatedIncomeDto.IncomeName && inc.IncomeDate.Month == updatedIncomeDto.IncomeDate.Month
                   select inc;
        }
    }
}
