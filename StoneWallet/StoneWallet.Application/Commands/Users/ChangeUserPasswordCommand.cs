using MediatR;
using StoneWallet.Application.Responses;

namespace StoneWallet.Application.Commands.Users
{
    /// <summary>
    /// Representa um comando para trocar a senha do usuário
    /// </summary>
    public class ChangeUserPasswordCommand : IRequest<Response>
    {
        public string NewPassword { get; }
        public string NewPasswordConfirmation { get; }

        /// <summary>
        /// Cria um comando para trocar a senha do usuário
        /// </summary>
        /// <param name="newPassword">Nova senha</param>
        /// <param name="newPasswordConfirmation">Confirmação da nova senha</param>
        public ChangeUserPasswordCommand(string newPassword, string newPasswordConfirmation)
        {
            NewPassword = newPassword;
            NewPasswordConfirmation = newPasswordConfirmation;
        }
    }
}