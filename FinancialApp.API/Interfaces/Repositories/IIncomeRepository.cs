using FinancialApp.API.Data.Dtos.Income;
using FinancialApp.API.Models;

namespace FinancialApp.API.Interfaces.Repositories
{
    public interface IIncomeRepository
    {
        public IQueryable<Income> SearchSameName(CreateIncomeDto incomeDto);
        public IQueryable<Income> SearchSameName(UpdateIncomeDto updatedIncomeDto);
        public Income GetIncomeById(int id);
        public IQueryable<Income> SearchSameDescription(string description);
        public IQueryable<Income> SearchMonthOfYear(int year, int month);

    }
}
