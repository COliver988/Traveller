using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace TravellerTracker.Migrations
{
    public partial class worldExdpansion : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Bases",
                table: "Worlds",
                nullable: true);

            migrationBuilder.AddColumn<char>(
                name: "Zone",
                table: "Worlds",
                nullable: false,
                defaultValue: ' ');
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Bases",
                table: "Worlds");

            migrationBuilder.DropColumn(
                name: "Zone",
                table: "Worlds");
        }
    }
}
