using MediatR;
using StoneWallet.Application.Commands.Wallets;
using StoneWallet.Application.Responses;
using StoneWallet.Domain.Contracts;
using System.Threading.Tasks;

namespace StoneWallet.Application.Handlers.Wallets
{
    /// <summary>
    /// Responsável por tratar o comando Consulta de uma Wallet
    /// </summary>
    public class QueryWalletHandler : IAsyncRequestHandler<QueryWalletInformation, Response>
    {
        private readonly IWalletRepository _repository;
        private readonly IAuthenticatedUser _authenticatedUser;

        /// <summary>
        /// Cria um tratador para o comando de Consulta da Wallet
        /// </summary>
        /// <param name="repository"></param>
        /// <param name="authenticatedUser"></param>
        public QueryWalletHandler(IWalletRepository repository, IAuthenticatedUser authenticatedUser)
        {
            _repository = repository;
            _authenticatedUser = authenticatedUser;
        }

        /// <summary>
        /// Executa o tratamento do comando
        /// </summary>
        /// <param name="message">Comando de consulta</param>
        public async Task<Response> Handle(QueryWalletInformation message)
        {
            var wallet = await _repository.GetWalletByUser(_authenticatedUser.UserId);

            return new Response(wallet);
        }
    }
}