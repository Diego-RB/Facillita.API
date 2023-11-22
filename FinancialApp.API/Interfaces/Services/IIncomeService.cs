using Facillita.API.Data.Dtos.Income;
using FluentResults;

namespace Facillita.API.Interfaces.Services
{
    public interface IIncomeService
    {
        public ReadIncomeDto AddIncome(CreateIncomeDto incomeDto);
        public List<ReadIncomeDto> ListIncomes(string userUId);
        public ReadIncomeDto ListIncomeById(int id);
        public List<ReadIncomeDto> ListIncomeByDescription(string userUId, string description);
        public List<ReadIncomeDto> ListIncomeByMonthOfYear(string userUId, int year, int month);
        public Result UpdateIncome(int id, UpdateIncomeDto updatedIncomeDto);
        public Result DeleteIncome(int id);

    }
}
