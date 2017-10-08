using MediatR;
using StoneWallet.Application.Commands.Users;
using StoneWallet.Application.Commands.Wallets;
using StoneWallet.Application.Responses;
using StoneWallet.Domain.Contracts;
using StoneWallet.Domain.Models.Entities;
using System;
using System.Threading.Tasks;

namespace StoneWallet.Application.Handlers.Users
{
    /// <summary>
    /// Responsável por tratar o comando de Criação de Usuário
    /// </summary>
    public class CreateUserHandler : IAsyncRequestHandler<CreateUserCommand, Response>
    {
        private readonly IUsersRepository _repository;
        private readonly IMediator _mediator;

        /// <summary>
        /// Cria um tratador para o comando de Criação de Usuário
        /// </summary>
        /// <param name="repository">Repositório do Usuário</param>
        /// <param name="mediator"></param>
        public CreateUserHandler(IUsersRepository repository, IMediator mediator)
        {
            _repository = repository;
            _mediator = mediator;
        }

        /// <summary>
        /// Executa o tratamento do comando
        /// </summary>
        /// <param name="message">Comando de criação de usuário</param>
        /// <returns>Resposta da execução do comando</returns>
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

                return new Response(user);
            }
            catch (Exception ex)
            {
                return new Response().AddError(ex.Message);
            }
        }
    }
}