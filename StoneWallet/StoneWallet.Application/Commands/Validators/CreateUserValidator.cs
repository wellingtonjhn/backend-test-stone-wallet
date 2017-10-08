using FluentValidation;
using StoneWallet.Application.Commands.Users;

namespace StoneWallet.Application.Commands.Validators
{
    public class CreateUserValidator : AbstractValidator<CreateUserCommand>
    {
        public CreateUserValidator()
        {
            RuleFor(a => a.Name)
                .NotEmpty()
                .WithMessage("O Nome não pode ficar vazio");

            RuleFor(a => a.Email)
                .NotEmpty()
                .WithMessage("O e-mail não pode ficar vazio");

            RuleFor(a => a.Password)
                .NotEmpty()
                .WithMessage("A senha não pode ficar vazia");
        }
    }
}