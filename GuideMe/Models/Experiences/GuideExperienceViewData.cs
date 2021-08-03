using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GuideMe.Models.Experiences
{
    public class GuideExperienceViewData : Entity
    {
        public string GuideExperienceId { get; set; }
        public string GuideFirstName { get; set; }
        public string GuideLastName { get; set; }
        public float GuideRating { get; set; }
        public double GuideLatitude { get; set; }
        public double GuideLongitude { get; set; }
        public double GuidePhotoUrl { get; set; }
        public List<string> ExperienceTags { get; set; }
    }
}
