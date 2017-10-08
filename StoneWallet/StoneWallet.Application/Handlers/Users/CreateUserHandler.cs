﻿using MediatR;
using StoneWallet.Application.Commands.Users;
using StoneWallet.Application.Commands.Wallets;
using StoneWallet.Application.Responses;
using StoneWallet.Domain.Contracts;
using StoneWallet.Domain.Models.Entities;
using System;
using System.Threading.Tasks;

namespace StoneWallet.Application.Handlers.Users
{
    public class CreateUserHandler : IAsyncRequestHandler<CreateUserCommand, Response>
    {
        private readonly IUsersRepository _repository;
        private readonly IMediator _mediator;

        public CreateUserHandler(IUsersRepository repository, IMediator mediator)
        {
            _repository = repository;
            _mediator = mediator;
        }

        public async Task<Response> Handle(CreateUserCommand message)
        {
            try
            {
                var existsUser = await _repository.ExistsUser(message.Email);

                if (existsUser)
                {
                    return new Response().AddError("Já existe um usuário com esse e-mail");
                }

                var user = new User(message.Name, message.Email, message.Password);

                await _repository.CreateUser(user);
                await _mediator.Publish(new CreateWalletCommand(user));

                return new Response(new UserResponse(user.Id, user.Email, user.Name, user.CreationDate));
            }
            catch (Exception ex)
            {
                return new Response().AddError(ex.Message);
            }
        }
    }
}