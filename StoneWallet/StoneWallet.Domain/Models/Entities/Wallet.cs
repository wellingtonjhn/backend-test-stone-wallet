using MongoDB.Bson;
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

        public ObjectId User { get; private set; }
        public decimal WalletLimit { get; private set; }
        public IReadOnlyCollection<CreditCard> CreditCards { get; private set; }
        public decimal MaximumCreditLimit { get { return CreditCards.Sum(a => a.CreditLimit); } }
        public decimal AvailableCredit { get { return CreditCards.Sum(a => a.AvailableCredit); } }

        /// <summary>
        /// Nova wallet
        /// </summary>
        /// <param name="userId">Usuário dono da wallet</param>
        public Wallet(ObjectId userId)
        {
            User = userId;
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

            if (AvailableCredit < amount)
            {
                throw new InvalidOperationException("Sem crédito disponível para realizar essa compra");
            }

            var selectedCard = SelectBetterCard();

            if (selectedCard.AvailableCredit >= amount)
            {
                selectedCard.Buy(amount);
            }
            else
            {
                SplitPurchaseIntoMultipleCards(amount, selectedCard);
            }
        }

        /// <summary>
        /// Realiza a divisão da compra em multiplos cartões
        /// </summary>
        /// <param name="amount">Valor a ser debitado no cartão</param>
        /// <param name="preSelectedCard">Cartão pré-selecionado</param>
        private void SplitPurchaseIntoMultipleCards(decimal amount, CreditCard preSelectedCard)
        {
            var availableCredit = preSelectedCard.AvailableCredit;
            var diffAmount = amount - availableCredit;

            preSelectedCard.Buy(availableCredit);
            Buy(diffAmount);

        }

        /// <summary>
        /// Seleciona o melhor cartão de crédito respeitando os seguintes critérios:
        /// <para>1 - Seleciona o cartão com a maior data de vencimento</para>
        /// <para>2 - Seleciona o cartão com o menor limite de crédito caso existam datas de vencimento duplicadas</para>
        /// </summary>
        /// <returns>Cartão de crédito</returns>
        private CreditCard SelectBetterCard()
        {
            var preferencialCard = GetPreferencialCard();
            var duplicatedDueDates = ExistsDuplicatedDueDates();

            return duplicatedDueDates
                ? GetMinimumAvailableLimitCard()
                : preferencialCard;
        }

        private CreditCard GetPreferencialCard()
        {
            return CreditCards
                .OrderByDescending(card => card.DueDate.Date)
                .FirstOrDefault(card => card.AvailableCredit > 0);
        }

        private CreditCard GetMinimumAvailableLimitCard()
        {
            return CreditCards
                .GroupBy(card => card.DueDate.Date)
                .Select(g => g.OrderBy(card => card.CreditLimit)
                        .FirstOrDefault(card => card.AvailableCredit > 0)
                ).FirstOrDefault();
        }

        private bool ExistsDuplicatedDueDates()
        {
            return CreditCards
                .GroupBy(x => x.DueDate.Date)
                .Any(g => g.Count() > 1);
        }
    }
}