using Facillita.API.Data;
using Facillita.API.Interfaces.Repositories;
using Facillita.API.Interfaces.Services;
using Facillita.API.Models;
using FluentResults;

namespace Facillita.API.Services
{
    public class UserService : IUserService
    {
        private FinancialContext _context;
        private IIncomeRepository _repository;

        public UserService(FinancialContext context, IIncomeRepository repository)
        {
            _context = context;
            _repository = repository;
        }

        public void AddIncome(User user)
        {
            _context.User.Add(user);
            _context.SaveChanges();
        }

        public Result DeleteIncome(int id)
        {
            Income income = _repository.GetIncomeById(id);
            if (income != null)
            {
                _context.Remove(income);
                _context.SaveChanges();
                return Result.Ok();
            }
            return Result.Fail("Income not found");
        }


    }
}
