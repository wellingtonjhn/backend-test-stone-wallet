using MediatR;
using StoneWallet.Application.Responses;

namespace StoneWallet.Application.Queries
{
    public class QueryWalletInformation : IRequest<Response>
    {
        public string UserId { get; }

        public QueryWalletInformation(string userId)
        {
            UserId = userId;
        }
    }
}