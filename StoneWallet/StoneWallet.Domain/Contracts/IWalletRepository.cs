using StoneWallet.Domain.Models.Entities;
using System.Threading.Tasks;

namespace StoneWallet.Domain.Contracts
{
    public interface IWalletRepository
    {
        Task CreateWallet(Wallet wallet);
        Task<Wallet> GetWalletByUser(string userId);
    }
}