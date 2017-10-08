using System;

namespace StoneWallet.Application.Responses
{
    public class UserResponse
    {
        public Guid Id { get; }
        public string Email { get; }
        public string Name { get; }
        public DateTime CreationDate { get; }

        public UserResponse(Guid id, string email, string name, DateTime creationDate)
        {
            Id = id;
            Email = email;
            Name = name;
            CreationDate = creationDate;
        }
    }
}