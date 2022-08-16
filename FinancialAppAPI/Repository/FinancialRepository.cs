using FinancialAppAPI.Data;
using FinancialAppAPI.Interfaces.Repositories;
using FinancialAppAPI.Models.FinancialSummary;

namespace FinancialAppAPI.Repository
{
    public class FinancialRepository : IFinancialRepository
    {
        private readonly FinancialContext _context;

        public FinancialRepository(FinancialContext context)
        {
            _context = context;
        }

        public double TotalIncome(int year, int month)
        {
            return _context.Incomes
                   .Where(income => income.IncomeDate.Year == year && income.IncomeDate.Month == month)
                   .Select(income => income.IncomeAmount).Sum();
        }

        public double TotalExpense(int year, int month)
        {
            return _context.Expenses
                    .Where(expense => expense.ExpenseDate.Year == year && expense.ExpenseDate.Month == month)
                    .Select(expense => expense.ExpenseAmount).Sum();
        }

        public List<ExpenseByCategory> CalculateExpensesByCategory(int year, int month)
        {
            List<ExpenseByCategory> repositoryList = new List<ExpenseByCategory>();

            for (int i = 0; i <= 7; i++)
            {
                var expenseByCategory = _context.Expenses.Where(expense => expense.ExpenseDate.Year == year
                && expense.ExpenseDate.Month == month
                && (int)expense.Category == i);
                var amountByCategory = expenseByCategory.Select(expense => expense.ExpenseAmount).Sum();
                if (amountByCategory > 0)
                {
                    repositoryList.Add(new ExpenseByCategory
                    {
                        CategoryId = i,
                        Total = amountByCategory
                    });
                }
            }
            return repositoryList;

        }
    }
}
