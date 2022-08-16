using FakeItEasy;
using FinancialAppAPI.Controllers;
using FinancialAppAPI.Data.Dtos.Expense;
using FinancialAppAPI.Interfaces.Services;
using FluentAssertions;
using FluentResults;
using Microsoft.AspNetCore.Mvc;


namespace FinancialApp.Tests.Controllers
{
    public class ExpenseControllerTests
    {
        private readonly IExpenseService _expenseService;

        public ExpenseControllerTests()
        {
            _expenseService = A.Fake<IExpenseService>();
        }

        [Fact]
        public void AddExpense_ReturnCreatedAtActionResult()
        {
            //Arrrange
            var expenseDto = A.Fake<CreateExpenseDto>();
            var controller = new ExpenseController(_expenseService);

            //Act
            var result = controller.AddExpense(expenseDto);

            //Assert
            result.Should().BeOfType(typeof(CreatedAtActionResult));
        }

        [Fact]
        public void AddExpense_ReturnBadRequest()
        {
            //Arrange
            var expenseDto = A.Fake<CreateExpenseDto>();
            A.CallTo(() => _expenseService.AddExpense(expenseDto)).Returns(null);
            var controller = new ExpenseController(_expenseService);

            //Act
            var result = controller.AddExpense(expenseDto);

            //Assert
            result.Should().BeOfType(typeof(BadRequestObjectResult));
        }

        [Fact]
        public void ListExpenses_ReturnOK()
        {
            //Arrange
            var controller = new ExpenseController(_expenseService);

            //Act
            var result = controller.ListExpenses();

            //Assert
            result.Should().BeOfType(typeof(OkObjectResult));
        }

        [Fact]
        public void ListExpenses_ReturnsNotFound()
        {
            //Arrange            
            A.CallTo(() => _expenseService.ListExpenses()).Returns(null);
            var controller = new ExpenseController(_expenseService);

            //Act
            var result = controller.ListExpenses();

            //Assert
            result.Should().BeOfType(typeof(NotFoundResult));
        }


        [Fact]
        public void ListExpenseById_ReturnsOk()
        {
            //Arrange
            var expenseId = 1;
            var controller = new ExpenseController(_expenseService);

            //Act
            var result = controller.ListExpenseById(expenseId);


            //Assert
            result.Should().BeOfType(typeof(OkObjectResult));
        }

        [Fact]
        public void ListExpenseById_ReturnsNotFound()
        {
            //Arrange
            var expenseId = 10;
            A.CallTo(() => _expenseService.ListExpenseById(expenseId)).Returns(null);
            var controller = new ExpenseController(_expenseService);

            //Act
            var result = controller.ListExpenseById(expenseId);


            //Assert
            result.Should().BeOfType(typeof(NotFoundResult));
        }


        [Fact]
        public void ListExpensesByDescription_ReturnsOk()
        {
            //Arrange
            var expenseDescription = "Market";
            var controller = new ExpenseController(_expenseService);


            //Act
            var result = controller.ListExpenseByDescription(expenseDescription);


            //Assert
            result.Should().BeOfType(typeof(OkObjectResult));
        }

        [Fact]
        public void ListExpensesByDescription_ReturnsNotFound()
        {
            //Arrange
            var expenseDescription = "Market";
            A.CallTo(() => _expenseService.ListExpenseByDescription(expenseDescription)).Returns(null);
            var controller = new ExpenseController(_expenseService);


            //Act
            var result = controller.ListExpenseByDescription(expenseDescription);


            //Assert
            result.Should().BeOfType(typeof(NotFoundResult));
        }

        [Fact]
        public void ListExpensesByMonthOfYear_ReturnsOk()
        {
            //Arrange
            var year = 2022;
            var month = 8;
            var controller = new ExpenseController(_expenseService);


            //Act
            var result = controller.ListExpenseByMonthOfYear(year, month);


            //Assert
            result.Should().BeOfType(typeof(OkObjectResult));
        }

        [Fact]
        public void ListExpensesByMonthOfYear_ReturnsNotFound()
        {
            //Arrange
            var year = 2022;
            var month = 8;
            A.CallTo(() => _expenseService.ListExpenseByMonthOfYear(year, month)).Returns(null);
            var controller = new ExpenseController(_expenseService);


            //Act
            var result = controller.ListExpenseByMonthOfYear(year, month);


            //Assert
            result.Should().BeOfType(typeof(NotFoundResult));
        }

        [Fact]
        public void UpdateExpense_OnSuccess_ReturnsNoContent()
        {
            //Arrange
            var expenseId = 1;
            var updatedExpenseDto = A.Fake<UpdateExpenseDto>();
            var controller = new ExpenseController(_expenseService);

            //Act
            var result = controller.UpdateExpense(expenseId, updatedExpenseDto);

            //Assert
            result.Should().BeOfType(typeof(NoContentResult));
        }

        [Fact]
        public void UpdateExpense_BadRequest()
        {
            //Arrange
            var expenseId = 1;
            var updatedExpenseDto = A.Fake<UpdateExpenseDto>();
            A.CallTo(() => _expenseService.UpdateExpense(expenseId, updatedExpenseDto)).Returns(Result.Fail("any error"));
            var controller = new ExpenseController(_expenseService);

            //Act
            var result = controller.UpdateExpense(expenseId, updatedExpenseDto);

            //Assert
            result.Should().BeOfType(typeof(BadRequestObjectResult));
        }

        [Fact]
        public void DeleteExpense_OnSuccess_ReturnNoContent()
        {

            //Arrange
            var expenseId = 1;
            var controller = new ExpenseController(_expenseService);

            //Act
            var result = controller.DeleteExpense(expenseId);

            //Assert
            result.Should().BeOfType(typeof(NoContentResult));
        }

        [Fact]
        public void DeleteExpense_ReturnNotFound()
        {
            //Arrange
            var expenseId = 10;
            A.CallTo(() => _expenseService.DeleteExpense(expenseId)).Returns(Result.Fail("Income not found"));
            var controller = new ExpenseController(_expenseService);

            //Act
            var result = controller.DeleteExpense(expenseId);

            //Assert
            result.Should().BeOfType(typeof(NotFoundResult));
        }
    }
}