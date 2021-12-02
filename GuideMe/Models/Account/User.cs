using GuideMe.Models;
using GuideMe.Models.Experiences;
using Microsoft.AspNetCore.Http;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;

namespace GuideMe.Models.Account
{
    public class User : Entity
    {
        public string FirebaseUserId { get; set; }
        public string Username { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Sex { get; set; }
        public DateTime Birthdate { get; set; }
        public string Country { get; set; }
        public string Phone { get; set; }
        public string TouristInstanceId { get; set; }
        public string GuideInstanceId { get; set; }
        public string AboutUser { get; set; }
        public List<Review> Reviews { get; set; } = new List<Review>();
        public string ProfilePhotoUrl { get; set; }
        public List<string> Roles { get; set; }
        public List<string> Wishlist { get; set; } = new List<string>();
        public List<string> Languages { get; set; } = new List<string>();
        public Address Address { get; set; } = new Address();
    }
}
