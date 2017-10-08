using Microsoft.AspNetCore.Http;
using StoneWallet.Domain.Contracts;
using System;
using System.Collections.Generic;
using System.Security.Claims;

namespace StoneWallet.Application.Core.Security
{
    public class AuthenticatedUser : IAuthenticatedUser
    {
        private readonly IHttpContextAccessor _accessor;

        public AuthenticatedUser(IHttpContextAccessor accessor)
        {
            _accessor = accessor;
        }

        public Guid UserId => new Guid(_accessor.HttpContext.User.Identity.Name);

        public bool IsAuthenticated()
        {
            return _accessor.HttpContext.User.Identity.IsAuthenticated;
        }

        public IEnumerable<Claim> GetClaimsIdentity()
        {
            return _accessor.HttpContext.User.Claims;
        }
    }
}