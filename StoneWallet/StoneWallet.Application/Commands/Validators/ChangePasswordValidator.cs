using FluentValidation;
using StoneWallet.Application.Commands.Users;

namespace StoneWallet.Application.Commands.Validators
{
    /// <summary>
    /// Representa um validador de dados para o comando de Alteração de Senha do Usuário
    /// </summary>
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