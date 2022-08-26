using FinancialApp.API.Data;
using FinancialApp.API.Interfaces.Repositories;
using FinancialApp.API.Interfaces.Services;
using FinancialApp.API.Models.FinancialSummary;
using FinancialApp.API.Repository;
using Newtonsoft.Json;

namespace FinancialApp.API.Services
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

