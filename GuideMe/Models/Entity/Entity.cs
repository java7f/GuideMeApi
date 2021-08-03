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
    public class Entity : IEntity
    {
        private DateTime _createdOn;
        public Entity()
        {
            Id = ObjectId.GenerateNewId().ToString();
        }

        [BsonElement(Order = 0)]
        public string Id { get; set; }

        [BsonElement(Order = 1)]
        public DateTime CreatedOn
        {
            get
            {
                if (_createdOn == null || _createdOn == DateTime.MinValue)
                    _createdOn = ObjectId.CreationTime;
                return _createdOn;
            }
            set
            {
                _createdOn = value;
            }
        }

        public ObjectId ObjectId => ObjectId.Parse(Id);
    }
}
