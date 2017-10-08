using MediatR;
using StoneWallet.Application.Responses;
using System;

namespace StoneWallet.Application.Queries
{
    public class QueryUserInformation : IRequest<Response>
    {
        public Guid UserId { get; }

        public QueryUserInformation(Guid userId)
        {
            UserId = userId;
        }
    }
}