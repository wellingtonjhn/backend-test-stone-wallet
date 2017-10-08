using MediatR;
using StoneWallet.Application.Responses;

namespace StoneWallet.Application.Commands.Wallets
{
    /// <summary>
    /// Representa um comando para realizar uma compra
    /// </summary>
    public class PurchaseCommand : IRequest<Response>
    {
        public decimal Amount { get; }

        /// <summary>
        /// Cria um comando para realizar uma compra 
        /// </summary>
        /// <param name="amount">Valor da compra</param>
        public PurchaseCommand(decimal amount)
        {
            Amount = amount;
        }
    }
}