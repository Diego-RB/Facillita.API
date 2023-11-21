using Facillita.API.Data.Dtos.Income;
using Facillita.API.Models;

namespace Facillita.API.Interfaces.Repositories
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
