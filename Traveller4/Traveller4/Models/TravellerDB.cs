using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace Traveller4.Models
{
    public class TravellerDB : DbContext
    {
        public DbSet<Ship> Ships { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite("Filename=TravellerDB.db");
        }
    }
}
