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
        public decimal MaximumCreditLimit { get { return CreditCards.Sum(a => a.CreditLimit); } }
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
        /// Remove um cartão de crédito da carteira
        /// </summary>
        /// <param name="number">Número do cartão</param>
        public void RemoveCreditCard(long number)
        {
            if (CreditCards.FirstOrDefault(a => a.Number == number) == null)
            {
                throw new InvalidOperationException("Cartão de crédito não encontrado");
            }

            var card = CreditCards.First(a => a.Number == number);
            _creditCards.Remove(card);
        }
        /// <summary>
        /// Modifica o limite de crédito da wallet
        /// </summary>
        /// <param name="limit">Valor do novo limite de crédito</param>
        public void ChangeWalletLimit(decimal limit)
        {
            if (limit > MaximumCreditLimit)
            {
                throw new InvalidOperationException($"Limite superior ao permitido para esta Wallet. O limite máximo é de {MaximumCreditLimit:C}");
            }

            WalletLimit = limit;
        }

        /// <summary>
        /// Realiza uma compra utilizando o melhor cartão de crédito disponível
        /// </summary>
        /// <param name="amount"></param>
        public void Buy(decimal amount)
        {
            if (!CreditCards.Any())
            {
                throw new InvalidOperationException("Não existem cartões de crédito disponíveis para realizar essa compra");
            }

            var selectedCard = SelectBetterCard(amount);

            if (selectedCard != null && selectedCard.AvailableCredit >= amount)
            {
                selectedCard.Buy(amount);
            }
            else if (AvailableCredit >= amount)
            {
                selectedCard = SelectBetterCard(amount);

                var availableCredit = selectedCard?.AvailableCredit ?? 0;
                var diffAmount = amount - availableCredit;

                if (availableCredit == 0)
                {
                    // SPLITAR O VALOR DA COMPRA
                }
                else
                {

                    if (diffAmount <= 0)
                    {
                        selectedCard.Buy(amount);
                    }
                    else
                    {
                        selectedCard.Buy(availableCredit);

                        Buy(diffAmount);
                    }
                }
            }
        }

        /// <summary>
        /// Seleciona o melhor cartão de crédito respeitando os seguintes critérios:
        /// <para>1 - Seleciona o cartão com a maior data de vencimento</para>
        /// <para>2 - Seleciona o cartão com o menor limite de crédito</para>
        /// </summary>
        /// <returns>Cartão de crédito</returns>
        private CreditCard SelectBetterCard(decimal amount)
        {
            var preferencialCard = CreditCards
                .OrderByDescending(card => card.DueDate)
                .FirstOrDefault();

            return CreditCards.Count(a => a.DueDate.Equals(preferencialCard?.DueDate)) == 0
                ? preferencialCard
                : GetMinimumAvailableLimitCard(amount);
        }

        private CreditCard GetMinimumAvailableLimitCard(decimal amount)
        {
            return CreditCards
                .GroupBy(c => c.DueDate)
                .Select(g => g.OrderBy(a => a.CreditLimit)
                    .FirstOrDefault(a => a.AvailableCredit >= amount)
                ).FirstOrDefault();
        }
    }
}