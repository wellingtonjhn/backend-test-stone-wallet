using System;
using StoneWallet.Domain.Models.Entities;
using System.Threading.Tasks;

namespace StoneWallet.Domain.Contracts
{
    /// <summary>
    /// Define o repositório de uma Wallet
    /// </summary>
    public interface IWalletRepository
    {
        /// <summary>
        /// Cria uma nova Wallet
        /// </summary>
        /// <param name="wallet">Wallet</param>
        /// <returns></returns>
        Task CreateWallet(Wallet wallet);

        /// <summary>
        /// Obtém uma Wallet pelo Id do usuário
        /// </summary>
        /// <param name="userId">Id do usuário</param>
        /// <returns>Wallet</returns>
        Task<Wallet> GetWalletByUser(Guid userId);

        /// <summary>
        /// Altera o limite da Wallet
        /// </summary>
        /// <param name="wallet">Wallet</param>
        /// <returns></returns>
        Task ChangeWalletLimit(Wallet wallet);
    }
}