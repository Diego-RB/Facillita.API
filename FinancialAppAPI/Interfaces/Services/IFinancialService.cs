using FinancialAppAPI.Models.FinancialSummary;

namespace FinancialAppAPI.Interfaces.Services
{
    public interface IFinancialService
    {       
        public JsonField MonthSummary(int year, int month);
    }
}
