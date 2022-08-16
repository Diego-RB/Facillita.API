using AutoMapper;
using FinancialAppAPI.Data.Dtos.Expense;
using FinancialAppAPI.Models;

namespace FinancialAppAPI.Profiles
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
