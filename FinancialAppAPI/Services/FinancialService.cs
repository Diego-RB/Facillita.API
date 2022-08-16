using FinancialAppAPI.Data;
using FinancialAppAPI.Interfaces.Repositories;
using FinancialAppAPI.Interfaces.Services;
using FinancialAppAPI.Models.FinancialSummary;
using FinancialAppAPI.Repository;
using Newtonsoft.Json;

namespace FinancialAppAPI.Services
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

