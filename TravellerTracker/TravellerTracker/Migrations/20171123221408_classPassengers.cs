using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace TravellerTracker.Migrations
{
    public partial class classPassengers : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "HighPassage",
                table: "ShipClasses",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "LowPassage",
                table: "ShipClasses",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "MidPassage",
                table: "ShipClasses",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "HighPassage",
                table: "ShipClasses");

            migrationBuilder.DropColumn(
                name: "LowPassage",
                table: "ShipClasses");

            migrationBuilder.DropColumn(
                name: "MidPassage",
                table: "ShipClasses");
        }
    }
}
