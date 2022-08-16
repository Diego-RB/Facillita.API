using AutoMapper;
using FinancialAppAPI.Data.Dtos.Income;
using FinancialAppAPI.Models;

namespace FinancialAppAPI.Profiles
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
