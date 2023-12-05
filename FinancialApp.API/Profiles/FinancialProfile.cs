using AutoMapper;
using Facillita.API.Data.Dtos.Financial;
using Facillita.API.Models;

namespace Facillita.API.Profiles
{
    public class FinancialProfile : Profile
    {
        public FinancialProfile()
        {
            CreateMap<ExtractDto, Extract>();
            CreateMap<Extract, ExtractDto>();
        }
    }
}
