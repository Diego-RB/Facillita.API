using AutoMapper;
using FinancialApp.Tests.Configuration;
using FinancialAppAPI.Data;
using FinancialAppAPI.Data.Dtos.Income;
using FinancialAppAPI.Interfaces.Repositories;
using FinancialAppAPI.Models;
using FinancialAppAPI.Repository;
using FinancialAppAPI.Services;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;

namespace FinancialApp.Tests.Services
{
    public class IncomeServicesTests
    {
        private readonly IMapper _mapper;
        private readonly IIncomeRepository _incomeRepository;



        public FinancialContext GetDatabaseContext()
        {
            var options = new DbContextOptionsBuilder<FinancialContext>()
            .UseInMemoryDatabase(databaseName: "Incomes")
            .Options;
            var context = new FinancialContext(options);
            context.Database.EnsureCreated();

            if (context.Incomes.Count() <= 0)
            {
                context.Incomes.AddRange(
                    new Income()
                    {
                        IncomeId = 1,
                        IncomeName = "Job Payment",
                        IncomeAmount = 2000,
                        IncomeDate = new DateTime(2022, 8, 5)
                    },
                    new Income()
                    {
                        IncomeId = 2,
                        IncomeName = "Dividends",
                        IncomeAmount = 3000,
                        IncomeDate = new DateTime(2022, 8, 3)
                    },
                    new Income()
                    {
                        IncomeId = 3,
                        IncomeName = "Business Profit",
                        IncomeAmount = 10000,
                        IncomeDate = new DateTime(2022, 8, 1)
                    }
                );
                context.SaveChanges();
            }
            return context;
        }

        public FinancialContext GetEmptyDatabaseContext()
        {
            var options = new DbContextOptionsBuilder<FinancialContext>()
            .UseInMemoryDatabase(databaseName: "EmptyIncomes")
            .Options;
            var context = new FinancialContext(options);
            return context;
        }

        public IncomeServicesTests()
        {
            _mapper = AutoMapperConfiguration.GetConfiguration();
            _incomeRepository = new IncomeRepository(GetDatabaseContext());
        }

        [Fact]
        public void AddIncome_IfAddedSuccessfullyReturnReadIncomeDto()
        {
            //Arrange
            var incomeDto = new CreateIncomeDto
            {
                IncomeName = "Salary",
                IncomeAmount = 1000,
                IncomeDate = new DateTime(2022, 7, 25)
            };
            var service = new IncomeService(GetDatabaseContext(), _mapper, _incomeRepository);

            //Act
            var result = service.AddIncome(incomeDto);

            //Result

            result.Should().BeOfType(typeof(ReadIncomeDto));
        }

        [Fact]
        public void AddIncome_ReturnNullIfHasSameDescriptionAtTheSameMonth()
        {
            //Arrange
            var incomeDto = new CreateIncomeDto
            {
                IncomeName = "Job Payment",
                IncomeAmount = 1000,
                IncomeDate = new DateTime(2022, 8, 3)
            };
            var service = new IncomeService(GetDatabaseContext(), _mapper, _incomeRepository);

            //Act
            var result = service.AddIncome(incomeDto);

            //Result

            result.Should().BeNull();
        }

        [Fact]
        public void ListIncomes_OnSuccessReturnListOfIncomes()
        {
            //Arrange            
            var service = new IncomeService(GetDatabaseContext(), _mapper, _incomeRepository);

            //Act
            var result = service.ListIncomes();

            //Result

            result.Should().BeOfType(typeof(List<ReadIncomeDto>));
        }

        [Fact]
        public void ListIncomes_IfEmptyReturnNull()
        {
            //Arrange          
            var service = new IncomeService(GetEmptyDatabaseContext(), _mapper, _incomeRepository);

            //Act

            var result = service.ListIncomes();

            //Result

            result.DefaultIfEmpty(null);

        }

        [Fact]
        public void ListIncomeById_OnSuccessReturnReadDto()
        {
            //Arrange
            var incomeId = 2;
            var service = new IncomeService(GetDatabaseContext(), _mapper, _incomeRepository);

            //Act
            var result = service.ListIncomeById(incomeId);

            //Result

            result.Should().BeOfType(typeof(ReadIncomeDto));
        }

        [Fact]
        public void ListIncomeById_ReturnsNullIfIdNotFound()
        {
            //Arrange
            var incomeId = 10;
            var service = new IncomeService(GetDatabaseContext(), _mapper, _incomeRepository);

            //Act
            var result = service.ListIncomeById(incomeId);

            //Result

            result.Should().BeNull();
        }

        [Fact]
        public void ListIncomeByDescription_OnSuccessReturnListOfIncomesWithRequiredDescription()
        {
            //Arrange
            var incomeDescription = "Job";
            var service = new IncomeService(GetDatabaseContext(), _mapper, _incomeRepository);

            //Act
            var result = service.ListIncomeByDescription(incomeDescription);

            //Result

            result.Should().BeOfType(typeof(List<ReadIncomeDto>));
        }

        [Fact]
        public void ListIncomeByDescription_ReturnsNullIfNoIncomeWithDescriptionIsFound()
        {
            //Arrange
            var incomeDescription = "Capital";
            var service = new IncomeService(GetDatabaseContext(), _mapper, _incomeRepository);

            //Act
            var result = service.ListIncomeByDescription(incomeDescription);

            //Result

            result.DefaultIfEmpty(null);
        }

        [Fact]
        public void ListIncomeByMonthOfYear_OnSuccessReturnListOfIncomesOfGivenMonthOfAYear()
        {
            //Arrange
            var year = 2022;
            var month = 8;
            var service = new IncomeService(GetDatabaseContext(), _mapper, _incomeRepository);

            //Act
            var result = service.ListIncomeByMonthOfYear(year, month);

            //Result

            result.Should().BeOfType(typeof(List<ReadIncomeDto>));
        }

        [Fact]
        public void ListIncomeByMonthOfYear_ReturnsNullIfNoIncomeOfGivenMonthOfAYearIsFound()
        {
            //Arrange
            var year = 2022;
            var month = 4;
            var service = new IncomeService(GetDatabaseContext(), _mapper, _incomeRepository);

            //Act
            var result = service.ListIncomeByMonthOfYear(year, month);

            //Result

            result.DefaultIfEmpty(null);
        }

        [Fact]
        public void UpdateIncome_OnSuccessReturnOk()
        {
            //Arrange
            var incomeId = 2;
            var updatedIncomeDto = new UpdateIncomeDto
            {
                IncomeName = "Rent",
                IncomeAmount = 3000,
                IncomeDate = new DateTime(2022, 8, 4)
            };
            var service = new IncomeService(GetDatabaseContext(), _mapper, _incomeRepository);

            //Act
            var result = service.UpdateIncome(incomeId, updatedIncomeDto);

            //Result
            result.IsSuccess.Should().BeTrue();
        }

        [Fact]
        public void UpdateIncome_ReturnsFailIfIdNotFound()
        {
            //Arrange
            var incomeId = 10;
            var updatedIncomeDto = new UpdateIncomeDto
            {
                IncomeName = "Rent",
                IncomeAmount = 3000,
                IncomeDate = new DateTime(2022, 8, 4)
            };
            var service = new IncomeService(GetDatabaseContext(), _mapper, _incomeRepository);

            //Act
            var result = service.UpdateIncome(incomeId, updatedIncomeDto);

            //Result

            result.IsFailed.Should().BeTrue();
        }

        [Fact]
        public void UpdateIncome_ReturnsFailIfUpdatedToAnIncomeWithSameDescriptionInSameMonth()
        {
            //Arrange
            var incomeId = 2;
            var updatedIncomeDto = new UpdateIncomeDto
            {
                IncomeName = "Job Payment",
                IncomeAmount = 3000,
                IncomeDate = new DateTime(2022, 8, 4)
            };
            var service = new IncomeService(GetDatabaseContext(), _mapper, _incomeRepository);

            //Act
            var result = service.UpdateIncome(incomeId, updatedIncomeDto);

            //Result

            result.IsFailed.Should().BeTrue();
        }

        [Fact]
        public void DeleteIncome_OnSuccessReturnsOk()
        {
            //Arrange
            var incomeId = 3;
            var service = new IncomeService(GetDatabaseContext(), _mapper, _incomeRepository);

            //Act
            var result = service.DeleteIncome(incomeId);

            //Result
            result.IsSuccess.Should().BeTrue();
        }

        [Fact]
        public void DeleteIncome_ReturnsFailIfIdNotFound()
        {
            //Arrange
            var incomeId = 10;           
            var service = new IncomeService(GetDatabaseContext(), _mapper, _incomeRepository);

            //Act
            var result = service.DeleteIncome(incomeId);

            //Result

            result.IsFailed.Should().BeTrue();
        }
    }
}
