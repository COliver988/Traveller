using Microsoft.EntityFrameworkCore;

namespace TrackerCore.Models
{
    public class TravellerDBContext : DbContext
    {
        public DbSet<Ship> Ships { get; set; }
        public DbSet<ShipClass> ShipClasses { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            options.UseSqlite("Data Source=TravellerTracker.db");
        }
    }
}
