using FluentValidation;
using FluentValidation.Results;
using MediatR;
using StoneWallet.Application.Responses;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StoneWallet.Application.Core.Middlewares
{
    /// <summary>
    /// Middleware responsável por validar os comandos antes de seus respectivos Handlers serem executados (fail-fast)
    /// </summary>
    /// <typeparam name="TRequest">Tipo do comando</typeparam>
    /// <typeparam name="TResponse">Tipo do retorno do comando</typeparam>
    public class FluentValidationMiddleware<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
        where TRequest : IRequest<TResponse> where TResponse : Response
    {
        private readonly IEnumerable<IValidator> _validators;

        public FluentValidationMiddleware(IEnumerable<IValidator<TRequest>> validators)
        {
            _validators = validators;
        }

        /// <summary>
        /// Executa o middleware
        /// </summary>
        /// <param name="request">Comando a ser validado</param>
        /// <param name="next">Próximo middleware do pipeline do MediatR</param>
        /// <returns>Retorna a resposta do comando ou os erros acusados pelo validador do comando</returns>
        public Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next)
        {
            var failures = _validators
                .Select(v => v.Validate(request))
                .SelectMany(result => result.Errors)
                .Where(f => f != null)
                .ToList();

            return failures.Any()
                ? Errors(failures)
                : next();
        }

        private static Task<TResponse> Errors(IEnumerable<ValidationFailure> failures)
        {
            var response = new Response();

            foreach (var failure in failures)
            {
                response.AddError(failure.ErrorMessage);
            }

            return Task.FromResult(response as TResponse);
        }
    }
}