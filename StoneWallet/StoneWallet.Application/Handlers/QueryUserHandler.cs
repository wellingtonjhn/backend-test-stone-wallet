﻿using MediatR;
using StoneWallet.Application.Queries;
using StoneWallet.Application.Responses;
using StoneWallet.Domain.Contracts;
using System.Threading.Tasks;

namespace StoneWallet.Application.Handlers
{
    public class QueryUserHandler : IAsyncRequestHandler<QueryUserInformation, Response>
    {
        private readonly IUsersRepository _repository;

        public QueryUserHandler(IUsersRepository repository)
        {
            _repository = repository;
        }

        public async Task<Response> Handle(QueryUserInformation message)
        {
            var user = await _repository.Get(message.UserId);

            if (user == null)
            {
                return new Response().AddError("Nenhum registro encontrado");
            }

            return new Response(new UserResponse(user.Id, user.Email, user.Name, user.CreationDate));
        }
    }
}