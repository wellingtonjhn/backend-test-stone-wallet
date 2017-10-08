using MediatR;
using StoneWallet.Application.Commands;
using StoneWallet.Application.Responses;
using StoneWallet.Domain.Contracts;
using System.Threading.Tasks;
using StoneWallet.Domain.Models.ValueObjects;

namespace StoneWallet.Application.Handlers
{
    public class AuthenticateUserHandler : IAsyncRequestHandler<AuthenticateUserCommand, Response>
    {
        private readonly IUsersRepository _repository;

        public AuthenticateUserHandler(IUsersRepository repository)
        {
            _repository = repository;
        }

        public async Task<Response> Handle(AuthenticateUserCommand message)
        {
            var password = new Password(message.Password);
            var user = await _repository.Authenticate(message.Email, password.Encoded);

            if (user == null)
            {
                return new Response().AddError("Usuário ou senha inválidos");
            }

            return new Response(new UserResponse(user.Id, user.Email, user.Name, user.CreationDate));
        }
    }
}