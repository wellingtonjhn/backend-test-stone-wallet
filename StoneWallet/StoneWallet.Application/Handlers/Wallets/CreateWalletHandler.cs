using MediatR;
using StoneWallet.Application.Commands.Wallets;
using StoneWallet.Domain.Contracts;
using StoneWallet.Domain.Models.Entities;
using System.Threading.Tasks;

namespace StoneWallet.Application.Handlers.Wallets
{
    /// <summary>
    /// Responsável por tratar o evento de Criação de uma nova Wallet
    /// <para>Esse evento é disparado no momento da criação de um novo usuário</para>
    /// </summary>
    public class CreateWalletHandler : IAsyncNotificationHandler<CreateWalletCommand>
    {
        private readonly IWalletRepository _repository;

        /// <summary>
        /// Cria um tratador para o evento de Criação de uma nova Wallet
        /// </summary>
        /// <param name="repository">Repositório da Wallet</param>
        public CreateWalletHandler(IWalletRepository repository)
        {
            _repository = repository;
        }

        /// <summary>
        /// Executa o tratamento do comando
        /// </summary>
        /// <param name="notification">Evento de Criação de nova Wallet</param>
        public async Task Handle(CreateWalletCommand notification)
        {
            var wallet = new Wallet(notification.User.Id);

            await _repository.CreateWallet(wallet);
        }
    }
}