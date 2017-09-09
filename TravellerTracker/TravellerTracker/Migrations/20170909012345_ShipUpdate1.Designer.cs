using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using TravellerTracker.Models;

namespace TravellerTracker.Migrations
{
    [DbContext(typeof(TravellerContext))]
    [Migration("20170909012345_ShipUpdate1")]
    partial class ShipUpdate1
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

                    b.Property<int>("Day");

                    b.Property<string>("Era");

                    b.Property<int>("FuelPerJump");

                    b.Property<string>("Name");

                    b.Property<string>("Sector");

                    b.Property<int>("ShipClassID");

                    b.Property<string>("World");

                    b.Property<int>("Year");

                    b.Property<int>("dTons");

                    b.HasKey("ShipId");

                    b.HasIndex("ShipClassID");

                    b.ToTable("Ships");
                });

            modelBuilder.Entity("Traveller.Models.ShipClass", b =>
                {
                    b.Property<int>("ShipClassID")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("Cargo");

                    b.Property<int>("Jump");

                    b.Property<int>("Man");

                    b.Property<string>("Name");

                    b.Property<int>("Power");

                    b.Property<int>("dTons");

                    b.HasKey("ShipClassID");

                    b.ToTable("ShipClasses");
                });

            modelBuilder.Entity("Traveller.Models.ShipLog", b =>
                {
                    b.Property<int>("ShipLogId")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("Day");

                    b.Property<string>("Log");

                    b.Property<int>("ShipId");

                    b.Property<int>("Year");

                    b.HasKey("ShipLogId");

                    b.ToTable("Logs");
                });

            modelBuilder.Entity("TravellerTracker.Models.Cargoes", b =>
                {
                    b.Property<int>("CargoesId")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Cargo");

                    b.Property<string>("CargoCode");

                    b.Property<string>("OriginationSystem");

                    b.Property<int>("PurchasePrice");

                    b.Property<int>("ShipID");

                    b.Property<int>("dTons");

                    b.HasKey("CargoesId");

                    b.ToTable("Cargoes");
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
