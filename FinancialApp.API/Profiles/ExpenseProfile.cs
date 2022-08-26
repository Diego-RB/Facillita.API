using AutoMapper;
using FinancialApp.API.Data.Dtos.Expense;
using FinancialApp.API.Models;

namespace FinancialApp.API.Profiles
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
