using aiproject.Repositories;

namespace aiproject.Entities
{
    public class MatchEntity : IEntity
    {
        public MatchEntity(int id, int apiId)
        {
            Id = id;
            ApiId = apiId;
        }

        public int Id { get; set; }
        public int ApiId { get; set; }
    }
}