using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using TravellerTracker.Models;

namespace TravellerTracker.Migrations
{
    [DbContext(typeof(TravellerContext))]
    [Migration("20170416155153_initialShip")]
    partial class initialShip
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
            modelBuilder
                .HasAnnotation("ProductVersion", "1.1.1");

            modelBuilder.Entity("Traveller.Models.Ship", b =>
                {
                    b.Property<int>("ShipId")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("CargoCapacity");

                    b.Property<int>("CargoCarried");

                    b.Property<int>("FuelPerJump");

                    b.Property<string>("Name");

                    b.Property<int>("dTons");

                    b.HasKey("ShipId");

                    b.ToTable("Ships");
                });
        }
    }
}
