using Microsoft.Extensions.Configuration;
using MongoDB.Driver;

namespace StoneWallet.Repository
{
    public class MongoDbContext
    {
        public IMongoDatabase Database;

        public MongoDbContext(IConfiguration configuration)
        {
            var connectionString = configuration["DatabaseSettings:MongoDbConnectionString"];

            var client = new MongoClient(connectionString);
            Database = client.GetDatabase("stone-wallet");
        }
    }
}