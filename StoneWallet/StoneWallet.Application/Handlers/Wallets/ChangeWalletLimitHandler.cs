using MediatR;
using StoneWallet.Application.Commands.Wallets;
using StoneWallet.Application.Responses;
using StoneWallet.Domain.Contracts;
using System;
using System.Threading.Tasks;

namespace StoneWallet.Application.Handlers.Wallets
{
    /// <summary>
    /// Responsável por tratar o comando de Alteração no Limite da Wallet
    /// </summary>
    public class ChangeWalletLimitHandler : IAsyncRequestHandler<ChangeWalletLimitCommand, Response>
    {
        private readonly IAuthenticatedUser _authenticatedUser;
        private readonly IWalletRepository _repository;

        /// <summary>
        /// Cria um tratador para o comando de Alteração no Limite da Wallet
        /// </summary>
        /// <param name="authenticatedUser">Usuário autenticado</param>
        /// <param name="repository">Repositório da Wallet</param>
        public ChangeWalletLimitHandler(IAuthenticatedUser authenticatedUser, IWalletRepository repository)
        {
            _authenticatedUser = authenticatedUser;
            _repository = repository;
        }

        /// <summary>
        /// Executa o tratamento do comando
        /// </summary>
        /// <param name="message">Comando de Alteração do Limite da Wallet</param>
        /// <returns>Resposta da execução do comando</returns>
        public async Task<Response> Handle(ChangeWalletLimitCommand message)
        {
            try
            {
                var wallet = await _repository.GetWalletByUser(_authenticatedUser.UserId);
                wallet.ChangeWalletLimit(message.Limit);

                await _repository.ChangeWalletLimit(wallet);

                return new Response($"Limite da Wallet alterado para {wallet.WalletLimit:C}");
            }
            catch (Exception ex)
            {
                return new Response().AddError(ex.Message);
            }
        }
    }
}