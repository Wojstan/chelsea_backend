using aiproject.Entities;
using Microsoft.EntityFrameworkCore;

namespace aiproject
{
    public class DatabaseContext : DbContext
    {
        public DatabaseContext(DbContextOptions<DatabaseContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<GoalEntity>()
                .HasOne(p => p.AppearanceEntity)
                .WithMany(b => b.Goals)
                .OnDelete(DeleteBehavior.Cascade);
            
            modelBuilder.Entity<RatingEntity>()
                .HasOne(p => p.AppearanceEntity)
                .WithMany(b => b.Ratings)
                .OnDelete(DeleteBehavior.Cascade);
        }

        public DbSet<UserEntity> Users { get; set; }
        
        public DbSet<MatchEntity> Matches { get; set; }

        public DbSet<AppearanceEntity> Appearances { get; set; }

        public DbSet<PlayerEntity> Players { get; set; }

        public DbSet<GoalEntity> Goals { get; set; }

        public DbSet<RatingEntity> Ratings { get; set; }

        public DbSet<RoleEntity> Roles { get; set; }
        
        public DbSet<TicketEntity> Tickets { get; set; }

    }
}