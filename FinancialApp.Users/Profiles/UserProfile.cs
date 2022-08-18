using AutoMapper;
using FinancialApp.Users.Data.Dtos;
using FinancialApp.Users.Models;
using Microsoft.AspNetCore.Identity;

namespace FinancialApp.Users.Profiles
{
    public class UserProfile : Profile
    {
        public UserProfile()
        {
            CreateMap<CreateUserDto, User>();
            CreateMap<User, IdentityUser<int>>();
        }
    }
}
