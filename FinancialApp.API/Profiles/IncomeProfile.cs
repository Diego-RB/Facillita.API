using AutoMapper;
using Facillita.API.Data.Dtos.Income;
using Facillita.API.Models;

namespace Facillita.API.Profiles
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
