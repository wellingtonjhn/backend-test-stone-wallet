using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace StoneWallet.Domain.Models.Entities
{
    /// <summary>
    /// Representa uma carteira de crédito
    /// </summary>
    public class Wallet : Entity
    {
        [JsonIgnore]
        public Guid UserId { get; }

        public decimal WalletLimit { get; private set; }
        public IReadOnlyCollection<CreditCard> CreditCards { get; }
        public decimal MaximumCreditCardsLimit { get { return CreditCards.Sum(a => a.CreditLimit); } }
        public decimal AvailableCreditCardsLimit { get { return CreditCards.Sum(a => a.AvailableCredit); } }
        public decimal TotalPendingPayment { get { return CreditCards.Sum(a => a.PendingPayment); } }

        private IList<CreditCard> _creditCards { get; } = new List<CreditCard>();

        protected Wallet()
        {
            CreditCards = new ReadOnlyCollection<CreditCard>(_creditCards);
        }

        /// <summary>
        /// Cria uma nova Wallet para um usuário
        /// </summary>
        /// <param name="userId">Usuário dono da wallet</param>
        public Wallet(Guid userId) : this()
        {
            UserId = userId;
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
            CheckIfExistsCreditCard("Deve existir pelo menos um cartão de crédito cadastrado");

            if (limit > MaximumCreditCardsLimit)
            {
                throw new InvalidOperationException($"Limite superior ao permitido para esta Wallet. O limite máximo é de {MaximumCreditCardsLimit:C}");
            }

            WalletLimit = limit;
        }

        /// <summary>
        /// Realiza uma compra utilizando o melhor cartão de crédito disponível
        /// </summary>
        /// <param name="amount"></param>
        public void Buy(decimal amount)
        {
            CheckIfExistsCreditCard("Não existem cartões de crédito disponíveis para realizar essa compra");
            CheckIfExistsCreditLimitAvailabe(amount);

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

        private void CheckIfExistsCreditCard(string message)
        {
            if (!CreditCards.Any())
            {
                throw new InvalidOperationException(message);
            }
        }

        private void CheckIfExistsCreditLimitAvailabe(decimal amount)
        {
            var changeWalletLimitMessage = $"Altere o limite da Wallet para até {MaximumCreditCardsLimit:C} e tente novamente";

            if (amount > WalletLimit)
            {
                throw new InvalidOperationException($"O limite máximo para compra é de {WalletLimit:C}");
            }
            if (WalletLimit < MaximumCreditCardsLimit)
            {
                throw new InvalidOperationException($"O limite máximo para compra é de {WalletLimit:C}. {changeWalletLimitMessage}");
            }
            if (TotalPendingPayment >= WalletLimit)
            {
                throw new InvalidOperationException($"O valor limite da Wallet ({WalletLimit:C}) foi atingido. {changeWalletLimitMessage}");
            }
            if (AvailableCreditCardsLimit < amount)
            {
                throw new InvalidOperationException($"Sem crédito disponível nos cartões para realizar essa compra. O limite disponível é de {AvailableCreditCardsLimit:C}");
            }
        }
    }
}