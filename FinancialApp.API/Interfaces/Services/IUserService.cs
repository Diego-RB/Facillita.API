using Facillita.API.Data.Dtos.User;
using FluentResults;

namespace Facillita.API.Interfaces.Services
{
    public interface IUserService
    {
        public UserDto AddUser(UserDto user);
        public Result DeleteUser(string uid);
    }
}
