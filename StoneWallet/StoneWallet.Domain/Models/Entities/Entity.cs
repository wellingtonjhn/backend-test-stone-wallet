using MongoDB.Bson;
using System;

namespace StoneWallet.Domain.Models.Entities
{
    public abstract class Entity
    {
        public ObjectId Id { get; } = ObjectId.GenerateNewId();
        public DateTime CreationDate { get; } = DateTime.Now;
    }
}