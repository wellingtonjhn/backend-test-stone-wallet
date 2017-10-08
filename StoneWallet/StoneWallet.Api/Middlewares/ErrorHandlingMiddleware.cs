using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Net;
using System.Threading.Tasks;

namespace StoneWallet.Api.Middlewares
{
    /// <summary>
    /// Middleware responsável por capturar exceções não tratadas da aplicação
    /// </summary>
    public class ErrorHandlingMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger _logger;

        /// <summary>
        /// Cria um novo midldeware para capturar exceções não tratadas
        /// </summary>
        /// <param name="next">Próximo middleware do pipeline</param>
        /// <param name="loggerFactory">Factory responsável por criar um Logger para o middleware</param>
        public ErrorHandlingMiddleware(RequestDelegate next, ILoggerFactory loggerFactory)
        {
            _next = next;
            _logger = loggerFactory.CreateLogger("Error");
        }

        /// <summary>
        /// Executa o middleware de forma assíncrona
        /// </summary>
        /// <param name="context">Contexto HTTP do request</param>
        /// <returns></returns>
        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.ToString(), "Unhandled error");

                await HandleExceptionAsync(context, ex);
            }
        }

        private static Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            const HttpStatusCode code = HttpStatusCode.InternalServerError;
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)code;
            return context.Response.WriteAsync(JsonConvert.SerializeObject(exception));
        }
    }

    /// <summary>
    /// Classe de extensão para caputrar exceções não tratadas
    /// </summary>
    public static class ErrorHandlingMiddlewareExtensions
    {
        /// <summary>
        /// Usa o middleware para capturar exceções não tratadas na aplicação
        /// </summary>
        /// <param name="app">Builder de pipeline da aplicação</param>
        /// <returns>Builder de pipeline da aplicação</returns>
        public static IApplicationBuilder UseErrorHandling(this IApplicationBuilder app)
        {
            return app.UseMiddleware<ErrorHandlingMiddleware>();
        }
    }
}