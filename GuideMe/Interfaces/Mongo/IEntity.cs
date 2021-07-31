using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;

namespace GuideMe.Interfaces.Mongo
{
    public interface IEntity
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        string Id { get; set; }

        DateTime CreatedOn { get; }
    }
}
