using Microsoft.Extensions.Configuration;
using Npgsql;

namespace StoneWallet.Repository
{
    /// <summary>
    /// Representa um Repositório
    /// </summary>
    public abstract class Repository
    {
        protected string ConnectionString { get; }

        /// <summary>
        /// Cria um novo repositório
        /// </summary>
        /// <param name="configuration">Configuração da aplicação</param>
        protected Repository(IConfiguration configuration)
        {
            ConnectionString = configuration.GetConnectionString("StoneWallet");
        }

        /// <summary>
        /// Obtém uma conexão com a base de dados
        /// </summary>
        /// <returns></returns>
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