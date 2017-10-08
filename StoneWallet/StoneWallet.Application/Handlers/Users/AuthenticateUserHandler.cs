using MediatR;
using StoneWallet.Application.Commands.Users;
using StoneWallet.Application.Responses;
using StoneWallet.Domain.Contracts;
using StoneWallet.Domain.Models.ValueObjects;
using System;
using System.Threading.Tasks;

namespace StoneWallet.Application.Handlers.Users
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
            try
            {
                var password = new Password(message.Password);
                var user = await _repository.Authenticate(message.Email, password.Encoded);

                return user == null 
                    ? new Response().AddError("Usuário ou senha inválidos") 
                    : new Response(user);
            }
            catch (Exception ex)
            {
                return new Response().AddError(ex.Message);
            }
        }
    }
}