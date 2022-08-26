using AutoMapper;
using FinancialApp.Tests.Configuration;
using FinancialApp.API.Data;
using FinancialApp.API.Data.Dtos.Expense;
using FinancialApp.API.Interfaces.Repositories;
using FinancialApp.API.Models;
using FinancialApp.API.Models.Enum;
using FinancialApp.API.Repository;
using FinancialApp.API.Services;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;


namespace FinancialApp.Tests.Services
{
    public class ExpenseServicesTests
    {
        private readonly IMapper _mapper;
        private readonly IExpenseRepository _expenseRepository;

        public FinancialContext GetDatabaseContext()
        {
            var options = new DbContextOptionsBuilder<FinancialContext>()
            .UseInMemoryDatabase(databaseName: "Expenses")
            .Options;
            var context = new FinancialContext(options);
            context.Database.EnsureCreated();

            if (context.Expenses.Count() <= 0)
            {
                context.Expenses.AddRange(
                    new Expense()
                    {
                        ExpenseId = 1,
                        ExpenseName = "Market",
                        ExpenseAmount = 400,
                        ExpenseDate = new DateTime(2022, 8, 4),
                        Category = (ExpenseCategory) 1
                    },
                    new Expense()
                    {
                        ExpenseId = 2,
                        ExpenseName = "Gas",
                        ExpenseAmount = 200,
                        ExpenseDate = new DateTime(2022, 8, 5),
                        Category = (ExpenseCategory) 4
                    }
                ); ;
                context.SaveChanges();
            }
            return context;
        }

        public FinancialContext GetEmptyDatabaseContext()
        {
            var options = new DbContextOptionsBuilder<FinancialContext>()
            .UseInMemoryDatabase(databaseName: "EmptyExpenses")
            .Options;
            var context = new FinancialContext(options);
            return context;
        }

        public ExpenseServicesTests()
        {
            _mapper = AutoMapperConfiguration.GetConfiguration();
            _expenseRepository = new ExpenseRepository(GetDatabaseContext());
        }

        [Fact]
        public void AddExpense_IfAddedSuccessfullyReturnReadExpenseDto()
        {
            //Arrange
            var expenseDto = new CreateExpenseDto
            {
                ExpenseName = "New Chair",
                ExpenseAmount = 350,
                ExpenseDate = new DateTime(2022, 8, 10),
                Category = (ExpenseCategory) 3
            };
            var service = new ExpenseService(GetDatabaseContext(), _mapper, _expenseRepository);

            //Act
            var result = service.AddExpense(expenseDto);

            //Result

            result.Should().BeOfType(typeof(ReadExpenseDto));
        }

        [Fact]
        public void AddExpense_ReturnNullIfHasSameDescriptionAtTheSameMonth()
        {
            //Arrange
            var expenseDto = new CreateExpenseDto
            {
                ExpenseName = "Gas",
                ExpenseAmount = 350,
                ExpenseDate = new DateTime(2022, 8, 10),
                Category = (ExpenseCategory) 4
            };
            var service = new ExpenseService(GetDatabaseContext(), _mapper, _expenseRepository);

            //Act
            var result = service.AddExpense(expenseDto);

            //Result

            result.Should().BeNull();
        }

        [Fact]
        public void ListExpenses_OnSuccessReturnListOfExpenses()
        {
            //Arrange            
            var service = new ExpenseService(GetDatabaseContext(), _mapper, _expenseRepository);

            //Act
            var result = service.ListExpenses();

            //Result

            result.Should().BeOfType(typeof(List<ReadExpenseDto>));
        }

        [Fact]
        public void ListExpenses_IfEmptyReturnNull()
        {
            //Arrange          
            var service = new ExpenseService(GetEmptyDatabaseContext(), _mapper, _expenseRepository);

            //Act

            var result = service.ListExpenses();

            //Result

            result.DefaultIfEmpty(null);

        }

        [Fact]
        public void ListExpenseById_OnSuccessReturnReadDto()
        {
            //Arrange
            var expenseId = 2;
            var service = new ExpenseService(GetDatabaseContext(), _mapper, _expenseRepository);

            //Act
            var result = service.ListExpenseById(expenseId);

            //Result

            result.Should().BeOfType(typeof(ReadExpenseDto));
        }

        [Fact]
        public void ListExpenseById_ReturnsNullIfIdNotFound()
        {
            //Arrange
            var expenseId = 10;
            var service = new ExpenseService(GetDatabaseContext(), _mapper, _expenseRepository);

            //Act
            var result = service.ListExpenseById(expenseId);

            //Result

            result.Should().BeNull();
        }

        [Fact]
        public void ListExpenseByDescription_OnSuccessReturnListOfExpensesWithRequiredDescription()
        {
            //Arrange
            var expenseDescription = "Gas";
            var service = new ExpenseService(GetDatabaseContext(), _mapper, _expenseRepository);

            //Act
            var result = service.ListExpenseByDescription(expenseDescription);

            //Result

            result.Should().BeOfType(typeof(List<ReadExpenseDto>));
        }

        [Fact]
        public void ListExpenseByDescription_ReturnsNullIfNoExpenseWithDescriptionIsFound()
        {
            //Arrange
            var expenseDescription = "Energy";
            var service = new ExpenseService(GetDatabaseContext(), _mapper, _expenseRepository);

            //Act
            var result = service.ListExpenseByDescription(expenseDescription);

            //Result

            result.DefaultIfEmpty(null);
        }

        [Fact]
        public void ListExpenseByMonthOfYear_OnSuccessReturnListOfExpensesOfGivenMonthOfAYear()
        {
            //Arrange
            var year = 2022;
            var month = 8;
            var service = new ExpenseService(GetDatabaseContext(), _mapper, _expenseRepository);

            //Act
            var result = service.ListExpenseByMonthOfYear(year, month);

            //Result

            result.Should().BeOfType(typeof(List<ReadExpenseDto>));
        }

        [Fact]
        public void ListExpenseByMonthOfYear_ReturnsNullIfNoExpenseOfGivenMonthOfAYearIsFound()
        {
            //Arrange
            var year = 2022;
            var month = 4;
            var service = new ExpenseService(GetDatabaseContext(), _mapper, _expenseRepository);

            //Act
            var result = service.ListExpenseByMonthOfYear(year, month);

            //Result

            result.DefaultIfEmpty(null);
        }

        [Fact]
        public void UpdateExpense_OnSuccessReturnOk()
        {
            //Arrange
            var expenseId = 2;
            var updatedExpenseDto = new UpdateExpenseDto
            {
                ExpenseName = "New Notebook",
                ExpenseAmount = 5000,
                ExpenseDate = new DateTime(2022, 8, 4),
                Category = 0

            };
            var service = new ExpenseService(GetDatabaseContext(), _mapper, _expenseRepository);

            //Act
            var result = service.UpdateExpense(expenseId, updatedExpenseDto);

            //Result

            result.IsSuccess.Should().BeTrue();
        }

        [Fact]
        public void UpdateExpense_ReturnsFailIfIdNotFound()
        {
            //Arrange
            var expenseId = 10;
            var updatedExpenseDto = new UpdateExpenseDto
            {
                ExpenseName = "New Notebook",
                ExpenseAmount = 5000,
                ExpenseDate = new DateTime(2022, 8, 4),
                Category = 0
            };
            var service = new ExpenseService(GetDatabaseContext(), _mapper, _expenseRepository);

            //Act
            var result = service.UpdateExpense(expenseId, updatedExpenseDto);

            //Result

            result.IsFailed.Should().BeTrue();
        }

        [Fact]
        public void UpdateExpense_ReturnsFailIfUpdatedToAnExpenseWithSameDescriptionInSameMonth()
        {
            //Arrange
            var expenseId = 2;
            var updatedExpenseDto = new UpdateExpenseDto
            {
                ExpenseName = "Market",
                ExpenseAmount = 500,
                ExpenseDate = new DateTime(2022, 8, 4),
                Category = (ExpenseCategory) 1
            };
            var service = new ExpenseService(GetDatabaseContext(), _mapper, _expenseRepository);

            //Act
            var result = service.UpdateExpense(expenseId, updatedExpenseDto);

            //Result

            result.IsFailed.Should().BeTrue();
        }

        [Fact]
        public void DeleteExpense_OnSuccessReturnsOk()
        {
            //Arrange
            var expenseId = 2;
            var service = new ExpenseService(GetDatabaseContext(), _mapper, _expenseRepository);

            //Act
            var result = service.DeleteExpense(expenseId);

            //Result
            result.IsSuccess.Should().BeTrue();
        }

        [Fact]
        public void DeleteExpense_ReturnsFailIfIdNotFound()
        {
            //Arrange
            var expenseId = 10;
            var service = new ExpenseService(GetDatabaseContext(), _mapper, _expenseRepository);

            //Act
            var result = service.DeleteExpense(expenseId);

            //Result

            result.IsFailed.Should().BeTrue();
        }
    }
}