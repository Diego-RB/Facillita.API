using Facillita.API.Models;

namespace Facillita.API.Interfaces.Repositories
{
    public interface IUserRepository
    {
        public User GetUserByUID(string uid);

    }
}
