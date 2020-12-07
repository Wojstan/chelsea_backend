using System.Collections.Generic;

namespace aiproject.Entities
{
    public class AppearanceEntity : IEntity
    {
        public int Id { get; set; }
        public int MatchId { get; set; }
        public int PlayerId { get; set; }

        public MatchEntity MatchEntity { get; set; }
        public PlayerEntity PlayerEntity { get; set; }

        public List<GoalEntity> Goals { get; set; } = new List<GoalEntity>();
        public List<RatingEntity> Ratings { get; set; } = new List<RatingEntity>();
    }
}