using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GuideMe.Models.Experiences
{
    public class ExperienceReservationRequest : ReservationBase
    {
        public ReservationStatus ReservationStatus { get; set; }
    }

    public enum ReservationStatus
    {
        PENDING,
        ACCEPTED,
        REJECTED
    }
}
