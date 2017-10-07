using Microsoft.EntityFrameworkCore;
using Traveller.Models;

namespace TravellerTracker.Models
{
    public class TravellerContext : DbContext
    {
        public DbSet<Ship> Ships { get; set; }
        public DbSet<ShipClass> ShipClasses { get; set; }
        public DbSet<Cargoes> Cargoes { get; set; }
        public DbSet<ShipLog> Logs { get; set; }
        public DbSet<World> Worlds { get; set; }
        public DbSet<Sector> Sectors { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            try
            {
                optionsBuilder.UseSqlite("Data Source=TravellerTracker.db");
            }
            catch (System.Exception ex)
            {
                var a = ex;
                throw;
            }
        }
    }
}
