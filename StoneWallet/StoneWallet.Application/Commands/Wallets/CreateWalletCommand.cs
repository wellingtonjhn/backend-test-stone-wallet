using MediatR;
using StoneWallet.Domain.Models.Entities;

namespace StoneWallet.Application.Commands.Wallets
{
    /// <summary>
    /// Representa um evento para criação de uma nova Wallet
    /// <para>Esse evento é disparado quando um novo usuário é criado</para>
    /// </summary>
    public class CreateWalletCommand : INotification
    {
        public User User { get; }

        /// <summary>
        /// Cria um evento para criação de uma nova Wallet
        /// </summary>
        /// <param name="user">Usuário dono da Wallet</param>
        public CreateWalletCommand(User user)
        {
            User = user;
        }
    }
}