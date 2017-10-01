using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace StoneWallet.Domain.Models.Entities
{
    /// <summary>
    /// Representa uma carteira de crédito
    /// </summary>
    public sealed class Wallet : Entity
    {
        private IList<CreditCard> _creditCards { get; } = new List<CreditCard>();

        public User User { get; }
        public decimal WalletLimit { get; private set; }
        public decimal MaxLimit { get { return CreditCards.Sum(a => a.CreditLimit); } }
        public decimal AvailableCredit { get { return CreditCards.Sum(a => a.AvailableCredit); } }
        public IReadOnlyCollection<CreditCard> CreditCards { get; }

        /// <summary>
        /// Nova wallet
        /// </summary>
        /// <param name="user">Usuário dono da wallet</param>
        public Wallet(User user)
        {
            User = user;
            CreditCards = new ReadOnlyCollection<CreditCard>(_creditCards);
        }

        /// <summary>
        /// Adicionar um novo cartão de crédito à carteira
        /// </summary>
        /// <param name="card">Cartão de crédito</param>
        public void AddCreditCard(CreditCard card)
        {
            if (CreditCards.Contains(card))
            {
                throw new InvalidOperationException("A Wallet já possui um cartão com essas caracteristicas");
            }

            _creditCards.Add(card);
        }

        /// <summary>
        /// Modifica o limite de crédito da wallet
        /// </summary>
        /// <param name="limit">Valor do novo limite de crédito</param>
        public void ChangeWalletLimit(decimal limit)
        {
            if (limit > MaxLimit)
            {
                throw new InvalidOperationException($"Limite superior ao permitido para esta Wallet. O limite máximo é de {MaxLimit:C}");
            }

            WalletLimit = limit;
        }
    }
}