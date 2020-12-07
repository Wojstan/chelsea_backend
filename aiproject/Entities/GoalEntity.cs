namespace aiproject.Entities
{
    public class GoalEntity : IEntity
    {
        public int Id { get; set; }
        public int AppearanceId { get; set; }
        
        public AppearanceEntity AppearanceEntity { get; set; }
    }
}