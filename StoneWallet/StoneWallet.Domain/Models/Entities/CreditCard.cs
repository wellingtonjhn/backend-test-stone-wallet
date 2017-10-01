using System;

namespace StoneWallet.Domain.Models.Entities
{
    public sealed class CreditCard : Entity
    {
        public long Number { get; }
        public DateTime DueDate { get; }
        public DateTime ExpirationDate { get; }
        public string Name { get; }
        public int Cvv { get; }
        public decimal CreditLimit { get; }
        public decimal AvailableCredit { get; private set; }

        public CreditCard(string name, long number, int cvv, decimal creditLimit, DateTime dueDate, DateTime expirationDate)
        {
            Number = number;
            DueDate = dueDate;
            ExpirationDate = expirationDate;
            Name = name;
            Cvv = cvv;
            CreditLimit = creditLimit;
            AvailableCredit = creditLimit;
        }

        public void ReleaseCredit(decimal amount)
        {
            AvailableCredit += amount;
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