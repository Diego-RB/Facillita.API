using FakeItEasy;
using FinancialAppAPI.Controllers;
using FinancialAppAPI.Interfaces.Services;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;

namespace FinancialApp.Tests.Controllers
{
    public class FinancialControllerTests
    {
        private readonly IFinancialService _financialService;

        public FinancialControllerTests()
        {
            _financialService = A.Fake<IFinancialService>();
        }

        [Fact]
        public void MonthSummary_ReturnOk()
        {
            //Arrange
            var year = 2022;
            var month = 8;
            var controller = new FinancialController(_financialService);

            //Act
            var result = controller.MonthSummary(year, month);

            //Assert
            result.Should().BeOfType(typeof(OkObjectResult));
        }

    }
}
