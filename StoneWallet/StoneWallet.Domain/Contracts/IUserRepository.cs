using System.Threading.Tasks;
using StoneWallet.Domain.Models.Entities;

namespace StoneWallet.Domain.Contracts
{
    public interface IUserRepository
    {
        Task CreateUser(User user);
        Task<User> Authenticate(string username, string password);
    }
}