using AutoMapper;
using Facillita.API.Data.Dtos.Expense;
using Facillita.API.Models;

namespace Facillita.API.Profiles
{
    public class ExpenseProfile : Profile
    {
        public ExpenseProfile()
        {
            CreateMap<CreateExpenseDto, Expense>();
            CreateMap<Expense, ReadExpenseDto>();
            CreateMap<UpdateExpenseDto, Expense>();
        }
    }
}
