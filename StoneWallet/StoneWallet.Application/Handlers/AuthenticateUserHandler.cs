using MediatR;
using StoneWallet.Application.Commands;
using StoneWallet.Application.Core.Messages;
using StoneWallet.Domain.Contracts;
using System.Threading.Tasks;

namespace StoneWallet.Application.Handlers
{
    public class AuthenticateUserHandler : IAsyncRequestHandler<AuthenticateUserCommand, Response>
    {
        private readonly IUserRepository _repository;

        public AuthenticateUserHandler(IUserRepository repository)
        {
            _repository = repository;
        }
        public async Task<Response> Handle(AuthenticateUserCommand message)
        {
            var user = await _repository.Authenticate(message.Email, message.Password);

            if (user == null)
            {
                return new Response().AddError("Usuário ou senha inválidos");
            }

            return new Response(user);
        }
    }
}