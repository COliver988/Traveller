using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace TravellerTracker.Migrations
{
    public partial class shipLogGeneralizedWorld : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<byte[]>(
                name: "Image",
                table: "Logs",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "WorldID",
                table: "Logs",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Image",
                table: "Logs");

            migrationBuilder.DropColumn(
                name: "WorldID",
                table: "Logs");
        }
    }
}
