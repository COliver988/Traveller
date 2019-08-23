﻿using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShipTracker.Models
{
    public class TravellerDBContext : DbContext
    {
        public DbSet<Ships> Ship { get; set; }
        public DbSet<ShipClasses> ShipClass { get; set; }

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
