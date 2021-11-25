using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GuideMe.Models.Experiences
{
    public class GuidingOffer : Entity
    {
        public string GuideFirstName { get; set; }
        public string GuideLastName { get; set; }
        public string TouristFirstName { get; set; }
        public string TouristLastName { get; set; }
        public string GuideId { get; set; }
        public string GuideExperienceId { get; set; }
        public string GuidePhotoUrl { get; set; }
        public string TouristDestination { get; set; }
        public string TouristAlertId { get; set; }
        public string TouristId { get; set; }
        public ReservationStatus ReservationStatus { get; set; }
        public DateTime FromDate { get; set; }
        public DateTime ToDate { get; set; }
    }


}
