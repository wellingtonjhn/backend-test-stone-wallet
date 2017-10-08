using MediatR;
using StoneWallet.Application.Responses;

namespace StoneWallet.Application.Commands.Wallets
{
    public class ChangeWalletLimitCommand : IRequest<Response>
    {
        public decimal Limit { get; }

        public ChangeWalletLimitCommand(decimal limit)
        {
            Limit = limit;
        }
    }
}