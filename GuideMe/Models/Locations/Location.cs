using GuideMe.Utils;
using Microsoft.AspNetCore.Http;
using MongoDB.Bson.Serialization.Attributes;

namespace GuideMe.Models.Locations
{
    public class Location : Entity
    {
        public string Name { get; set; }
        public string UserId { get; set; }
        public string LocationPhotoUrl { get; set; }
        public Coordinate Coordinates { get; set; }
        [BsonIgnore]
        public IFormFile LocationPhotoFile { get; set; }
        [BsonIgnore]
        public string LocationPhotoFileBase64 { get; set; }
        [BsonIgnore]
        public string LocationPhotoFileName { get; set; }
    }
}
