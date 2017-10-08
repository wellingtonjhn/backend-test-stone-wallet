using System;
using MediatR;
using StoneWallet.Application.Responses;

namespace StoneWallet.Application.Commands.Wallets
{
    /// <summary>
    /// Representa um comando para exclusão de um cartão de crédito
    /// </summary>
    public class RemoveCreditCardCommand : IRequest<Response>
    {
        public Guid CreditCardId { get; }

        /// <summary>
        /// Cria um comando para exclusão de um cartão de crédito
        /// </summary>
        /// <param name="creditCardId">Id do cartão de crédito</param>
        public RemoveCreditCardCommand(Guid creditCardId)
        {
            CreditCardId = creditCardId;
        }
    }
}