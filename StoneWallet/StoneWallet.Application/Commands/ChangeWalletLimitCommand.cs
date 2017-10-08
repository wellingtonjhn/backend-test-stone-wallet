using MediatR;
using StoneWallet.Application.Responses;

namespace StoneWallet.Application.Commands
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