using System;

namespace StoneWallet.Domain.Models.Entities
{
    public abstract class Entity
    {
        public Guid Id { get; } = Guid.NewGuid();
        public DateTime CreationDate { get; } = DateTime.Now;
        public DateTime LastModificationDate { get; protected set; }
    }
}