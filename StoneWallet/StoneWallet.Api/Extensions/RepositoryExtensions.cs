using Microsoft.Extensions.DependencyInjection;
using StoneWallet.Domain.Contracts;
using StoneWallet.Repository;

namespace StoneWallet.Api.Extensions
{
    public static class RepositoryExtensions
    {
        public static void AddRepository(this IServiceCollection services)
        {
            services.AddScoped<IUsersRepository, UsersesRepository>();
            services.AddScoped<IWalletRepository, WalletRepository>();
            services.AddScoped<ICreditCardRepository, CreditCardRepository>();
        }
    }
}
