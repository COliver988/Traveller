using System.Data.Entity;

namespace ShipTracker.Models
{
    public class TravellerDBContext : DbContext
    {
        public DbSet<Ship> Ship { get; set; }
        public DbSet<ShipClass> ShipClass { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            try
            {
                _ = optionsBuilder.UseSqlite("Data Source=TravellerTracker.db");
            }
            catch (System.Exception ex)
            {
                var a = ex;
                throw;
            }
        }
    }
}
