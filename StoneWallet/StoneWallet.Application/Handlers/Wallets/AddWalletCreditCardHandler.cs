using MediatR;
using StoneWallet.Application.Commands.Wallets;
using StoneWallet.Application.Responses;
using StoneWallet.Domain.Contracts;
using StoneWallet.Domain.Models.Entities;
using System;
using System.Threading.Tasks;

namespace StoneWallet.Application.Handlers.Wallets
{
    /// <summary>
    /// Responsável por tratar o comando de Inclusão de Cartão de Crédito
    /// </summary>
    public class AddWalletCreditCardHandler : IAsyncRequestHandler<AddCreditCardCommand, Response>
    {
        private readonly IWalletRepository _walletRepository;
        private readonly ICreditCardRepository _creditCardRepository;
        private readonly IAuthenticatedUser _authenticatedUser;

        /// <summary>
        /// Cria um tratador para o comando de Inclusão de Cartão de Crédito
        /// </summary>
        /// <param name="walletRepository">Repositório da Wallet</param>
        /// <param name="creditCardRepository">Repositório do Cartão de Crédito</param>
        /// <param name="authenticatedUser">Usuário Autenticado</param>
        public AddWalletCreditCardHandler(IWalletRepository walletRepository,
            ICreditCardRepository creditCardRepository,
            IAuthenticatedUser authenticatedUser)
        {
            _walletRepository = walletRepository;
            _creditCardRepository = creditCardRepository;
            _authenticatedUser = authenticatedUser;
        }

        /// <summary>
        /// Executa o tratamento do comando
        /// </summary>
        /// <param name="message">Comando de Inclusão de Cartão de Crédito</param>
        /// <returns>Resposta da execução do comando</returns>
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