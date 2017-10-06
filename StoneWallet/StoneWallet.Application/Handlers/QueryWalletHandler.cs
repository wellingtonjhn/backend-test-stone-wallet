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

        public QueryWalletHandler(IWalletRepository repository)
        {
            _repository = repository;
        }

        public async Task<Response> Handle(QueryWalletInformation message)
        {
            var wallet = await _repository.GetWalletByUser(message.UserId);

            return new Response(wallet);
        }
    }
}