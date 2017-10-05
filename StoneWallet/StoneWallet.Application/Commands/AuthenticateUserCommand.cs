using MediatR;
using StoneWallet.Application.Core.Messages;

namespace StoneWallet.Application.Commands
{
    public class AuthenticateUserCommand : IRequest<Response>
    {
        public string Email { get; }
        public string Password { get; }

        public AuthenticateUserCommand(string email, string password)
        {
            Email = email;
            Password = password;
        }
    }
}