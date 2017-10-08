using MediatR;
using StoneWallet.Application.Responses;

namespace StoneWallet.Application.Commands.Wallets
{
    public class PurchaseCommand : IRequest<Response>
    {
        public decimal Amount { get; }

        public PurchaseCommand(decimal amount)
        {
            Amount = amount;
        }
    }
}