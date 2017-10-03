using StoneWallet.Domain.Contracts;
using StoneWallet.Domain.Models.Entities;
using System.Threading.Tasks;

namespace StoneWallet.Repository
{
    public class UserRepository : IUserRepository
    {
        public async Task CreateUser(User user)
        {
            await Task.FromResult(user);
        }

        public async Task<User> Authenticate(string username, string password)
        {
            User user = null;

            return await Task.FromResult(user);
        }
    }
}