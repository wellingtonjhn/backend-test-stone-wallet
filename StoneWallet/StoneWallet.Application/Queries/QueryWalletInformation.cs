using MediatR;
using StoneWallet.Application.Responses;
using System;

namespace StoneWallet.Application.Queries
{
    public class QueryWalletInformation : IRequest<Response>
    {
        public Guid UserId { get; }

        public QueryWalletInformation(Guid userId)
        {
            UserId = userId;
        }
    }
}