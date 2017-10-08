using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using System;
using System.Threading.Tasks;

namespace StoneWallet.Api.Middlewares
{
    /// <summary>
    /// Middleware responsável por verificar se o usuário está autenticado na aplicação
    /// </summary>
    public class AuthenticateSchemeMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly string _scheme;

        /// <summary>
        /// Cria um novo middleware para verificar se o usuário está autenticado na aplicação
        /// </summary>
        /// <param name="next">Próximo middleware do pipeline</param>
        /// <param name="scheme">Schema de autenticação</param>
        public AuthenticateSchemeMiddleware(RequestDelegate next, string scheme)
        {
            _next = next;
            _scheme = scheme ?? throw new ArgumentNullException(nameof(scheme));
        }

        /// <summary>
        /// Executa o middleware de forma assíncrona
        /// </summary>
        /// <param name="httpContext">Contexto HTTP do request</param>
        /// <returns></returns>
        public async Task Invoke(HttpContext httpContext)
        {
            var result = await httpContext.AuthenticateAsync(_scheme);

            if (result.Succeeded)
            {
                httpContext.User = result.Principal;
            }

            await _next(httpContext);
        }
    }

    /// <summary>
    /// Classe de extensão para validação de autenticação do usuário
    /// </summary>
    public static class AuthenticateMiddlewareExtensions
    {
        /// <summary>
        /// Usa o middleware para verificar se o usuário está autenticado na aplicação
        /// </summary>
        /// <param name="app">Builder de pipeline da aplicação</param>
        /// <param name="scheme">Schema de autenticação</param>
        /// <returns>Builder de pipeline da aplicação</returns>
        public static IApplicationBuilder UseAuthenticationScheme(this IApplicationBuilder app, string scheme)
        {
            return app.UseMiddleware<AuthenticateSchemeMiddleware>(scheme);
        }
    }
}