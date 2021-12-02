namespace GuideMe.Models.Account
{
    public class Review
    {
        public string UserId { get; set; } = "";
        public string UserName { get; set; } = "";
        public string RatingComment { get; set; } = "";
        public float RatingValue { get; set; } = 0;
        public string ProfilePhotoUrl { get; set; } = "";
    }
}
