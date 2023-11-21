using Facillita.API.Models.FinancialSummary;

namespace Facillita.API.Interfaces.Repositories
{
    public interface IFinancialRepository
    {
        public double TotalExpense(int year, int month);
        public double TotalIncome(int year, int month);
        public List<ExpenseByCategory> CalculateExpensesByCategory(int year, int month);

    }
}
