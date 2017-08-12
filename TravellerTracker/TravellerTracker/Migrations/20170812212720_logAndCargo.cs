using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace TravellerTracker.Migrations
{
    public partial class logAndCargo : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Cargoes",
                columns: table => new
                {
                    CargoesId = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Cargo = table.Column<string>(nullable: true),
                    CargoCode = table.Column<string>(nullable: true),
                    OriginationSystem = table.Column<string>(nullable: true),
                    PurchasePrice = table.Column<int>(nullable: false),
                    ShipID = table.Column<int>(nullable: false),
                    dTons = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cargoes", x => x.CargoesId);
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
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Cargoes");

            migrationBuilder.DropTable(
                name: "Logs");
        }
    }
}
