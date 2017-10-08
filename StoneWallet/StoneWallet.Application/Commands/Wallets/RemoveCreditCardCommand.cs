using System;
using MediatR;
using StoneWallet.Application.Responses;

namespace StoneWallet.Application.Commands.Wallets
{
    public class RemoveCreditCardCommand : IRequest<Response>
    {
        public Guid CreditCardId { get; }

        public RemoveCreditCardCommand(Guid creditCardId)
        {
            CreditCardId = creditCardId;
        }
    }
}