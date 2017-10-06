using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using System;
using System.Threading.Tasks;

namespace StoneWallet.Api.Middlewares
{
    public class AuthenticateSchemeMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly string _scheme;

        public AuthenticateSchemeMiddleware(RequestDelegate next, string scheme)
        {
            _next = next;
            _scheme = scheme ?? throw new ArgumentNullException(nameof(scheme));
        }

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

    public static class AuthenticateMiddlewareExtensions
    {
        public static IApplicationBuilder UseAuthenticationScheme(this IApplicationBuilder app, string scheme)
        {
            return app.UseMiddleware<AuthenticateSchemeMiddleware>(scheme);
        }
    }
}