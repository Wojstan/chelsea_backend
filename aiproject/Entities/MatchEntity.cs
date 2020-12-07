using System.Collections.Generic;

namespace aiproject.Entities
{
    public class MatchEntity : IEntity
    {
        public MatchEntity(int id)
        {
            Id = id;
        }

        public int Id { get; set; }

        public List<AppearanceEntity> Appearances { get; set; } = new List<AppearanceEntity>();
    }
}