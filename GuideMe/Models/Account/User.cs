using GuideMe.Models;
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
        public string AboutUser { get; set; }
        public List<Review> Reviews { get; set; }
        public string ProfilePhotoUrl { get; set; }
        public List<string> Roles { get; set; }
    }
}
