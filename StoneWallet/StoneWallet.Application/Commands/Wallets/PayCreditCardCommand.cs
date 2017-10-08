using MediatR;
using StoneWallet.Application.Responses;
using System;

namespace StoneWallet.Application.Commands.Wallets
{
    /// <summary>
    /// Representa um comando para pagamento de um cartão de crédito
    /// </summary>
    public class PayCreditCardCommand : IRequest<Response>
    {
        public Guid CardId { get; }
        public decimal Amount { get; }

        /// <summary>
        /// Cria um comando para pagamento de um cartão de crédito
        /// </summary>
        /// <param name="cardId">Id do cartão de crédito</param>
        /// <param name="amount">Valor para pagamento</param>
        public PayCreditCardCommand(Guid cardId, decimal amount)
        {
            CardId = cardId;
            Amount = amount;
        }
    }
}