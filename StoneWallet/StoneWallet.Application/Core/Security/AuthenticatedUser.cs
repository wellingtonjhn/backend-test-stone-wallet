using Microsoft.AspNetCore.Http;
using StoneWallet.Domain.Contracts;
using System;
using System.Collections.Generic;
using System.Security.Claims;

namespace StoneWallet.Application.Core.Security
{
    /// <summary>
    /// Representa um usuário autenticado na aplicação
    /// </summary>
    public class AuthenticatedUser : IAuthenticatedUser
    {
        private readonly IHttpContextAccessor _accessor;

        /// <summary>
        /// Cria um novo usuário autenticado no sistema
        /// </summary>
        /// <param name="accessor">Interface de acesso ao contexto HTTP do request</param>
        public AuthenticatedUser(IHttpContextAccessor accessor)
        {
            _accessor = accessor;
        }

        /// <summary>
        /// Id do usuário autenticado
        /// </summary>
        public Guid UserId => new Guid(_accessor.HttpContext.User.Identity.Name);

        /// <summary>
        /// Verifica se o usuário está autenticado
        /// </summary>
        /// <returns></returns>
        public bool IsAuthenticated()
        {
            return _accessor.HttpContext.User.Identity.IsAuthenticated;
        }

        /// <summary>
        /// Obtém as claims do usuário contidas no token JWT
        /// </summary>
        /// <returns></returns>
        public IEnumerable<Claim> GetClaimsIdentity()
        {
            return _accessor.HttpContext.User.Claims;
        }
    }
}