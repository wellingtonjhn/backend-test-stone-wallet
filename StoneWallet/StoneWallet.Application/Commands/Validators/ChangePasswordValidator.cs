using FluentValidation;
using StoneWallet.Application.Commands.Users;

namespace StoneWallet.Application.Commands.Validators
{
    public class ChangePasswordValidator : AbstractValidator<ChangeUserPasswordCommand>
    {
        public ChangePasswordValidator()
        {
            RuleFor(a => a.NewPassword)
                .NotEmpty()
                .WithMessage("A nova senha não pode ficar vazia");

            RuleFor(a => a.NewPasswordConfirmation)
                .NotEmpty()
                .WithMessage("A confirmação da nova senha não pode ficar vazia");
        }
    }
}