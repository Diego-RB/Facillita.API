using AutoMapper;
using Facillita.Users.Data.Dtos;
using Facillita.Users.Models;
using Microsoft.AspNetCore.Identity;

namespace Facillita.Users.Profiles
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
