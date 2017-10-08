using MediatR;
using StoneWallet.Application.Commands.Wallets;
using StoneWallet.Application.Responses;
using StoneWallet.Domain.Contracts;
using StoneWallet.Domain.Models.Entities;
using System;
using System.Threading.Tasks;

namespace StoneWallet.Application.Handlers.Wallets
{
    public class AddWalletCreditCardHandler : IAsyncRequestHandler<AddCreditCardCommand, Response>
    {
        private readonly IWalletRepository _walletRepository;
        private readonly ICreditCardRepository _creditCardRepository;
        private readonly IAuthenticatedUser _authenticatedUser;

        public AddWalletCreditCardHandler(IWalletRepository walletRepository,
            ICreditCardRepository creditCardRepository,
            IAuthenticatedUser authenticatedUser)
        {
            _walletRepository = walletRepository;
            _creditCardRepository = creditCardRepository;
            _authenticatedUser = authenticatedUser;
        }

        public async Task<Response> Handle(AddCreditCardCommand message)
        {
            try
            {
                var wallet = await _walletRepository.GetWalletByUser(_authenticatedUser.UserId);

                var creditCard = new CreditCard(
                    wallet.Id,
                    message.PrintedName,
                    message.Number,
                    message.Cvv,
                    message.CreditLimit,
                    message.DueDate,
                    message.ExpirationDate);

                wallet.AddCreditCard(creditCard);

                await _creditCardRepository.CreateCreditCard(creditCard);

                return new Response(creditCard);
            }
            catch (Exception ex)
            {
                return new Response().AddError(ex.Message);
            }
        }
    }
}