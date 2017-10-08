using MediatR;
using StoneWallet.Application.Responses;
using System;

namespace StoneWallet.Application.Commands.Wallets
{
    public class AddCreditCardCommand : IRequest<Response>
    {
        public long Number { get; }
        public DateTime DueDate { get; }
        public DateTime ExpirationDate { get; }
        public string PrintedName { get; }
        public int Cvv { get; }
        public decimal CreditLimit { get; }

        public AddCreditCardCommand(long number, DateTime dueDate, DateTime expirationDate, string printedName, int cvv, decimal creditLimit)
        {
            Number = number;
            DueDate = dueDate;
            ExpirationDate = expirationDate;
            PrintedName = printedName;
            Cvv = cvv;
            CreditLimit = creditLimit;
        }
    }
}