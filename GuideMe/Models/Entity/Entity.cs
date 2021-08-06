using GuideMe.Interfaces.Mongo;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;

namespace GuideMe.Models
{
    /// <summary>
    /// Represents an Entity in MongoDB
    /// </summary>
    [BsonIgnoreExtraElements(Inherited = true)]
    public abstract class Entity : IEntity
    {
        private DateTime _createdOn;
        public Entity()
        {
            Id = ObjectId.GenerateNewId().ToString();
        }

        [BsonElement(Order = 0)]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }

        [BsonElement(Order = 1)]
        public DateTime CreatedOn
        {
            get
            {
                if (_createdOn == null || _createdOn == DateTime.MinValue)
                    _createdOn = DateTime.Now;
                return _createdOn;
            }
            set
            {
                _createdOn = value;
            }
        }
    }
}
