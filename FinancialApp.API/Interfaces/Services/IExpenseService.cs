using Facillita.API.Data.Dtos.Expense;
using FluentResults;

namespace Facillita.API.Interfaces.Services
{
    public interface IExpenseService
    {
        public ReadExpenseDto AddExpense(CreateExpenseDto expenseDto);
        public List<ReadExpenseDto> ListExpenses(string userUId);
        public ReadExpenseDto ListExpenseById(int id);
        public List<ReadExpenseDto> ListExpenseByDescription(string userUId, string description);
        public List<ReadExpenseDto> ListExpenseByMonthOfYear(string userUId, int year, int month);
        public Result UpdateExpense(int id, UpdateExpenseDto updatedExpenseDto);
        public Result DeleteExpense(int id);

    }
}
