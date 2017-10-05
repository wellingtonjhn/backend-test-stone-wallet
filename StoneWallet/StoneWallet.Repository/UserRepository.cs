using MongoDB.Driver;
using StoneWallet.Domain.Contracts;
using StoneWallet.Domain.Models.Entities;
using System.Threading.Tasks;

namespace StoneWallet.Repository
{
    public class UserRepository : IUserRepository
    {
        private const string UsersCollection = "users";
        private readonly IMongoCollection<User> _users;

        public UserRepository(MongoDbContext dbContext)
        {
            _users = dbContext.Database.GetCollection<User>(UsersCollection);
        }

        public async Task CreateUser(User user)
        {
            await _users.InsertOneAsync(user);
        }

        public async Task<bool> ExistsUser(string email)
        {
            var user = await _users.Find(u => u.Email.Equals(email)).FirstOrDefaultAsync();
            return user != null;
        }

        public async Task<User> Authenticate(string email, string password)
        {
            var user = await _users.Find(u => u.Email.Equals(email) && u.Password.Equals(password))
                .FirstOrDefaultAsync();

            return user;
        }
    }
}