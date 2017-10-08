using System;

namespace StoneWallet.Domain.Models.Entities
{
    /// <summary>
    /// Representa uma Entidade
    /// </summary>
    public abstract class Entity
    {
        public Guid Id { get; protected set; } = Guid.NewGuid();
        public DateTime CreationDate { get; protected set; } = DateTime.Now;
    }
}