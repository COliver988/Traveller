using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace TravellerTracker.Migrations
{
    public partial class initialize : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ActualValues",
                columns: table => new
                {
                    ActualValueId = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    DiceRoll = table.Column<int>(nullable: false),
                    PercentageValue = table.Column<int>(nullable: false),
                    TravellerVersionId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ActualValues", x => x.ActualValueId);
                });

            migrationBuilder.CreateTable(
                name: "Cargo",
                columns: table => new
                {
                    CargoID = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    BasePurchasePrice = table.Column<int>(nullable: false),
                    CargoCode = table.Column<string>(nullable: true),
                    CargoTypeId = table.Column<int>(nullable: false),
                    D1 = table.Column<int>(nullable: false),
                    D2 = table.Column<int>(nullable: false),
                    Description = table.Column<string>(nullable: true),
                    IsSingleUnits = table.Column<bool>(nullable: true),
                    Multiplier = table.Column<int>(nullable: false),
                    PurchaseDMs = table.Column<string>(nullable: true),
                    QtyDie = table.Column<int>(nullable: false),
                    ResaleDMs = table.Column<string>(nullable: true),
                    TravellerVersionId = table.Column<int>(nullable: false),
                    dTons = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cargo", x => x.CargoID);
                });

            migrationBuilder.CreateTable(
                name: "CargoTypes",
                columns: table => new
                {
                    CargoTypeId = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Description = table.Column<string>(nullable: true),
                    Type = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CargoTypes", x => x.CargoTypeId);
                });

            migrationBuilder.CreateTable(
                name: "Sectors",
                columns: table => new
                {
                    SectorID = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Milieu = table.Column<string>(nullable: true),
                    Name = table.Column<string>(nullable: true),
                    Tags = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Sectors", x => x.SectorID);
                });

            migrationBuilder.CreateTable(
                name: "Ships",
                columns: table => new
                {
                    ShipId = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    CargoCarried = table.Column<int>(nullable: false),
                    Credits = table.Column<int>(nullable: false),
                    Day = table.Column<int>(nullable: false),
                    Era = table.Column<string>(nullable: true),
                    Fuel = table.Column<int>(nullable: false),
                    HighPaxCarried = table.Column<int>(nullable: false),
                    LowPaxCarried = table.Column<int>(nullable: false),
                    MidPaxCarried = table.Column<int>(nullable: false),
                    Name = table.Column<string>(nullable: true),
                    SectorID = table.Column<int>(nullable: false),
                    ShipClassID = table.Column<int>(nullable: false),
                    TravellerVersionID = table.Column<int>(nullable: false),
                    WorldID = table.Column<int>(nullable: false),
                    Year = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Ships", x => x.ShipId);
                });

            migrationBuilder.CreateTable(
                name: "ShipCargo",
                columns: table => new
                {
                    ShipCargoID = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    CargoCode = table.Column<string>(nullable: true),
                    CargoID = table.Column<int>(nullable: false),
                    CargoType = table.Column<int>(nullable: false),
                    DayLoaded = table.Column<int>(nullable: false),
                    DayUnloaded = table.Column<int>(nullable: false),
                    DestinationID = table.Column<int>(nullable: false),
                    OriginWorldID = table.Column<int>(nullable: false),
                    PurchasePrice = table.Column<int>(nullable: false),
                    ResellPrice = table.Column<int>(nullable: false),
                    ShipID = table.Column<int>(nullable: false),
                    YearLoaded = table.Column<int>(nullable: false),
                    YearUnloaded = table.Column<int>(nullable: false),
                    dTons = table.Column<int>(nullable: false),
                    isActive = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ShipCargo", x => x.ShipCargoID);
                });

            migrationBuilder.CreateTable(
                name: "ShipClasses",
                columns: table => new
                {
                    ShipClassID = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Cargo = table.Column<int>(nullable: false),
                    Fuel = table.Column<int>(nullable: false),
                    FuelPerParsec = table.Column<int>(nullable: false),
                    HGClass = table.Column<string>(nullable: true),
                    HighPassage = table.Column<int>(nullable: false),
                    Jump = table.Column<int>(nullable: false),
                    LowPassage = table.Column<int>(nullable: false),
                    Man = table.Column<int>(nullable: false),
                    MidPassage = table.Column<int>(nullable: false),
                    Name = table.Column<string>(nullable: true),
                    Power = table.Column<int>(nullable: false),
                    WeeksEndurance = table.Column<int>(nullable: false),
                    dTons = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ShipClasses", x => x.ShipClassID);
                });

            migrationBuilder.CreateTable(
                name: "Logs",
                columns: table => new
                {
                    ShipLogId = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Day = table.Column<int>(nullable: false),
                    Image = table.Column<byte[]>(nullable: true),
                    Log = table.Column<string>(nullable: true),
                    ShipId = table.Column<int>(nullable: false),
                    WorldID = table.Column<int>(nullable: false),
                    Year = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Logs", x => x.ShipLogId);
                });

            migrationBuilder.CreateTable(
                name: "Starports",
                columns: table => new
                {
                    StarportId = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Class = table.Column<char>(nullable: false),
                    Downport = table.Column<string>(nullable: true),
                    Quality = table.Column<string>(nullable: true),
                    Repairs = table.Column<string>(nullable: true),
                    Yards = table.Column<string>(nullable: true),
                    hasRefinedFuel = table.Column<bool>(nullable: false),
                    hasUnrefinedFuel = table.Column<bool>(nullable: false),
                    isStarport = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Starports", x => x.StarportId);
                });

            migrationBuilder.CreateTable(
                name: "TradeClassifications",
                columns: table => new
                {
                    TradeClassificationID = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Atmospheres = table.Column<string>(nullable: true),
                    Classification = table.Column<string>(nullable: true),
                    Description = table.Column<string>(nullable: true),
                    Gov = table.Column<string>(nullable: true),
                    Hydro = table.Column<string>(nullable: true),
                    Law = table.Column<string>(nullable: true),
                    Name = table.Column<string>(nullable: true),
                    Pop = table.Column<string>(nullable: true),
                    Sizes = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TradeClassifications", x => x.TradeClassificationID);
                });

            migrationBuilder.CreateTable(
                name: "TravellerVersions",
                columns: table => new
                {
                    TravellerVersionId = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    CargoCodeType = table.Column<int>(nullable: false),
                    Cost1HighPax = table.Column<int>(nullable: false),
                    Cost1Jump = table.Column<int>(nullable: false),
                    Cost1LowPax = table.Column<int>(nullable: false),
                    Cost1MidPax = table.Column<int>(nullable: false),
                    Cost2HighPax = table.Column<int>(nullable: false),
                    Cost2Jump = table.Column<int>(nullable: false),
                    Cost2LowPax = table.Column<int>(nullable: false),
                    Cost2MidPax = table.Column<int>(nullable: false),
                    Cost3HighPax = table.Column<int>(nullable: false),
                    Cost3Jump = table.Column<int>(nullable: false),
                    Cost3LowPax = table.Column<int>(nullable: false),
                    Cost3MidPax = table.Column<int>(nullable: false),
                    Cost4HighPax = table.Column<int>(nullable: false),
                    Cost4Jump = table.Column<int>(nullable: false),
                    Cost4LowPax = table.Column<int>(nullable: false),
                    Cost4MidPax = table.Column<int>(nullable: false),
                    Cost5HighPax = table.Column<int>(nullable: false),
                    Cost5Jump = table.Column<int>(nullable: false),
                    Cost5LowPax = table.Column<int>(nullable: false),
                    Cost5MidPax = table.Column<int>(nullable: false),
                    Cost6HighPax = table.Column<int>(nullable: false),
                    Cost6Jump = table.Column<int>(nullable: false),
                    Cost6LowPax = table.Column<int>(nullable: false),
                    Cost6MidPax = table.Column<int>(nullable: false),
                    D1TopRange = table.Column<int>(nullable: false),
                    D2TopRange = table.Column<int>(nullable: false),
                    DaysForCargoSearch = table.Column<int>(nullable: false),
                    Name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TravellerVersions", x => x.TravellerVersionId);
                });

            migrationBuilder.CreateTable(
                name: "Worlds",
                columns: table => new
                {
                    WorldID = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Alliance = table.Column<string>(nullable: true),
                    Bases = table.Column<string>(nullable: true),
                    CulturalExt = table.Column<string>(nullable: true),
                    Ex = table.Column<string>(nullable: true),
                    Hex = table.Column<string>(nullable: true),
                    Importance = table.Column<string>(nullable: true),
                    Name = table.Column<string>(nullable: true),
                    PBG = table.Column<string>(nullable: true),
                    Remarks = table.Column<string>(nullable: true),
                    SectorID = table.Column<int>(nullable: false),
                    Stellar = table.Column<string>(nullable: true),
                    UWP = table.Column<string>(nullable: true),
                    WorldCount = table.Column<int>(nullable: false),
                    WorldImage = table.Column<byte[]>(nullable: true),
                    Zone = table.Column<char>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Worlds", x => x.WorldID);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ActualValues");

            migrationBuilder.DropTable(
                name: "Cargo");

            migrationBuilder.DropTable(
                name: "CargoTypes");

            migrationBuilder.DropTable(
                name: "Sectors");

            migrationBuilder.DropTable(
                name: "Ships");

            migrationBuilder.DropTable(
                name: "ShipCargo");

            migrationBuilder.DropTable(
                name: "ShipClasses");

            migrationBuilder.DropTable(
                name: "Logs");

            migrationBuilder.DropTable(
                name: "Starports");

            migrationBuilder.DropTable(
                name: "TradeClassifications");

            migrationBuilder.DropTable(
                name: "TravellerVersions");

            migrationBuilder.DropTable(
                name: "Worlds");
        }
    }
}
