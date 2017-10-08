using FluentValidation;
using StoneWallet.Application.Commands.Wallets;

namespace StoneWallet.Application.Commands.Validators
{
    public class ChangeWalletLimitValidator : AbstractValidator<ChangeWalletLimitCommand>
    {
        public ChangeWalletLimitValidator()
        {
            RuleFor(a => a.Limit)
                .GreaterThan(0)
                .WithMessage($"O limite real da Wallet deve ser maior que {0:C}");
        }
    }
}