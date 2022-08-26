using FinancialApp.API.Models.FinancialSummary;

namespace FinancialApp.API.Interfaces.Services
{
    public interface IFinancialService
    {       
        public JsonField MonthSummary(int year, int month);
    }
}
