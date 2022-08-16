using FinancialAppAPI.Data.Dtos.Income;
using FluentResults;

namespace FinancialAppAPI.Interfaces.Services
{
    public interface IIncomeService
    {
        public ReadIncomeDto AddIncome(CreateIncomeDto incomeDto);
        public List<ReadIncomeDto> ListIncomes();
        public ReadIncomeDto ListIncomeById(int id);
        public List<ReadIncomeDto> ListIncomeByDescription(string description);
        public List<ReadIncomeDto> ListIncomeByMonthOfYear(int year, int month);
        public Result UpdateIncome(int id, UpdateIncomeDto updatedIncomeDto);
        public Result DeleteIncome(int id);

    }
}
