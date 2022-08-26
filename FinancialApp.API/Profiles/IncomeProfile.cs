using AutoMapper;
using FinancialApp.API.Data.Dtos.Income;
using FinancialApp.API.Models;

namespace FinancialApp.API.Profiles
{
    public class IncomeProfile : Profile
    {
        public IncomeProfile()
        {
            CreateMap<CreateIncomeDto, Income>();
            CreateMap<Income, ReadIncomeDto>();
            CreateMap<UpdateIncomeDto, Income>();
        }
    }
}
