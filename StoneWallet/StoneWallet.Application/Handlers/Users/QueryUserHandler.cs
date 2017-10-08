using MediatR;
using StoneWallet.Application.Commands.Users;
using StoneWallet.Application.Responses;
using StoneWallet.Domain.Contracts;
using System;
using System.Threading.Tasks;

namespace StoneWallet.Application.Handlers.Users
{
    public class QueryUserHandler : IAsyncRequestHandler<QueryUserInformation, Response>
    {
        private readonly IUsersRepository _repository;
        private readonly IAuthenticatedUser _authenticatedUser;

        public QueryUserHandler(IUsersRepository repository, IAuthenticatedUser authenticatedUser)
        {
            _repository = repository;
            _authenticatedUser = authenticatedUser;
        }

        public async Task<Response> Handle(QueryUserInformation message)
        {
            try
            {
                var user = await _repository.Get(_authenticatedUser.UserId);

                if (user == null)
                {
                    return new Response().AddError("Nenhum registro encontrado");
                }

                return new Response(user);
            }
            catch (Exception ex)
            {
                return new Response().AddError(ex.Message);
            }
        }
    }
}