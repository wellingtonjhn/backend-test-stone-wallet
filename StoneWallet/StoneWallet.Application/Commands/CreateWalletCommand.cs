using MediatR;
using StoneWallet.Domain.Models.Entities;

namespace StoneWallet.Application.Commands
{
    public class CreateWalletCommand : INotification
    {
        public User User { get; }

        public CreateWalletCommand(User user)
        {
            User = user;
        }
    }
}