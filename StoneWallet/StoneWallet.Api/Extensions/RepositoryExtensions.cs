using Microsoft.Extensions.DependencyInjection;
using StoneWallet.Domain.Contracts;
using StoneWallet.Repository;

namespace StoneWallet.Api.Extensions
{
    /// <summary>
    /// Representa extensões para configurar os repositórios da aplicação
    /// </summary>
    public static class RepositoryExtensions
    {
        /// <summary>
        /// Registra os Repositórios da aplicação no injetor de dependências
        /// </summary>
        /// <param name="services">Instância do injetor de dependências</param>
        public static void AddRepository(this IServiceCollection services)
        {
            services.AddScoped<IUsersRepository, UsersRepository>();
            services.AddScoped<IWalletRepository, WalletRepository>();
            services.AddScoped<ICreditCardRepository, CreditCardRepository>();
        }
    }
}
