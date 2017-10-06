using MediatR;
using StoneWallet.Application.Responses;

namespace StoneWallet.Application.Queries
{
    public class QueryUserInformation : IRequest<Response>
    {
        public string UserId { get; }

        public QueryUserInformation(string userId)
        {
            UserId = userId;
        }
    }
}