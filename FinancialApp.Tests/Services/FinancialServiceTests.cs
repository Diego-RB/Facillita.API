using FinancialApp.API.Data;
using FinancialApp.API.Interfaces.Repositories;
using FinancialApp.API.Models.FinancialSummary;
using FinancialApp.API.Repository;
using FinancialApp.API.Services;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;

namespace FinancialApp.Tests.Services
{
    public class FinancialServiceTests
    {
        private readonly IFinancialRepository _financialRepository;
        public FinancialContext GetDatabaseContext()
        {
            var options = new DbContextOptionsBuilder<FinancialContext>()
            .UseInMemoryDatabase(databaseName: "EmptyExpenses")
            .Options;
            var context = new FinancialContext(options);
            return context;
        }
        public FinancialServiceTests()
        {
            _financialRepository = new FinancialRepository(GetDatabaseContext());
        }

        [Fact]
        public void MonthSummary_ReturnJsonField()
        {
            //Arrange
            var year = 2022;
            var month = 8;
            var service = new FinancialService(_financialRepository);

            //Act
            var result = service.MonthSummary(year, month);

            //Assert
            result.Should().BeOfType(typeof(JsonField));
        }
    }
}
