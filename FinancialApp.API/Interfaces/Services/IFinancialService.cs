using Facillita.API.Data.Dtos.Financial;
using Facillita.API.Models.FinancialSummary;

namespace Facillita.API.Interfaces.Services
{
    public interface IFinancialService
    {
        public JsonField MonthSummary(string userUId, int year, int month);
        public List<ExtractDto> GetExtract(string userUID);
    }
}
