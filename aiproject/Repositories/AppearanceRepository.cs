using System.Collections.Generic;
using System.Linq;
using aiproject.Entities;
using Microsoft.EntityFrameworkCore;

namespace aiproject.Repositories
{
    public class AppearanceRepository : BaseRepository<AppearanceEntity, DatabaseContext>
    {
        public AppearanceRepository(DatabaseContext context) : base(context)
        {
        }

        public IEnumerable<AppearanceEntity> GetAllApps()
        {
            return Context.Set<AppearanceEntity>()
                .Include(app => app.MatchEntity)
                .Include(app => app.PlayerEntity).ToList();
        } 
    }
}