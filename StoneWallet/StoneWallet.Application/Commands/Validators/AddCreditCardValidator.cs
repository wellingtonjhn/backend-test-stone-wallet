using FluentValidation;
using StoneWallet.Application.Commands.Wallets;

namespace StoneWallet.Application.Commands.Validators
{
    public class AddCreditCardValidator : AbstractValidator<AddCreditCardCommand>
    {
        public AddCreditCardValidator()
        {
            RuleFor(card => card.Number)
                .GreaterThan(0)
                .WithMessage("Número do cartão inválido");

            RuleFor(card => card.Number.ToString().Length)
                .Equal(16)
                .WithMessage("O número do cartão deve ter 16 dígitos");

            RuleFor(card => card.Cvv)
                .GreaterThan(0)
                .WithMessage("CVV do cartão inválido");
            
            RuleFor(card => card.Cvv.ToString().Length)
                .Equal(3)
                .WithMessage("O campo CVV do cartão deve ter 3 dígitos");

            RuleFor(a => a.PrintedName)
                .NotEmpty()
                .WithMessage("O nome impresso não pode ficar vazio");
        }
    }
}