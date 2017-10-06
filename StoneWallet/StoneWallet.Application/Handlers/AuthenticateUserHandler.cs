using MediatR;
using StoneWallet.Application.Commands;
using StoneWallet.Application.Responses;
using StoneWallet.Domain.Contracts;
using StoneWallet.Domain.Models.ValueTypes;
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
            var password = new Password(message.Password);
            var user = await _repository.Authenticate(message.Email, password.Encoded);

            if (user == null)
            {
                return new Response().AddError("Usuário ou senha inválidos");
            }

            return new Response(new UserResponse(user.Id, user.Email, user.Name));
        }
    }
}