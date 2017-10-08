using Newtonsoft.Json;
using System;

namespace StoneWallet.Domain.Models.Entities
{
    /// <summary>
    /// Representa um Cartão de Crédito
    /// </summary>
    public class CreditCard : Entity
    {
        [JsonIgnore]
        public Guid WalletId { get; }

        public long Number { get; }
        public DateTime DueDate { get; }
        public DateTime ExpirationDate { get; }
        public string PrintedName { get; }
        public int Cvv { get; }
        public decimal CreditLimit { get; }
        public decimal AvailableCredit { get; private set; }
        public decimal PendingPayment => CreditLimit - AvailableCredit;

        protected CreditCard() { }

        /// <summary>
        /// Novo cartão de crédito
        /// </summary>
        /// <param name="walletId">Id da Wallet</param>
        /// <param name="printedName">Nome impresso do cliente</param>
        /// <param name="number">Número do cartão</param>
        /// <param name="cvv">Código de segurança</param>
        /// <param name="creditLimit">Limite de crédito</param>
        /// <param name="dueDate">Data de vencimento</param>
        /// <param name="expirationDate">Data de validade</param>
        public CreditCard(Guid walletId, string printedName, long number, int cvv, decimal creditLimit, DateTime dueDate, DateTime expirationDate)
        {
            WalletId = walletId;
            Number = number;
            DueDate = dueDate;
            ExpirationDate = expirationDate;
            PrintedName = printedName;
            Cvv = cvv;
            CreditLimit = creditLimit;
            AvailableCredit = creditLimit;
        }

        /// <summary>
        /// Liberar limite de crédito
        /// </summary>
        /// <param printedName="amount">Valor a ser liberado</param>
        public void ReleaseCredit(decimal amount) => AvailableCredit += amount;

        /// <summary>
        /// Realiza uma compra no cartão
        /// </summary>
        /// <param name="amount">Valor da compra</param>
        public void Buy(decimal amount)
        {
            if (amount > AvailableCredit)
            {
                throw new InvalidOperationException("Cartão não possui crédito disponível para realizar a compra");
            }
            AvailableCredit -= amount;
        }

        public override int GetHashCode() => Number.GetHashCode();

        public override bool Equals(object obj)
        {
            if (obj == null || obj.GetType() != typeof(CreditCard))
                return false;

            var creditCard = obj as CreditCard;
            return Number == creditCard?.Number;
        }
    }
}