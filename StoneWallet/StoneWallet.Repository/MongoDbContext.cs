using MongoDB.Driver;

namespace StoneWallet.Repository
{
    public class MongoDbContext
    {
        public IMongoDatabase Database;

        public MongoDbContext()
        {
            var connectionString = "mongodb://stone:stone@ds040017.mlab.com:40017/stone-wallet";
            var client = new MongoClient(connectionString);
            Database = client.GetDatabase("stone-wallet");
        }
    }
}