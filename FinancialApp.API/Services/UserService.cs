using AutoMapper;
using Facillita.API.Data;
using Facillita.API.Data.Dtos.User;
using Facillita.API.Interfaces.Services;
using Facillita.API.Models;
using FluentResults;

namespace Facillita.API.Services
{
    public class UserService : IUserService
    {
        private FinancialContext _context;
        private readonly IMapper _mapper;

        public UserService(FinancialContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public UserDto AddUser(UserDto userDto)
        {
            User user = _mapper.Map<User>(userDto);

            _context.User.Add(user);
            _context.SaveChanges();
            return _mapper.Map<UserDto>(user);
        }

        public bool hasUser(string userName)
        {
            return _context.User.Any(x => userName.Contains(x.Username));
        }

        public Result DeleteUser(string uid)
        {
            User user = _context.User.Where(x => x.UID == uid).FirstOrDefault();
            if (user != null)
            {
                _context.Remove(user);
                _context.SaveChanges();
                return Result.Ok();
            }
            return Result.Fail("Income not found");
        }


    }
}
