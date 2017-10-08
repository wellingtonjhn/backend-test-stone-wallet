using System;
using System.Collections.Generic;
using System.Security.Claims;

namespace StoneWallet.Domain.Contracts
{
    /// <summary>
    /// Define um usuário autenticado no sistema
    /// </summary>
    public interface IAuthenticatedUser
    {
        /// <summary>
        /// Id do usuário
        /// </summary>
        Guid UserId { get; }

        /// <summary>
        /// Verifica se o usuário está autenticado
        /// </summary>
        /// <returns>True para usuário autenticado, False para usuário não autenticado</returns>
        bool IsAuthenticated();

        /// <summary>
        /// Obtém as claims do usuário
        /// </summary>
        /// <returns></returns>
        IEnumerable<Claim> GetClaimsIdentity();
    }
}