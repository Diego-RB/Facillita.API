using AutoMapper;
using Facillita.API.Data.Dtos.User;
using Facillita.API.Models;

namespace Facillita.API.Profiles
{
    public class UserProfile : Profile
    {
        public UserProfile()
        {
            CreateMap<UserDto, User>();
            CreateMap<User, UserDto>();
        }
    }
}
