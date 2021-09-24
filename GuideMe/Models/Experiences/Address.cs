using GuideMe.Utils;

namespace GuideMe.Models.Experiences
{
    public class Address
    {
        public string Street { get; set; }
        public string StreetNo { get; set; }
        public string City { get; set; }
        public string Country { get; set; }
        public string ZipCode { get; set; }
        public Coordinate Coordinates { get; set; }
    }
}
