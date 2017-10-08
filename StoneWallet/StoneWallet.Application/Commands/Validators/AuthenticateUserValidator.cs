using FluentValidation;
using StoneWallet.Application.Commands.Users;

namespace StoneWallet.Application.Commands.Validators
{
    /// <summary>
    /// Representa um validador de dados para o comando de Autenticação do Usuário
    /// </summary>
    public class AuthenticateUserValidator : AbstractValidator<AuthenticateUserCommand>
    {
        public AuthenticateUserValidator()
        {
            RuleFor(a => a.Email)
                .NotEmpty()
                .WithMessage("O e-mail não pode ficar vazio");

            RuleFor(a => a.Password)
                .NotEmpty()
                .WithMessage("A senha não pode ficar vazia");
        }
    }
}