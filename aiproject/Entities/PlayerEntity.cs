using System.Collections.Generic;

namespace aiproject.Entities
{
    public class PlayerEntity : IEntity
    {
        public int Id { get; set; }
        public int Number { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public int Position { get; set; }
        public int Apps { get; set; } = 0;
        public int Goals { get; set; } = 0;
        public double Rating { get; set; } = 0;
        public string Img { get; set; }
        
        public List<AppearanceEntity> Appearances = new List<AppearanceEntity>();
    }
}