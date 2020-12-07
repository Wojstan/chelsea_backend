using System.Collections.Generic;
using System.Linq;
using aiproject.Entities;
using Microsoft.EntityFrameworkCore;

namespace aiproject.Repositories
{
    public class UserRepository : BaseRepository<UserEntity, DatabaseContext>
    {
        public UserRepository(DatabaseContext context) : base(context)
        {
        }

        public IEnumerable<UserEntity> GetAllUsers()
        {
            return Context.Set<UserEntity>().Include(user => user.RoleEntity).ToList();
        }
    }
}