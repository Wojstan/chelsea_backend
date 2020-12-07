namespace aiproject.Entities
{
    public class RatingEntity : IEntity
    {
        public int Id { get; set; }
        public int Value { get; set; }
        public int AppearanceId { get; set; }
        public int UserId { get; set; }
        
        public AppearanceEntity AppearanceEntity { get; set; }
        public UserEntity UserEntity { get; set; }
    }
}