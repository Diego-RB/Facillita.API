using Facillita.API.Interfaces.Repositories;
using Facillita.API.Interfaces.Services;
using Facillita.API.Models;
using Facillita.API.Models.FinancialSummary;

namespace Facillita.API.Services
{
    public class FinancialService : IFinancialService
    {
        private readonly IFinancialRepository _repository;

        public FinancialService(IFinancialRepository repository)
        {
            _repository = repository;
        }

        public JsonField MonthSummary(string userUId, int year, int month)
        {
            var totalIncome = _repository.TotalIncome(userUId, year, month);
            var totalExpense = _repository.TotalExpense(userUId, year, month);
            var balance = totalIncome - totalExpense;
            var listExpenseByCategory = _repository.CalculateExpensesByCategory(userUId, year, month);

            return new JsonField
            {
                TotalIncome = totalIncome,
                TotalExpense = totalExpense,
                Balance = balance,
                List = listExpenseByCategory
            };
        }

        public List<Extract> GetExtract(string userUID)
        {
            return _repository.GetExtrac(userUID);
        }

    }




}

