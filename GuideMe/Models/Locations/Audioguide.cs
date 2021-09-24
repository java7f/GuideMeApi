using Microsoft.AspNetCore.Http;
using MongoDB.Bson.Serialization.Attributes;
using System.Text.Json.Serialization;

namespace GuideMe.Models.Locations
{
    public class Audioguide : Entity
    {
        public string Name { get; set; }
        public string LocationId { get; set; }
        public string AudioguideUrl { get; set; }
        public string AudioLocale { get; set; }
        [BsonIgnore]
        public IFormFile Audiofile { get; set; }
        public string AudiofileName { get; set; }
    }
}
