using FinancialAppAPI.Data;
using FinancialAppAPI.Data.Dtos.Expense;
using FinancialAppAPI.Models;

namespace FinancialAppAPI.Interfaces.Repositories
{
    public interface IExpenseRepository
    {
        public IQueryable<Expense> SearchSameName(CreateExpenseDto expenseDto);
        public Expense GetExpenseById(int id);
        public IQueryable<Expense> SearchSameDescription(string description);
        public IQueryable<Expense> SearchMonthOfYear(int year, int month);
        public IQueryable<Expense> SearchSameName(UpdateExpenseDto updatedExpenseDto);

    }
}
