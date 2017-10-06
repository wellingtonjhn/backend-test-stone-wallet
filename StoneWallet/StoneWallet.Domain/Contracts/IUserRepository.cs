using StoneWallet.Domain.Models.Entities;
using System.Threading.Tasks;

namespace StoneWallet.Domain.Contracts
{
    public interface IUserRepository
    {
        Task<User> Get(string id);
        Task CreateUser(User user);
        Task<bool> ExistsUser(string email);
        Task<User> Authenticate(string email, string password);
    }
}