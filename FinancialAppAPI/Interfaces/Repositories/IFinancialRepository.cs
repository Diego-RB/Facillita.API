using FinancialAppAPI.Models.FinancialSummary;

namespace FinancialAppAPI.Interfaces.Repositories
{
    public interface IFinancialRepository
    {
        public double TotalExpense(int year, int month);
        public double TotalIncome(int year, int month);
        public List<ExpenseByCategory> CalculateExpensesByCategory(int year, int month);

    }
}
