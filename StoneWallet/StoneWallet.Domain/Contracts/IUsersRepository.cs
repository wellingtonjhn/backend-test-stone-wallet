using StoneWallet.Domain.Models.Entities;
using System;
using System.Threading.Tasks;

namespace StoneWallet.Domain.Contracts
{
    public interface IUsersRepository
    {
        Task<User> Get(Guid id);
        Task CreateUser(User user);
        Task<bool> ExistsUser(string email);
        Task<User> Authenticate(string email, string password);
    }
}