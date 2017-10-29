using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace TravellerTracker.Migrations
{
    public partial class tradeCodes : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
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
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TradeClassifications");
        }
    }
}
