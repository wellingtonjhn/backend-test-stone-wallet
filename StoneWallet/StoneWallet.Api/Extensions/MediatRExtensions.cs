using FluentValidation;
using MediatR;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using StoneWallet.Application.Core.Middlewares;
using System.Reflection;

namespace StoneWallet.Api.Extensions
{
    /// <summary>
    /// Representa extensões para configurar o MediatR
    /// </summary>
    public static class MediatRExtensions
    {
        /// <summary>
        /// Registra o MediatR no injetor de dependências
        /// </summary>
        /// <param name="services">Instância do injetor de dependências</param>
        /// <param name="configuration">Instância das configurações da aplicação</param>
        public static void AddMediatR(this IServiceCollection services, IConfiguration configuration)
        {
            var assemblyName = configuration.GetValue<string>("CommandSettings:Assembly");

            AssemblyScanner
                .FindValidatorsInAssembly(Assembly.Load(new AssemblyName(assemblyName)))
                .ForEach(result => services.AddScoped(result.InterfaceType, result.ValidatorType));

            services.AddScoped(typeof(IPipelineBehavior<,>), typeof(FluentValidationMiddleware<,>));
            services.AddMediatR();
        }
    }
}
