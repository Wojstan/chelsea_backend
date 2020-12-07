using aiproject.Entities;

namespace aiproject.Repositories
{
    public class PlayerRepository : BaseRepository<PlayerEntity, DatabaseContext>
    {
        public PlayerRepository(DatabaseContext context) : base(context)
        {
        }
    }
}