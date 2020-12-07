using System.Collections;
using aiproject.Entities;

namespace aiproject.Repositories
{
    public class RoleRepository : BaseRepository<RoleEntity, DatabaseContext>
    {
        public RoleRepository(DatabaseContext context) : base(context)
        {
        }
    }
}