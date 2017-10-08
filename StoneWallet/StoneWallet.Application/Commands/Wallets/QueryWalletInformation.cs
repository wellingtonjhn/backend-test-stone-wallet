using MediatR;
using StoneWallet.Application.Responses;

namespace StoneWallet.Application.Commands.Wallets
{
    /// <summary>
    /// Representa uma consulta de dados da Wallet
    /// </summary>
    public class QueryWalletInformation : IRequest<Response>
    {
    }
}