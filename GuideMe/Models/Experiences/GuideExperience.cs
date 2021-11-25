using GuideMe.Models.Account;
using Microsoft.AspNetCore.Http;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GuideMe.Models.Experiences
{
    public class GuideExperience : Entity
    {
        public string GuideFirebaseId { get; set; }
        public string GuideFirstName { get; set; }
        public string GuideLastName { get; set; }
        public string ExperienceDescription { get; set; }
        public decimal GuideRating { get; set; }
        public decimal ExperiencePrice { get; set; }
        public Address GuideAddress { get; set; }
        public string GuidePhotoUrl { get; set; }
        public List<string> ExperienceTags { get; set; } = new List<string>();
        public List<Review> GuideReviews { get; set; } = new List<Review>();
    }
}
