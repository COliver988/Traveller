using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace TravellerTracker.Migrations
{
    public partial class Location : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Sector",
                table: "Ships",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "World",
                table: "Ships",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Sector",
                table: "Ships");

            migrationBuilder.DropColumn(
                name: "World",
                table: "Ships");
        }
    }
}
