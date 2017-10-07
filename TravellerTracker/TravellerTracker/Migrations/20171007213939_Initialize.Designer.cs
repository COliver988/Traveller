using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using TravellerTracker.Models;

namespace TravellerTracker.Migrations
{
    [DbContext(typeof(TravellerContext))]
    [Migration("20171007213939_Initialize")]
    partial class Initialize
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
            modelBuilder
                .HasAnnotation("ProductVersion", "1.1.1");

            modelBuilder.Entity("Traveller.Models.Sector", b =>
                {
                    b.Property<int>("SectorID")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Milieu");

                    b.Property<string>("Name");

                    b.HasKey("SectorID");

                    b.ToTable("Sectors");
                });

            modelBuilder.Entity("Traveller.Models.Ship", b =>
                {
                    b.Property<int>("ShipId")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("CargoCarried");

                    b.Property<int>("Credits");

                    b.Property<int>("Day");

                    b.Property<string>("Era");

                    b.Property<string>("Name");

                    b.Property<int>("SectorID");

                    b.Property<int>("ShipClassID");

                    b.Property<int>("WorldID");

                    b.Property<int>("Year");

                    b.HasKey("ShipId");

                    b.ToTable("Ships");
                });

            modelBuilder.Entity("Traveller.Models.ShipClass", b =>
                {
                    b.Property<int>("ShipClassID")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("Cargo");

                    b.Property<string>("HGClass");

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

            modelBuilder.Entity("Traveller.Models.World", b =>
                {
                    b.Property<int>("WorldID")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Alliance");

                    b.Property<string>("CulturalExt");

                    b.Property<string>("Ex");

                    b.Property<string>("Hex");

                    b.Property<string>("Importance");

                    b.Property<string>("Name");

                    b.Property<string>("PBG");

                    b.Property<string>("Remarks");

                    b.Property<int>("SectorID");

                    b.Property<string>("Stellar");

                    b.Property<string>("UWP");

                    b.Property<int>("WorldCount");

                    b.HasKey("WorldID");

                    b.ToTable("Worlds");
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
        }
    }
}
