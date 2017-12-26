using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace TravellerTracker.Migrations
{
    public partial class shipCargoloadedStat : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "DayLoaded",
                table: "ShipCargo",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "DayUnloaded",
                table: "ShipCargo",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "YearLoaded",
                table: "ShipCargo",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "YearUnloaded",
                table: "ShipCargo",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "isActive",
                table: "ShipCargo",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DayLoaded",
                table: "ShipCargo");

            migrationBuilder.DropColumn(
                name: "DayUnloaded",
                table: "ShipCargo");

            migrationBuilder.DropColumn(
                name: "YearLoaded",
                table: "ShipCargo");

            migrationBuilder.DropColumn(
                name: "YearUnloaded",
                table: "ShipCargo");

            migrationBuilder.DropColumn(
                name: "isActive",
                table: "ShipCargo");
        }
    }
}
