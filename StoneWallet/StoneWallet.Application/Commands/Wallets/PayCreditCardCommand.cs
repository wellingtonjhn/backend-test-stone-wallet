using MediatR;
using StoneWallet.Application.Responses;
using System;

namespace StoneWallet.Application.Commands.Wallets
{
    public class PayCreditCardCommand : IRequest<Response>
    {
        public Guid CardId { get; }
        public decimal Amount { get; }

        public PayCreditCardCommand(Guid cardId, decimal amount)
        {
            CardId = cardId;
            Amount = amount;
        }
    }
}