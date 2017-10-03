using FluentValidation;
using FluentValidation.Results;
using MediatR;
using StoneWallet.Application.Core.Messages;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StoneWallet.Application.Core.Middlewares
{

    public class FluentValidationMiddleware<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
        where TRequest : IRequest<TResponse> where TResponse : Response
    {
        private readonly IEnumerable<IValidator> _validators;

        public FluentValidationMiddleware(IEnumerable<IValidator<TRequest>> validators)
        {
            _validators = validators;
        }

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
            var commandErrors = new Response(null);

            foreach (var failure in failures)
            {
                commandErrors.Errors.Add(failure.PropertyName, failure.ErrorMessage);
            }

            return Task.FromResult(commandErrors as TResponse);
        }
    }
}