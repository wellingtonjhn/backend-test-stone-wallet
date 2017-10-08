using System;
using System.Collections.Generic;
using System.Security.Claims;

namespace StoneWallet.Domain.Contracts
{
    public interface IAuthenticatedUser
    {
        Guid UserId { get; }
        bool IsAuthenticated();
        IEnumerable<Claim> GetClaimsIdentity();
    }
}