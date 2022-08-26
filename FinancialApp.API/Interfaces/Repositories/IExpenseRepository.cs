using FinancialApp.API.Data;
using FinancialApp.API.Data.Dtos.Expense;
using FinancialApp.API.Models;

namespace FinancialApp.API.Interfaces.Repositories
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
