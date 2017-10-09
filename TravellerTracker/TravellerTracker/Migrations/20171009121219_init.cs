using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace TravellerTracker.Migrations
{
    public partial class init : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
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
                    Name = table.Column<string>(nullable: true),
                    SectorID = table.Column<int>(nullable: false),
                    ShipClassID = table.Column<int>(nullable: false),
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
                    CargoID = table.Column<int>(nullable: false),
                    ShipID = table.Column<int>(nullable: false)
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
                    HGClass = table.Column<string>(nullable: true),
                    Jump = table.Column<int>(nullable: false),
                    Man = table.Column<int>(nullable: false),
                    Name = table.Column<string>(nullable: true),
                    Power = table.Column<int>(nullable: false),
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
                    Log = table.Column<string>(nullable: true),
                    ShipId = table.Column<int>(nullable: false),
                    Year = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Logs", x => x.ShipLogId);
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
                    Zone = table.Column<char>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Worlds", x => x.WorldID);
                });

            migrationBuilder.CreateTable(
                name: "Cargo",
                columns: table => new
                {
                    CargoID = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    BasePurchasePrice = table.Column<int>(nullable: false),
                    CargoCode = table.Column<string>(nullable: true),
                    dTons = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cargo", x => x.CargoID);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
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
                name: "Worlds");

            migrationBuilder.DropTable(
                name: "Cargo");
        }
    }
}
