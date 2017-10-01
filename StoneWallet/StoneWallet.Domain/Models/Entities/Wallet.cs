using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace StoneWallet.Domain.Models.Entities
{
    public sealed class Wallet : Entity
    {
        private IList<CreditCard> _creditCards { get; } = new List<CreditCard>();

        public User User { get; }
        public decimal MaxLimit { get; private set; }
        public decimal WalletLimit { get; private set; }
        public decimal AvailableCredit { get { return CreditCards.Sum(a => a.AvailableCredit); } }
        public IReadOnlyCollection<CreditCard> CreditCards { get; }

        public Wallet(User user)
        {
            User = user;
            CreditCards = new ReadOnlyCollection<CreditCard>(_creditCards);
        }

        public void AddCreditCard(CreditCard card)
        {
            if (CreditCards.Contains(card))
            {
                throw new InvalidOperationException("A Wallet já possui um cartão com essas caracteristicas");
            }

            _creditCards.Add(card);

            CalculateMaxCreditLimit();
        }

        public void ChangeWalletLimit(decimal limit)
        {
            if (limit > MaxLimit)
            {
                throw new InvalidOperationException($"Limite superior ao permitido para esta Wallet. O limite máximo é de {MaxLimit:C}");
            }

            WalletLimit = limit;
        }
        
        private void CalculateMaxCreditLimit()
        {
            var maxLimit = CreditCards.Sum(a => a.CreditLimit);
            MaxLimit = maxLimit;
        }
    }
}