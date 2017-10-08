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
                .WithMessage("O novo limite da Wallet deve ser maior que zero");
        }
    }
}