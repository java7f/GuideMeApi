using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GuideMe.Models.Experiences
{
    public class ExperienceReservation : ReservationBase
    {
        public string Description { get; set; }
        public List<string> ExperienceTags { get; set; }
    }
}
