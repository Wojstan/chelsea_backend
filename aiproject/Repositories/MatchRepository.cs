using aiproject.Entities;

namespace aiproject.Repositories
{
    public class MatchRepository : BaseRepository<MatchEntity, DatabaseContext>
    {
        public MatchRepository(DatabaseContext context) : base(context)
        {
        }
    }
}