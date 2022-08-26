using FinancialApp.API.Models.FinancialSummary;

namespace FinancialApp.API.Interfaces.Repositories
{
    public interface IFinancialRepository
    {
        public double TotalExpense(int year, int month);
        public double TotalIncome(int year, int month);
        public List<ExpenseByCategory> CalculateExpensesByCategory(int year, int month);

    }
}
