using Facillita.API.Data;
using Facillita.API.Interfaces.Repositories;
using Facillita.API.Models;

namespace Facillita.API.Repository
{
    public class UserRepository : IUserRepository
    {
        private readonly FinancialContext _context;
        public UserRepository(FinancialContext context)
        {
            _context = context;
        }

        public User GetUserByUID(string uid)
        {
            return _context.User.FirstOrDefault(user => user.UID == uid);
        }
    }
}
