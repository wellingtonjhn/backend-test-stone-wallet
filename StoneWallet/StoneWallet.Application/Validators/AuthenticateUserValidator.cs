using FluentValidation;
using StoneWallet.Application.Commands;

namespace StoneWallet.Application.Validators
{
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