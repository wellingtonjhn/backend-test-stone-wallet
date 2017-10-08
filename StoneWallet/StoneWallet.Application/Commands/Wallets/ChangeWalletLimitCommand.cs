using MediatR;
using StoneWallet.Application.Responses;

namespace StoneWallet.Application.Commands.Wallets
{
    /// <summary>
    /// Representa um comando para alteração do limite da Wallet
    /// </summary>
    public class ChangeWalletLimitCommand : IRequest<Response>
    {
        public decimal Limit { get; }

        /// <summary>
        /// Cria um comando para alteração do limite da Wallet
        /// </summary>
        /// <param name="limit">Novo limite da Wallet</param>
        public ChangeWalletLimitCommand(decimal limit)
        {
            Limit = limit;
        }
    }
}