using MediatR;
using StoneWallet.Application.Commands.Wallets;
using StoneWallet.Application.Responses;
using StoneWallet.Domain.Contracts;
using System;
using System.Threading.Tasks;

namespace StoneWallet.Application.Handlers.Wallets
{
    /// <summary>
    /// Responsável por tratar o comando de Compra da Wallet
    /// </summary>
    public class PurchaseHandler : IAsyncRequestHandler<PurchaseCommand, Response>
    {
        private readonly IWalletRepository _walletRepository;
        private readonly ICreditCardRepository _creditCardRepository;
        private readonly IAuthenticatedUser _authenticatedUser;

        /// <summary>
        /// Cria um tratador para o comando de Compra 
        /// </summary>
        /// <param name="walletRepository">Repositório da Wallet</param>
        /// <param name="creditCardRepository">Repositório de Cartão de Crédito</param>
        /// <param name="authenticatedUser">Usuário autenticado</param>
        public PurchaseHandler(
            IWalletRepository walletRepository,
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
        /// <param name="message">Comando de Compra</param>
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