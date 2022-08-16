using FinancialAppAPI.Data;
using FinancialAppAPI.Data.Dtos.Expense;
using FinancialAppAPI.Interfaces.Repositories;
using FinancialAppAPI.Models;

namespace FinancialAppAPI.Repository
{
    public class ExpenseRepository : IExpenseRepository
    {
        private readonly FinancialContext _context;

        public ExpenseRepository(FinancialContext context)
        {
            _context = context;
        }

        public IQueryable<Expense> SearchSameName(CreateExpenseDto expenseDto)
        {
            return from exp in _context.Expenses
                   where exp.ExpenseName == expenseDto.ExpenseName && exp.ExpenseDate.Month == expenseDto.ExpenseDate.Month
                   select exp;
        }

        public Expense GetExpenseById(int id)
        {
            return _context.Expenses.FirstOrDefault(expense => expense.ExpenseId == id);
        }

        public IQueryable<Expense> SearchSameDescription(string description)
        {
            return _context.Expenses
                .Where(expense => expense.ExpenseName.ToLower()
                .Contains(description.ToLower()));
        }

        public IQueryable<Expense> SearchMonthOfYear(int year, int month)
        {
            return _context.Expenses
                .Where(expense => expense.ExpenseDate.Year == year && expense.ExpenseDate.Month == month);
        }

        public IQueryable<Expense> SearchSameName(UpdateExpenseDto updatedExpenseDto)
        {
            return from exp in _context.Expenses
                   where exp.ExpenseName == updatedExpenseDto.ExpenseName && exp.ExpenseDate.Month == updatedExpenseDto.ExpenseDate.Month
                   select exp;
        }

    }
}
