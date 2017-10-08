using MediatR;
using StoneWallet.Application.Responses;

namespace StoneWallet.Application.Commands.Users
{
    public class ChangeUserPasswordCommand : IRequest<Response>
    {
        public string NewPassword { get; }
        public string NewPasswordConfirmation { get; }

        public ChangeUserPasswordCommand(string newPassword, string newPasswordConfirmation)
        {
            NewPassword = newPassword;
            NewPasswordConfirmation = newPasswordConfirmation;
        }
    }
}