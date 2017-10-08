using System;
using MediatR;
using StoneWallet.Application.Commands.Wallets;
using StoneWallet.Application.Responses;
using System.Threading.Tasks;
using StoneWallet.Domain.Contracts;

namespace StoneWallet.Application.Handlers.Wallets
{
    public class PurchaseHandler : IAsyncRequestHandler<PurchaseCommand, Response>
    {
        private readonly IWalletRepository _walletRepository;
        private readonly ICreditCardRepository _creditCardRepository;
        private readonly IAuthenticatedUser _authenticatedUser;

        public PurchaseHandler(
            IWalletRepository walletRepository,
            ICreditCardRepository creditCardRepository,
            IAuthenticatedUser authenticatedUser)
        {
            _walletRepository = walletRepository;
            _creditCardRepository = creditCardRepository;
            _authenticatedUser = authenticatedUser;
        }

        public async Task<Response> Handle(PurchaseCommand message)
        {
            try
            {
                var wallet = await _walletRepository.GetWalletByUser(_authenticatedUser.UserId);
                wallet.Buy(message.Amount);

                foreach (var creditCard in wallet.CreditCards)
                {
                    await _creditCardRepository.ChangeCardLimits(creditCard);
                }

                return new Response($"Compra realizada com sucesso no valor de {message.Amount:C}");
            }
            catch (Exception ex)
            {
                return new Response().AddError(ex.Message);
            }
        }
    }
}