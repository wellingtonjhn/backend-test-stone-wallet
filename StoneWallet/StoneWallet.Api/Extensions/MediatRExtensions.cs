using FluentValidation;
using MediatR;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using StoneWallet.Application.Core.Middlewares;
using System.Reflection;

namespace StoneWallet.Api.Extensions
{
    public static class MediatRExtensions
    {
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
