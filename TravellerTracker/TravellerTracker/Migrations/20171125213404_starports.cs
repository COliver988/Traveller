using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace TravellerTracker.Migrations
{
    public partial class starports : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
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
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Starports");
        }
    }
}
