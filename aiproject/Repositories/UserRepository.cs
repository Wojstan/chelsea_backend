using aiproject.Entities;

namespace aiproject.Repositories
{
    public class UserRepository : BaseRepository<UserEntity, DatabaseContext>
    {
        public UserRepository(DatabaseContext context) : base(context)
        {
        }
    }

}