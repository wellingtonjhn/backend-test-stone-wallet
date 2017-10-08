﻿using FluentValidation;
using StoneWallet.Application.Commands.Wallets;

namespace StoneWallet.Application.Commands.Validators
{
    public class PurchaseValidator : AbstractValidator<PurchaseCommand>
    {
        public PurchaseValidator()
        {
            RuleFor(card => card.Amount)
                .GreaterThan(0)
                .WithMessage($"O valor da compra deve ser maior que {0:C}");
        }
    }
}