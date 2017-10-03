using StoneWallet.Domain.Models.Entities;

namespace StoneWallet.Application.Events
{
    public class UserCreated : Event<UserCreated>
    {
        public User User { get;  }

        public UserCreated(User user)
        {
            User = user;
        }
    }
}