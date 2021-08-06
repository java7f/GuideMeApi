using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson.Serialization.IdGenerators;
using System;

namespace GuideMe.Interfaces.Mongo
{
    public interface IEntity
    {
        [BsonId]
        string Id { get; set; }
        DateTime CreatedOn { get; }
    }
}
