using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace TravellerTracker.Migrations
{
    public partial class shipPassengers : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "HighPaxCarried",
                table: "Ships",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "LowPaxCarried",
                table: "Ships",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "MidPaxCarried",
                table: "Ships",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "HighPaxCarried",
                table: "Ships");

            migrationBuilder.DropColumn(
                name: "LowPaxCarried",
                table: "Ships");

            migrationBuilder.DropColumn(
                name: "MidPaxCarried",
                table: "Ships");
        }
    }
}
