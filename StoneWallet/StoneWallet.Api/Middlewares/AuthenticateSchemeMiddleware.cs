using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using System;
using System.Threading.Tasks;

namespace StoneWallet.Api.Middlewares
{
    /// <summary>
    /// Middleware respons�vel por verificar se o usu�rio est� autenticado na aplica��o
    /// </summary>
    public class AuthenticateSchemeMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly string _scheme;

        /// <summary>
        /// Cria um novo middleware para verificar se o usu�rio est� autenticado na aplica��o
        /// </summary>
        /// <param name="next">Pr�ximo middleware do pipeline</param>
        /// <param name="scheme">Schema de autentica��o</param>
        public AuthenticateSchemeMiddleware(RequestDelegate next, string scheme)
        {
            _next = next;
            _scheme = scheme ?? throw new ArgumentNullException(nameof(scheme));
        }

        /// <summary>
        /// Executa o middleware de forma ass�ncrona
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
    /// Classe de extens�o para valida��o de autentica��o do usu�rio
    /// </summary>
    public static class AuthenticateMiddlewareExtensions
    {
        /// <summary>
        /// Usa o middleware para verificar se o usu�rio est� autenticado na aplica��o
        /// </summary>
        /// <param name="app">Builder de pipeline da aplica��o</param>
        /// <param name="scheme">Schema de autentica��o</param>
        /// <returns>Builder de pipeline da aplica��o</returns>
        public static IApplicationBuilder UseAuthenticationScheme(this IApplicationBuilder app, string scheme)
        {
            return app.UseMiddleware<AuthenticateSchemeMiddleware>(scheme);
        }
    }
}