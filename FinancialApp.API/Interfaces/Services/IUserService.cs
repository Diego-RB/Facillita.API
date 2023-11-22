using Facillita.API.Models;
using FluentResults;

namespace Facillita.API.Interfaces.Services
{
    public interface IUserService
    {
        public void AddIncome(User user);
        public Result DeleteIncome(int id);
    }
}
