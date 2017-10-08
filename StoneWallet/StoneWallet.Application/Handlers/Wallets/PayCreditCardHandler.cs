using MediatR;
using StoneWallet.Application.Commands.Wallets;
using StoneWallet.Application.Responses;
using StoneWallet.Domain.Contracts;
using System;
using System.Threading.Tasks;

namespace StoneWallet.Application.Handlers.Wallets
{
    /// <summary>
    /// Responsável por tratar o comando de pagamento do cartão de crédito
    /// </summary>
    public class PayCreditCardHandler : IAsyncRequestHandler<PayCreditCardCommand, Response>
    {
        private readonly ICreditCardRepository _creditCardRepository;

        /// <summary>
        /// Cria um tratador para o comando de pagamento do cartão de crédito
        /// </summary>
        /// <param name="creditCardRepository">Repositório de Cartão de Crédito</param>
        public PayCreditCardHandler(ICreditCardRepository creditCardRepository)
        {
            _creditCardRepository = creditCardRepository;
        }

        /// <summary>
        /// Executa o tratamento do comando
        /// </summary>
        /// <param name="message">Comando de Pagamento de Cartão de Crédito</param>
        public async Task<Response> Handle(PayCreditCardCommand message)
        {
            try
            {
                var creditCard = await _creditCardRepository.Get(message.CardId);
                creditCard.ReleaseCredit(message.Amount);

                await _creditCardRepository.ChangeCardLimits(creditCard);

                var response = new
                {
                    amountPaid = message.Amount,
                    creditCard.AvailableCredit,
                    creditCard.PendingPayment
                }; 

                return new Response(response);
            }
            catch (Exception ex)
            {
                return new Response().AddError(ex.Message);
            }
        }
    }
}