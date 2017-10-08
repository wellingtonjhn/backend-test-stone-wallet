using Microsoft.Extensions.Configuration;
using Npgsql;

namespace StoneWallet.Repository
{
    public abstract class Repository
    {
        protected string ConnectionString { get; }

        protected Repository(IConfiguration configuration)
        {
            ConnectionString = configuration.GetConnectionString("StoneWallet");
        }

        protected NpgsqlConnection GetConnection()
        {
            var connection = new NpgsqlConnection(ConnectionString)
            {
                UserCertificateValidationCallback = delegate { return true; }
            };
            return connection;
        }
    }
}