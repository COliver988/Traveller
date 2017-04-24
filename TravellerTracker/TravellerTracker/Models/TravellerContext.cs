using Microsoft.EntityFrameworkCore;
using Traveller.Models;

namespace TravellerTracker.Models
{
    public class TravellerContext : DbContext
    {
        public DbSet<Ship> Ships { get; set; }
        public DbSet<ShipClass> ShipClasses { get; set; }

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
