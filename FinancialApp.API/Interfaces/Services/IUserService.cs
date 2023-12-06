using Facillita.API.Data.Dtos.User;
using FluentResults;

namespace Facillita.API.Interfaces.Services
{
    public interface IUserService
    {
        public UserDto AddUser(UserDto user);
        public Result DeleteUser(string uid);
        public bool hasUser(string userName);
    }
}
