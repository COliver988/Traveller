using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using TravellerTracker.Models;
using Traveller.Models;

namespace TravellerTracker.Migrations
{
    [DbContext(typeof(TravellerContext))]
    [Migration("20180409170931_tradeCodeEffects")]
    partial class tradeCodeEffects
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
            modelBuilder
                .HasAnnotation("ProductVersion", "1.1.1");

            modelBuilder.Entity("Traveller.Models.ActualValue", b =>
                {
                    b.Property<int>("ActualValueId")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("DiceRoll");

                    b.Property<int>("PercentageValue");

                    b.Property<int>("TravellerVersionId");

                    b.HasKey("ActualValueId");

                    b.ToTable("ActualValues");
                });

            modelBuilder.Entity("Traveller.Models.Cargo", b =>
                {
                    b.Property<int>("CargoID")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("BasePurchasePrice");

                    b.Property<string>("CargoCode");

                    b.Property<int>("CargoTypeId");

                    b.Property<int>("D1");

                    b.Property<int>("D2");

                    b.Property<string>("Description");

                    b.Property<bool?>("IsSingleUnits");

                    b.Property<int>("Multiplier");

                    b.Property<string>("PurchaseDMs");

                    b.Property<int>("QtyDie");

                    b.Property<string>("ResaleDMs");

                    b.Property<int>("TravellerVersionId");

                    b.Property<int>("dTons");

                    b.HasKey("CargoID");

                    b.ToTable("Cargo");
                });

            modelBuilder.Entity("Traveller.Models.CargoType", b =>
                {
                    b.Property<int>("CargoTypeId")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Description");

                    b.Property<string>("Type");

                    b.HasKey("CargoTypeId");

                    b.ToTable("CargoTypes");
                });

            modelBuilder.Entity("Traveller.Models.Sector", b =>
                {
                    b.Property<int>("SectorID")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Milieu");

                    b.Property<string>("Name");

                    b.Property<string>("Tags");

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

                    b.Property<int>("Fuel");

                    b.Property<int>("HighPaxCarried");

                    b.Property<int>("LowPaxCarried");

                    b.Property<int>("MidPaxCarried");

                    b.Property<string>("Name");

                    b.Property<string>("Notes");

                    b.Property<int>("SectorID");

                    b.Property<int>("ShipClassID");

                    b.Property<int>("TravellerVersionID");

                    b.Property<int>("WorldID");

                    b.Property<int>("Year");

                    b.HasKey("ShipId");

                    b.ToTable("Ships");
                });

            modelBuilder.Entity("Traveller.Models.ShipCargo", b =>
                {
                    b.Property<int>("ShipCargoID")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("CargoCode");

                    b.Property<int>("CargoID");

                    b.Property<int>("CargoType");

                    b.Property<int>("DayLoaded");

                    b.Property<int>("DayUnloaded");

                    b.Property<int>("DestinationID");

                    b.Property<int>("OriginWorldID");

                    b.Property<int>("PurchasePrice");

                    b.Property<int>("ResellPrice");

                    b.Property<int>("ShipID");

                    b.Property<int>("YearLoaded");

                    b.Property<int>("YearUnloaded");

                    b.Property<int>("dTons");

                    b.Property<int>("isActive");

                    b.HasKey("ShipCargoID");

                    b.ToTable("ShipCargo");
                });

            modelBuilder.Entity("Traveller.Models.ShipClass", b =>
                {
                    b.Property<int>("ShipClassID")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("Cargo");

                    b.Property<int>("Fuel");

                    b.Property<int>("FuelPerParsec");

                    b.Property<string>("HGClass");

                    b.Property<int>("HighPassage");

                    b.Property<int>("Jump");

                    b.Property<int>("LowPassage");

                    b.Property<int>("Man");

                    b.Property<int>("MidPassage");

                    b.Property<string>("Name");

                    b.Property<int>("Power");

                    b.Property<int>("WeeksEndurance");

                    b.Property<int>("dTons");

                    b.HasKey("ShipClassID");

                    b.ToTable("ShipClasses");
                });

            modelBuilder.Entity("Traveller.Models.ShipLog", b =>
                {
                    b.Property<int>("ShipLogId")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("Day");

                    b.Property<byte[]>("Image");

                    b.Property<string>("Log");

                    b.Property<int>("ShipId");

                    b.Property<int>("WorldID");

                    b.Property<int>("Year");

                    b.HasKey("ShipLogId");

                    b.ToTable("Logs");
                });

            modelBuilder.Entity("Traveller.Models.Starport", b =>
                {
                    b.Property<int>("StarportId")
                        .ValueGeneratedOnAdd();

                    b.Property<char>("Class");

                    b.Property<string>("Downport");

                    b.Property<string>("Quality");

                    b.Property<string>("Repairs");

                    b.Property<string>("Yards");

                    b.Property<bool>("hasRefinedFuel");

                    b.Property<bool>("hasUnrefinedFuel");

                    b.Property<bool>("isStarport");

                    b.HasKey("StarportId");

                    b.ToTable("Starports");
                });

            modelBuilder.Entity("Traveller.Models.TradeClassification", b =>
                {
                    b.Property<int>("TradeClassificationID")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Atmospheres")
                        .IsRequired();

                    b.Property<int>("BuyingAdjustment");

                    b.Property<string>("Classification")
                        .IsRequired();

                    b.Property<string>("Description");

                    b.Property<string>("Gov")
                        .IsRequired();

                    b.Property<string>("Hydro")
                        .IsRequired();

                    b.Property<bool>("IsManuallyAssigned");

                    b.Property<string>("Law")
                        .IsRequired();

                    b.Property<string>("Name")
                        .IsRequired();

                    b.Property<string>("Pop")
                        .IsRequired();

                    b.Property<string>("Sizes")
                        .IsRequired();

                    b.HasKey("TradeClassificationID");

                    b.ToTable("TradeClassifications");
                });

            modelBuilder.Entity("Traveller.Models.TradeGood", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("CargoTypeID");

                    b.Property<string>("Description");

                    b.Property<string>("TradeCode");

                    b.HasKey("ID");

                    b.ToTable("TradeGoods");
                });

            modelBuilder.Entity("Traveller.Models.TravellerVersion", b =>
                {
                    b.Property<int>("TravellerVersionId")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("CargoCodeType");

                    b.Property<int>("Cost1HighPax");

                    b.Property<int>("Cost1Jump");

                    b.Property<int>("Cost1LowPax");

                    b.Property<int>("Cost1MidPax");

                    b.Property<int>("Cost2HighPax");

                    b.Property<int>("Cost2Jump");

                    b.Property<int>("Cost2LowPax");

                    b.Property<int>("Cost2MidPax");

                    b.Property<int>("Cost3HighPax");

                    b.Property<int>("Cost3Jump");

                    b.Property<int>("Cost3LowPax");

                    b.Property<int>("Cost3MidPax");

                    b.Property<int>("Cost4HighPax");

                    b.Property<int>("Cost4Jump");

                    b.Property<int>("Cost4LowPax");

                    b.Property<int>("Cost4MidPax");

                    b.Property<int>("Cost5HighPax");

                    b.Property<int>("Cost5Jump");

                    b.Property<int>("Cost5LowPax");

                    b.Property<int>("Cost5MidPax");

                    b.Property<int>("Cost6HighPax");

                    b.Property<int>("Cost6Jump");

                    b.Property<int>("Cost6LowPax");

                    b.Property<int>("Cost6MidPax");

                    b.Property<int>("D1TopRange");

                    b.Property<int>("D2TopRange");

                    b.Property<int>("DaysForCargoSearch");

                    b.Property<string>("Name");

                    b.HasKey("TravellerVersionId");

                    b.ToTable("TravellerVersions");
                });

            modelBuilder.Entity("Traveller.Models.World", b =>
                {
                    b.Property<int>("WorldID")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Alliance");

                    b.Property<string>("Bases");

                    b.Property<string>("CulturalExt");

                    b.Property<string>("Ex");

                    b.Property<string>("Hex");

                    b.Property<string>("Importance");

                    b.Property<string>("Name");

                    b.Property<string>("Notes");

                    b.Property<string>("PBG");

                    b.Property<string>("Remarks");

                    b.Property<int>("SectorID");

                    b.Property<string>("Stellar");

                    b.Property<string>("UWP");

                    b.Property<int>("WorldCount");

                    b.Property<byte[]>("WorldImage");

                    b.Property<char>("Zone");

                    b.HasKey("WorldID");

                    b.ToTable("Worlds");
                });

            modelBuilder.Entity("Traveller.Models.WorldTC", b =>
                {
                    b.Property<int>("WorldTCID")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("TradeClassificationID");

                    b.Property<int>("WorldID");

                    b.HasKey("WorldTCID");

                    b.ToTable("WorldTCs");
                });

            modelBuilder.Entity("TravellerTracker.Models.ImageList", b =>
                {
                    b.Property<int>("ImageListID")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Description");

                    b.Property<int>("ShipID");

                    b.Property<int>("WorldID");

                    b.Property<byte[]>("theImage");

                    b.HasKey("ImageListID");

                    b.ToTable("ImageLists");
                });
        }
    }
}
