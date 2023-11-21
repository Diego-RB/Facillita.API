using Facillita.API.Data;
using Facillita.API.Interfaces.Repositories;
using Facillita.API.Interfaces.Services;
using Facillita.API.Models.FinancialSummary;
using Facillita.API.Repository;
using Newtonsoft.Json;

namespace Facillita.API.Services
{
    public class FinancialService : IFinancialService
    {
        private readonly IFinancialRepository _repository;

        public FinancialService(IFinancialRepository repository)
        {
            _repository = repository;
        }     

        public JsonField MonthSummary(int year, int month)
        {
            var totalIncome = _repository.TotalIncome(year, month);
            var totalExpense = _repository.TotalExpense(year, month);              
            var balance = totalIncome - totalExpense;
            var listExpenseByCategory = _repository.CalculateExpensesByCategory(year, month);
                        
            return new JsonField {
                TotalIncome = totalIncome,
                TotalExpense = totalExpense,
                Balance = balance,
                List = listExpenseByCategory
            };
        }
        
    }

 

   
}

