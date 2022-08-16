using FinancialAppAPI.Data;
using FinancialAppAPI.Interfaces.Repositories;
using FinancialAppAPI.Models.FinancialSummary;
using FinancialAppAPI.Repository;
using FinancialAppAPI.Services;
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
