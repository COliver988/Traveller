using Microsoft.EntityFrameworkCore;

namespace ShipTracker.Models
{
    public class TravellerDBContext : DbContext
    {
        public DbSet<Ship> Ship { get; set; }
        public DbSet<ShipClass> ShipClass { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
             optionsBuilder.UseSqlite("Data Source=TravellerTracker.db");
        }
    }
}
