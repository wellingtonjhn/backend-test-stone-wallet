﻿using MediatR;
using StoneWallet.Application.Commands;
using StoneWallet.Application.Core.Messages;
using StoneWallet.Domain.Contracts;
using StoneWallet.Domain.Models.Entities;
using System.Threading.Tasks;
using StoneWallet.Domain.Models.ValueTypes;

namespace StoneWallet.Application.Handlers
{
    public class CreateUserHandler : IAsyncRequestHandler<CreateUserCommand, Response>
    {
        private readonly IUserRepository _repository;

        public CreateUserHandler(IUserRepository repository)
        {
            _repository = repository;
        }

        public async Task<Response> Handle(CreateUserCommand message)
        {
            var existsUser = await _repository.ExistsUser(message.Email);

            if (existsUser)
            {
                return new Response().AddError("Já existe um usuário com esse e-mail");
            }

            var password = new Password(message.Password);
            var user = new User(message.Name, message.Email, password.Encoded);

            await _repository.CreateUser(user);

            return new Response(user);
        }
    }
}