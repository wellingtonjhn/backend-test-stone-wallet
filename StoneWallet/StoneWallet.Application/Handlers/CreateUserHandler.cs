using MediatR;
using StoneWallet.Application.Commands;
using StoneWallet.Application.Responses;
using StoneWallet.Domain.Contracts;
using StoneWallet.Domain.Models.Entities;
using StoneWallet.Domain.Models.ValueTypes;
using System.Threading.Tasks;

namespace StoneWallet.Application.Handlers
{
    public class CreateUserHandler : IAsyncRequestHandler<CreateUserCommand, Response>
    {
        private readonly IUserRepository _repository;
        private readonly IMediator _mediator;

        public CreateUserHandler(IUserRepository repository, IMediator mediator)
        {
            _repository = repository;
            _mediator = mediator;
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
            await _mediator.Publish(new CreateWalletCommand(user));

            return new Response(new UserResponse(user.Id, user.Email, user.Name));
        }
    }
}