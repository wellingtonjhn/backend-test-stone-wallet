using MediatR;
using StoneWallet.Application.Commands.Wallets;
using StoneWallet.Application.Responses;
using StoneWallet.Domain.Contracts;
using System;
using System.Threading.Tasks;

namespace StoneWallet.Application.Handlers.Wallets
{
    public class PayCreditCardHandler : IAsyncRequestHandler<PayCreditCardCommand, Response>
    {
        private readonly ICreditCardRepository _creditCardRepository;

        public PayCreditCardHandler(ICreditCardRepository creditCardRepository)
        {
            _creditCardRepository = creditCardRepository;
        }

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