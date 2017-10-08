using MediatR;
using StoneWallet.Application.Queries;
using StoneWallet.Application.Responses;
using StoneWallet.Domain.Contracts;
using System.Threading.Tasks;

namespace StoneWallet.Application.Handlers
{
    public class QueryWalletHandler : IAsyncRequestHandler<QueryWalletInformation, Response>
    {
        private readonly IWalletRepository _repository;
        private readonly IAuthenticatedUser _authenticatedUser;

        public QueryWalletHandler(IWalletRepository repository, IAuthenticatedUser authenticatedUser)
        {
            _repository = repository;
            _authenticatedUser = authenticatedUser;
        }

        public async Task<Response> Handle(QueryWalletInformation message)
        {
            var wallet = await _repository.GetWalletByUser(_authenticatedUser.UserId);

            return new Response(wallet);
        }
    }
}