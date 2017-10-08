using FluentValidation;
using StoneWallet.Application.Commands.Users;

namespace StoneWallet.Application.Commands.Validators
{
    /// <summary>
    /// Representa um validador de dados para o comando de Criação de Usuário
    /// </summary>
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