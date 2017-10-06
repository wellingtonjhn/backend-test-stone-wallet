using MediatR;
using MongoDB.Bson;
using StoneWallet.Application.Commands;
using StoneWallet.Domain.Contracts;
using StoneWallet.Domain.Models.Entities;
using System.Threading.Tasks;

namespace StoneWallet.Application.Handlers
{
    public class CreateWalletHandler : IAsyncNotificationHandler<CreateWalletCommand>
    {
        private readonly IWalletRepository _repository;

        public CreateWalletHandler(IWalletRepository repository)
        {
            _repository = repository;
        }

        public async Task Handle(CreateWalletCommand notification)
        {
            var wallet = new Wallet(new ObjectId(notification.User.Id));

            await _repository.CreateWallet(wallet);
        }
    }
}