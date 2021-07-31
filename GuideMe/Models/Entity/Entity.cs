using GuideMe.Interfaces.Mongo;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;

namespace GuideMe.Models
{
    public abstract class Entity : IEntity
    {
        public String Id { get; set; }
        public DateTime CreatedOn => DateTime.Now;
    }
}
