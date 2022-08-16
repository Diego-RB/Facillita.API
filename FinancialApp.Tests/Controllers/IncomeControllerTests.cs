using FakeItEasy;
using FinancialAppAPI.Controllers;
using FinancialAppAPI.Data.Dtos.Income;
using FinancialAppAPI.Interfaces.Services;
using FluentAssertions;
using FluentResults;
using Microsoft.AspNetCore.Mvc;


namespace FinancialApp.Tests.Controllers
{
    public class IncomeControllerTests
    {
        private readonly IIncomeService _incomeService;

        public IncomeControllerTests()
        {
            _incomeService = A.Fake<IIncomeService>(); 
        }

        [Fact]
        public void AddIncome_ReturnCreatedAtActionResult()
        {
            //Arrrange
            var incomeDto = A.Fake<CreateIncomeDto>();
            var controller = new IncomeController(_incomeService);

            //Act
            var result = controller.AddIncome(incomeDto);

            //Assert
            result.Should().BeOfType(typeof(CreatedAtActionResult));
        }

        [Fact]
        public void AddIncome_ReturnBadRequest()
        {
            //Arrange
            var incomeDto = A.Fake<CreateIncomeDto>();
            A.CallTo(() => _incomeService.AddIncome(incomeDto)).Returns(null);
            var controller = new IncomeController(_incomeService);

            //Act
            var result = controller.AddIncome(incomeDto);

            //Assert
            result.Should().BeOfType(typeof(BadRequestObjectResult));
        }

        [Fact]
        public void ListIncomes_ReturnOK()
        {
            //Arrange
            var controller = new IncomeController(_incomeService);

            //Act
            var result = controller.ListIncomes();

            //Assert
            result.Should().BeOfType(typeof(OkObjectResult));
        }

        [Fact]
        public void ListIncomes_ReturnsNotFound()
        {
            //Arrange            
            A.CallTo(() => _incomeService.ListIncomes()).Returns(null);
            var controller = new IncomeController(_incomeService);

            //Act
            var result = controller.ListIncomes();

            //Assert
            result.Should().BeOfType(typeof(NotFoundResult));
        }


        [Fact]
        public void ListIncomesById_ReturnsOk()
        {
            //Arrange
            var incomeId = 1;
            var controller = new IncomeController(_incomeService);

            //Act
            var result = controller.ListIncomeById(incomeId);


            //Assert
            result.Should().BeOfType(typeof(OkObjectResult));
        }

        [Fact]
        public void ListIncomesById_ReturnsNotFound()
        {
            //Arrange
            var incomeId = 10;
            A.CallTo(() => _incomeService.ListIncomeById(incomeId)).Returns(null);
            var controller = new IncomeController(_incomeService);

            //Act
            var result = controller.ListIncomeById(incomeId);


            //Assert
            result.Should().BeOfType(typeof(NotFoundResult));
        }


        [Fact]
        public void ListIncomesByDescription_ReturnsOk()
        {
            //Arrange
            var incomeDescription = "Salary";
            var controller = new IncomeController(_incomeService);


            //Act
            var result = controller.ListIncomeByDescription(incomeDescription);


            //Assert
            result.Should().BeOfType(typeof(OkObjectResult));
        }

        [Fact]
        public void ListIncomesByDescription_ReturnsNotFound()
        {
            //Arrange
            var incomeDescription = "Salary";
            A.CallTo(() => _incomeService.ListIncomeByDescription(incomeDescription)).Returns(null);
            var controller = new IncomeController(_incomeService);


            //Act
            var result = controller.ListIncomeByDescription(incomeDescription);


            //Assert
            result.Should().BeOfType(typeof(NotFoundResult));
        }

        [Fact]
        public void ListIncomesByMonthOfYear_ReturnsOk()
        {
            //Arrange
            var year = 2022;
            var month = 8;
            var controller = new IncomeController(_incomeService);


            //Act
            var result = controller.ListIncomeByMonthOfYear(year, month);


            //Assert
            result.Should().BeOfType(typeof(OkObjectResult));
        }

        [Fact]
        public void ListIncomesByMonthOfYear_ReturnsNotFound()
        {
            //Arrange
            var year = 2022;
            var month = 8;
            A.CallTo(() => _incomeService.ListIncomeByMonthOfYear(year, month)).Returns(null);
            var controller = new IncomeController(_incomeService);


            //Act
            var result = controller.ListIncomeByMonthOfYear(year, month);


            //Assert
            result.Should().BeOfType(typeof(NotFoundResult));
        }

        [Fact]
        public void UpdateIncome_OnSuccess_ReturnsNoContent()
        {
            //Arrange
            var incomeId = 1;
            var updatedIncomeDto = A.Fake<UpdateIncomeDto>();
            var controller = new IncomeController(_incomeService);

            //Act
            var result = controller.UpdateIncome(incomeId, updatedIncomeDto);

            //Assert
            result.Should().BeOfType(typeof(NoContentResult));
        }

        [Fact]
        public void UpdateIncome_BadRequest()
        {
            //Arrange
            var incomeId = 1;
            var updatedIncomeDto = A.Fake<UpdateIncomeDto>();
            A.CallTo(() => _incomeService.UpdateIncome(incomeId, updatedIncomeDto)).Returns(Result.Fail("any error"));
            var controller = new IncomeController(_incomeService);

            //Act
            var result = controller.UpdateIncome(incomeId, updatedIncomeDto);

            //Assert
            result.Should().BeOfType(typeof(BadRequestObjectResult));
        }

        [Fact]
        public void DeleteIncome_OnSuccess_ReturnNoContent()
        {

            //Arrange
            var incomeId = 1;
            var controller = new IncomeController(_incomeService);

            //Act
            var result = controller.DeleteIncome(incomeId);

            //Assert
            result.Should().BeOfType(typeof(NoContentResult));
        }

        [Fact]
        public void DeleteIncome_ReturnNotFound()
        {
            //Arrange
            var incomeId = 10;
            A.CallTo(() => _incomeService.DeleteIncome(incomeId)).Returns(Result.Fail("Income not found"));
            var controller = new IncomeController(_incomeService);

            //Act
            var result = controller.DeleteIncome(incomeId);

            //Assert
            result.Should().BeOfType(typeof(NotFoundResult));
        }
    }
}