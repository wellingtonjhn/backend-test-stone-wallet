using MediatR;
using StoneWallet.Application.Commands;
using StoneWallet.Application.Core.Messages;
using StoneWallet.Application.Events;
using StoneWallet.Domain.Contracts;
using StoneWallet.Domain.Models.Entities;
using System.Threading.Tasks;

namespace StoneWallet.Application.Handlers
{
    public class CreateUserHandler : IAsyncRequestHandler<CreateUser, Response>
    {
        private readonly IUserRepository _repository;

        public CreateUserHandler(IUserRepository repository)
        {
            _repository = repository;
        }

        public async Task<Response> Handle(CreateUser message)
        {
            var user = new User(message.Name, message.Email, message.Password);

            await _repository.CreateUser(user);

            return new Response(new UserCreated(user));
        }
    }
}