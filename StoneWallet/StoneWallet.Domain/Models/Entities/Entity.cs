using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;

namespace StoneWallet.Domain.Models.Entities
{
    public abstract class Entity
    {
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; protected set; }
        public DateTime CreationDate { get; protected set; } = DateTime.Now;
    }
}