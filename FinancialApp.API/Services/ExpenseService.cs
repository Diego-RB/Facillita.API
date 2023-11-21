using AutoMapper;
using Facillita.API.Data;
using Facillita.API.Data.Dtos.Expense;
using Facillita.API.Interfaces.Repositories;
using Facillita.API.Interfaces.Services;
using Facillita.API.Models;
using FluentResults;
using System.Globalization;

namespace Facillita.API.Services
{
    public class ExpenseService : IExpenseService
    {
        private FinancialContext _context;
        private IMapper _mapper;
        private IExpenseRepository _repository;

        public ExpenseService(FinancialContext context, IMapper mapper, IExpenseRepository repository)
        {
            _context = context;
            _mapper = mapper;
            _repository = repository;   
        }
        public ReadExpenseDto AddExpense(CreateExpenseDto expenseDto)
        {
            Expense expense = _mapper.Map<Expense>(expenseDto);

            //Verifies if an expense with same name exists in the same month 
            var searchSameName = _repository.SearchSameName(expenseDto);

            ////If there isn't an expense with same in the same month, it'll be allowed to be add
            if (searchSameName.Count() == 0)
            {
                _context.Expenses.Add(expense);
                _context.SaveChanges();
                return _mapper.Map<ReadExpenseDto>(expense);
            }
            return null;
        }

        public List<ReadExpenseDto> ListExpenses()
        {
            List<Expense> Expenses = _context.Expenses.ToList();
            if (Expenses != null)
            {
                List<ReadExpenseDto> readDto = _mapper.Map<List<ReadExpenseDto>>(Expenses);
                return readDto;
            }
            return null;
        }

        public ReadExpenseDto ListExpenseById(int id)
        {
            Expense expense = _repository.GetExpenseById(id);
            if (expense != null)
            {
                ReadExpenseDto readDto = _mapper.Map<ReadExpenseDto>(expense);
                return readDto;
            }
            return null;
        }

        public List<ReadExpenseDto> ListExpenseByDescription(string description)
        {
            List<ReadExpenseDto> ExpensesByDescription = new List<ReadExpenseDto>();
            var queryDescription = _repository.SearchSameDescription(description);
            if (queryDescription != null)
            {
                foreach (var expense in queryDescription)
                {
                    ReadExpenseDto expenseDto = _mapper.Map<ReadExpenseDto>(expense);
                    ExpensesByDescription.Add(expenseDto);
                }
                return ExpensesByDescription;
            }
            return null;
        }

        public List<ReadExpenseDto> ListExpenseByMonthOfYear(int year, int month)
        {
            List<ReadExpenseDto> ExpensesByMonthOfYear = new List<ReadExpenseDto>();
            var queryYearAndMonth = _repository.SearchMonthOfYear(year, month);
            if (queryYearAndMonth != null)
            {
                foreach (var expense in queryYearAndMonth)
                {
                    ReadExpenseDto expenseDto = _mapper.Map<ReadExpenseDto>(expense);
                    ExpensesByMonthOfYear.Add(expenseDto);
                }
                return ExpensesByMonthOfYear;
            }
            return null;
        }

        public Result UpdateExpense(int id, UpdateExpenseDto updatedExpenseDto)
        {
            Expense expense = _repository.GetExpenseById(id);

            //Verifies if an expense with same name exists in the same month 

            var searchSameName = _repository.SearchSameName(updatedExpenseDto);

            if (expense != null)
            {
                //If there isn't other expense with same name except for the one being changed, it'll be allowed to be updated
                if (searchSameName.Count() == 0 || searchSameName.Select(inc => inc.ExpenseName).Contains(expense.ExpenseName))
                {
                    _mapper.Map<ReadExpenseDto>(expense);
                    _mapper.Map(updatedExpenseDto, expense);
                    _context.SaveChanges();
                    return Result.Ok();
                }
                return Result.Fail($"Expense with same name already exists in {CultureInfo.GetCultureInfo("en-Us").DateTimeFormat.GetMonthName(updatedExpenseDto.ExpenseDate.Month)}");
            }
            return Result.Fail("Expense not found");
        }

        public Result DeleteExpense(int id)
        {
            Expense expense = _repository.GetExpenseById(id);
            if (expense != null)
            {
                _context.Remove(expense);
                _context.SaveChanges();
                return Result.Ok();
            }
            return Result.Fail("Expense not found");
        }
    }
}
