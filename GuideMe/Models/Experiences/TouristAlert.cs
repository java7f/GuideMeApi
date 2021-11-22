using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GuideMe.Models.Experiences
{
    public class TouristAlert : Entity
    {
        public string TouristFirstName { get; set; }
        public string TouristPhotoUrl { get; set; }
        public string TouristCountry { get; set; }
        public string TouristDestination { get; set; }
        public DateTime FromDate { get; set; }
        public DateTime ToDate { get; set; }
        public List<string> TouristLanguages { get; set; }
        public List<string> ExperienceTags { get; set; }
    }
}
