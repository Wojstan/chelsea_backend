using System.Collections.Generic;
using System.Linq;
using aiproject.Entities;
using Microsoft.EntityFrameworkCore;

namespace aiproject.Repositories
{
    public class RatingRepository : BaseRepository<RatingEntity, DatabaseContext>
    {
        public RatingRepository(DatabaseContext context) : base(context)
        {
        }


        public IEnumerable<RatingEntity> GetAllRatings()
        {
            return Context.Set<RatingEntity>()
                .Include(rating => rating.AppearanceEntity)
                .ThenInclude(app => app.MatchEntity)
                .Include(rating => rating.AppearanceEntity)
                .ThenInclude(player => player.PlayerEntity)
                .ToList();
        }
        
    }
}