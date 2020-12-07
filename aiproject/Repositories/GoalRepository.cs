using System.Collections.Generic;
using System.Linq;
using aiproject.Entities;
using Microsoft.EntityFrameworkCore;

namespace aiproject.Repositories
{
    public class GoalRepository : BaseRepository<GoalEntity, DatabaseContext>
    {
        public GoalRepository(DatabaseContext context) : base(context)
        {
        }

        public IEnumerable<GoalEntity> GetAllGoals()
        {
            return Context.Set<GoalEntity>()
                .Include(goal => goal.AppearanceEntity)
                .ThenInclude(goal => goal.PlayerEntity)
                .ToList();
        }
    }
}