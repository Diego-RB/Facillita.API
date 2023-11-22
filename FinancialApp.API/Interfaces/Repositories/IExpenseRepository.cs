using Facillita.API.Data.Dtos.Expense;
using Facillita.API.Models;

namespace Facillita.API.Interfaces.Repositories
{
    public interface IExpenseRepository
    {
        public IQueryable<Expense> SearchSameName(CreateExpenseDto expenseDto);
        public Expense GetExpenseById(int id);
        public IQueryable<Expense> SearchSameDescription(string userUId, string description);
        public IQueryable<Expense> SearchMonthOfYear(string userUId, int year, int month);
        public IQueryable<Expense> SearchSameName(UpdateExpenseDto updatedExpenseDto);

    }
}
