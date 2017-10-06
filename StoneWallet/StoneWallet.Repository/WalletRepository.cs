using MongoDB.Bson;
using MongoDB.Driver;
using StoneWallet.Domain.Contracts;
using StoneWallet.Domain.Models.Entities;
using System.Threading.Tasks;

namespace StoneWallet.Repository
{
    public class WalletRepository : IWalletRepository
    {
        private const string WalletCollection = "wallets";
        private readonly IMongoCollection<Wallet> _wallets;

        public WalletRepository(MongoDbContext dbContext)
        {
            _wallets = dbContext.Database.GetCollection<Wallet>(WalletCollection);
        }

        public async Task CreateWallet(Wallet wallet)
        {
            await _wallets.InsertOneAsync(wallet);
        }

        public async Task<Wallet> GetWalletByUser(string userId)
        {
            return await _wallets
                .Find(u => u.User.Equals(new ObjectId(userId)))
                .FirstOrDefaultAsync();
        }
    }
}