using StoneWallet.Domain.Models.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace StoneWallet.Domain.Contracts
{
    /// <summary>
    /// Define um contrato de repositório de Cartão de Crédito
    /// </summary>
    public interface ICreditCardRepository
    {
        /// <summary>
        /// Obtém um Cartão de Crédito por Id
        /// </summary>
        /// <param name="id">Id do Cartão de Crédito</param>
        /// <returns>Cartão de Crédito</returns>
        Task<CreditCard> Get(Guid id);

        /// <summary>
        /// Obtém uma lista de Cartões de Crédito para determinada Wallet
        /// </summary>
        /// <param name="id">Id da Wallet</param>
        /// <returns>Lista de Cartões de Crédito</returns>
        Task<IEnumerable<CreditCard>> GetByWalletId(Guid id);

        /// <summary>
        /// Adiciona um novo Cartão de Crédito
        /// </summary>
        /// <param name="card">Cartão de Crédito</param>
        /// <returns></returns>
        Task CreateCreditCard(CreditCard card);

        /// <summary>
        /// Remove um novo Cartão de Crédito
        /// </summary>
        /// <param name="card">Cartão de Crédito</param>
        /// <returns></returns>
        Task RemoveCreditCard(CreditCard card);

        /// <summary>
        /// Altera os limites de um Cartão de Crédito
        /// </summary>
        /// <param name="card">Cartão de Crédito</param>
        /// <returns></returns>
        Task ChangeCardLimits(CreditCard card);
    }
}