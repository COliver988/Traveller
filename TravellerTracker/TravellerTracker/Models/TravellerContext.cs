using Microsoft.EntityFrameworkCore;
using Traveller.Models;

namespace TravellerTracker.Models
{
    public class TravellerContext : DbContext
    {
        public DbSet<Ship> Ships { get; set; }
        public DbSet<ShipClass> ShipClasses { get; set; }
        public DbSet<Cargo> Cargo { get; set; }
        public DbSet<ShipLog> Logs { get; set; }
        public DbSet<ShipCargo> ShipCargo { get; set; }
        public DbSet<World> Worlds { get; set; }
        public DbSet<Sector> Sectors { get; set; }
        public DbSet<TradeClassification> TradeClassifications { get; set; }
        public DbSet<TravellerVersion> TravellerVersions { get; set; }

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
