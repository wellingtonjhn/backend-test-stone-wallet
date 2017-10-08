using System;

namespace StoneWallet.Domain.Models.Entities
{
    public abstract class Entity
    {
        public Guid Id { get; protected set; } = Guid.NewGuid();
        public DateTime CreationDate { get; protected set; } = DateTime.Now;
    }
}