using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net.Sockets;

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

        public void Buy(decimal amount)
        {
            if (!CreditCards.Any())
            {
                throw new InvalidOperationException("Não existem cartões de crédito disponíveis para realizar essa compra");    
            }

            //Você prefere usar o cartão que está mais longe de vencer porque terá mais tempo para pagar a conta.
            var preferencialCard = CreditCards
                .OrderByDescending(card => card.DueDate)
                .FirstOrDefault();

            //Caso os dois cartões vençam no mesmo dia, você prefere usar aquele que tem menor limite para continuar tendo um cartão com o limite mais alto.
            if (CreditCards.Any(a => a.DueDate.Equals(preferencialCard.DueDate) && a.Number != preferencialCard.Number))
            {
                preferencialCard = CreditCards
                    .GroupBy(cards => cards.DueDate)
                    .Select(g => g.OrderBy(a => a.CreditLimit).FirstOrDefault())
                    .FirstOrDefault();
            }

            if (preferencialCard.AvailableCredit >= amount)
            {
                preferencialCard.Buy(amount);
            }
            else if (CreditCards.Count > 1)
            {
                //Somente no caso em que não for possível fazer a compra em um único cartão, o sistema deve dividir a compra em mais cartões.
                //Para isso, você vai preenchendo os cartões usando as mesmas ordens de prioridade já descritas.
                //Ou seja, você gasta primeiro do cartão que está mais longe de vencer e "completa" com o próximo cartão mais longe de vencer.
                //Caso os cartões vençam no mesmo dia, você gasta primeiro do com menor limite e "completa" com o que tem mais limite.

                var availableCredit = preferencialCard.AvailableCredit;
                preferencialCard.Buy(availableCredit);

                var diffAmount = amount - availableCredit;
                Buy(diffAmount);
            }
        }
    }
}