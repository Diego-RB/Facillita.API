using Facillita.API.Models.FinancialSummary;

namespace Facillita.API.Interfaces.Services
{
    public interface IFinancialService
    {       
        public JsonField MonthSummary(int year, int month);
    }
}
