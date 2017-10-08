using MediatR;
using StoneWallet.Application.Responses;

namespace StoneWallet.Application.Commands.Wallets
{
    public class QueryWalletInformation : IRequest<Response>
    {
    }
}