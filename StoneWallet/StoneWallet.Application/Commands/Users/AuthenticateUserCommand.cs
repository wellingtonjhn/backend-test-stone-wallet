using MediatR;
using StoneWallet.Application.Responses;

namespace StoneWallet.Application.Commands.Users
{
    public class AuthenticateUserCommand : IRequest<Response>
    {
        public string Email { get; set; }
        public string Password { get; set; }

        public AuthenticateUserCommand(string email, string password)
        {
            Email = email;
            Password = password;
        }
    }
}