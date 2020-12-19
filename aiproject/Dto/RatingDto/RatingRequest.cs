namespace aiproject.Dto
{
    public class RatingRequest
    {
        public int Value { get; set; }
        public int AppearanceId { get; set; }
    }

    public class ModifyRatingRequest
    {
        public int Value { get; set; }
    }
}