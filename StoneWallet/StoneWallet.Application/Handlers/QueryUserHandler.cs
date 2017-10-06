using MediatR;
using StoneWallet.Application.Queries;
using StoneWallet.Application.Responses;
using StoneWallet.Domain.Contracts;
using System.Threading.Tasks;

namespace StoneWallet.Application.Handlers
{
    public class QueryUserHandler : IAsyncRequestHandler<QueryUserInformation, Response>
    {
        private readonly IUserRepository _repository;

        public QueryUserHandler(IUserRepository repository)
        {
            _repository = repository;
        }

        public async Task<Response> Handle(QueryUserInformation message)
        {
            var user = await _repository.Get(message.UserId);

            return new Response(new UserResponse(user.Id, user.Email, user.Name));
        }
    }
}