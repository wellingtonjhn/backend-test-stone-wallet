using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using StoneWallet.Application.Core.Security;
using StoneWallet.Domain.Contracts;

namespace StoneWallet.Api.Extensions
{
    /// <summary>
    ///  Representa extensões para configurar o ASP.NET MVC
    /// </summary>
    public static class MvcExtensions
    {
        /// <summary>
        /// Registra o ASP.NET MVC no injetor de dependências
        /// </summary>
        /// <param name="services">Instância do injetor de dependências</param>
        public static void AddMvcWithCustomConfiguration(this IServiceCollection services)
        {
            services.AddAuthorization(options =>
            {
                options.AddPolicy("Bearer", new AuthorizationPolicyBuilder()
                    .AddAuthenticationSchemes(JwtBearerDefaults.AuthenticationScheme)
                    .RequireAuthenticatedUser()
                    .Build());
            });

            services.AddMvc(config =>
            {
                var policy = new AuthorizationPolicyBuilder()
                    .RequireAuthenticatedUser()
                    .Build();

                config.Filters.Add(new AuthorizeFilter(policy));

            }).AddJsonOptions(options =>
            {
                options.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
            });

            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddScoped<IAuthenticatedUser, AuthenticatedUser>();
        }
    }
}