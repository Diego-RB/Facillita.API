using AutoMapper;
using FinancialApp.API.Data.Dtos.Expense;
using FinancialApp.API.Data.Dtos.Income;
using FinancialApp.API.Models;

namespace FinancialApp.Tests.Configuration
{
    public class AutoMapperConfiguration
    {
        public static IMapper GetConfiguration()
        {
            var autoMapperConfig = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<CreateIncomeDto, Income>().ReverseMap();
                cfg.CreateMap<Income, ReadIncomeDto>().ReverseMap();
                cfg.CreateMap<UpdateIncomeDto, Income>().ReverseMap();
                cfg.CreateMap<CreateExpenseDto, Expense>().ReverseMap();
                cfg.CreateMap<Expense, ReadExpenseDto>().ReverseMap();
                cfg.CreateMap<UpdateExpenseDto, Expense>().ReverseMap();

            });
            return autoMapperConfig.CreateMapper();
        }
    }
}
