using MediatR;
using StoneWallet.Application.Responses;

namespace StoneWallet.Application.Commands.Users
{
    public class CreateUserCommand : IRequest<Response>
    {
        public string Name { get; }
        public string Email { get; }
        public string Password { get; }

        public CreateUserCommand(string name, string email, string password)
        {
            Name = name;
            Email = email;
            Password = password;
        }
    }
}