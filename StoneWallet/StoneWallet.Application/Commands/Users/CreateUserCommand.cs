using MediatR;
using StoneWallet.Application.Responses;

namespace StoneWallet.Application.Commands.Users
{
    /// <summary>
    /// Representa um comando para criação de um novo usuário
    /// </summary>
    public class CreateUserCommand : IRequest<Response>
    {
        public string Name { get; }
        public string Email { get; }
        public string Password { get; }

        /// <summary>
        /// Cria um comando para criação de um novo usuário
        /// </summary>
        /// <param name="name">Nome do usuário</param>
        /// <param name="email">E-mail do usuário</param>
        /// <param name="password">Senha do usuário</param>
        public CreateUserCommand(string name, string email, string password)
        {
            Name = name;
            Email = email;
            Password = password;
        }
    }
}