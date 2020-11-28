using aiproject.Entities;
using Microsoft.EntityFrameworkCore;

namespace aiproject
{
    public class DatabaseContext : DbContext
    {
        public DatabaseContext(DbContextOptions<DatabaseContext> options) : base(options)
        {
        }
        
        //public DbSet<WeatherForecast> WeatherForecasts { get; set; }

        public DbSet<UserEntity> Users { get; set; }
        
        public DbSet<MatchEntity> Matches { get; set; }
        
        //public DbSet<Role> Roles { get; set; }

        //public DbSet<UserRole> UserRoles { get; set; }
    }
}