using MediatR;
using StoneWallet.Application.Responses;
using System;

namespace StoneWallet.Application.Commands.Wallets
{
    /// <summary>
    /// Representa um comando para criação de um novo cartão de crédito
    /// </summary>
    public class AddCreditCardCommand : IRequest<Response>
    {
        public long Number { get; }
        public DateTime DueDate { get; }
        public DateTime ExpirationDate { get; }
        public string PrintedName { get; }
        public int Cvv { get; }
        public decimal CreditLimit { get; }

        /// <summary>
        /// Cria um comando para criação de um novo cartão de crédito
        /// </summary>
        /// <param name="number">Número do cartão</param>
        /// <param name="dueDate">Data de vencimento</param>
        /// <param name="expirationDate">Data de expiração</param>
        /// <param name="printedName">Nome impresso no cartão</param>
        /// <param name="cvv">Código de segurança</param>
        /// <param name="creditLimit">Limite de crédito</param>
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