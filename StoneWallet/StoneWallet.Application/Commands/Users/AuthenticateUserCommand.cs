using MediatR;
using StoneWallet.Application.Responses;

namespace StoneWallet.Application.Commands.Users
{
    /// <summary>
    /// Representa um comando para autenticar um usuário na aplicação
    /// </summary>
    public class AuthenticateUserCommand : IRequest<Response>
    {
        public string Email { get; set; }
        public string Password { get; set; }

        /// <summary>
        /// Cria um comando para autenticar o usuário
        /// </summary>
        /// <param name="email">E-mail do usuário</param>
        /// <param name="password">Senha do usuário</param>
        public AuthenticateUserCommand(string email, string password)
        {
            Email = email;
            Password = password;
        }
    }
}