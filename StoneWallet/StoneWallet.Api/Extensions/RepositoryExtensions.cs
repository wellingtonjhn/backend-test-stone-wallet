using Microsoft.Extensions.DependencyInjection;
using StoneWallet.Domain.Contracts;
using StoneWallet.Repository;

namespace StoneWallet.Api.Extensions
{
    public static class RepositoryExtensions
    {
        public static void AddRepository(this IServiceCollection services)
        {
            services.AddScoped<MongoDbContext>();
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IWalletRepository, WalletRepository>();
        }
    }
}
