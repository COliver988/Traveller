using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace TravellerTracker.Migrations
{
    public partial class T5TradeCodeMatrix : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "TradeClassEffects",
                columns: table => new
                {
                    TradeClassEffectsID = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Adjustment = table.Column<int>(nullable: false),
                    Destination = table.Column<string>(nullable: true),
                    Source = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TradeClassEffects", x => x.TradeClassEffectsID);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TradeClassEffects");
        }
    }
}
