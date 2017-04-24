using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using TravellerTracker.Models;

namespace TravellerTracker.Migrations
{
    [DbContext(typeof(TravellerContext))]
    [Migration("20170423122027_newShipClass")]
    partial class newShipClass
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

                    b.Property<int>("ShipClassID");

                    b.Property<int>("dTons");

                    b.HasKey("ShipId");

                    b.HasIndex("ShipClassID");

                    b.ToTable("Ships");
                });

            modelBuilder.Entity("Traveller.Models.ShipClass", b =>
                {
                    b.Property<int>("ShipClassID")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("Jump");

                    b.Property<int>("Man");

                    b.Property<string>("Name");

                    b.Property<int>("Power");

                    b.Property<int>("dTons");

                    b.HasKey("ShipClassID");

                    b.ToTable("ShipClasses");
                });

            modelBuilder.Entity("Traveller.Models.Ship", b =>
                {
                    b.HasOne("Traveller.Models.ShipClass", "Class")
                        .WithMany()
                        .HasForeignKey("ShipClassID")
                        .OnDelete(DeleteBehavior.Cascade);
                });
        }
    }
}
