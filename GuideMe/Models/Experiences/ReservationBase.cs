using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GuideMe.Models.Experiences
{
    public class ReservationBase : Entity
    {
        public string TouristUserId { get; set; }
        public string GuideUserId { get; set; }
        public string GuideExperienceId { get; set; }
        public string GuideFirstName { get; set; }
        public string GuideLastName { get; set; }
        public string TouristFirstName { get; set; }
        public string TouristLastName { get; set; }
        public DateTime FromDate { get; set; }
        public DateTime ToDate { get; set; }
        public decimal Price { get; set; }
    }
}
