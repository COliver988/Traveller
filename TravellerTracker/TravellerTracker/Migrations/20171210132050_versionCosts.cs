using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace TravellerTracker.Migrations
{
    public partial class versionCosts : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Cost1Jump",
                table: "TravellerVersions",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Cost2Jump",
                table: "TravellerVersions",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Cost3Jump",
                table: "TravellerVersions",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Cost4Jump",
                table: "TravellerVersions",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Cost5Jump",
                table: "TravellerVersions",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Cost6Jump",
                table: "TravellerVersions",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "HighPassageCost",
                table: "TravellerVersions",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "LowPassageCost",
                table: "TravellerVersions",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "MidPassageCost",
                table: "TravellerVersions",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Cost1Jump",
                table: "TravellerVersions");

            migrationBuilder.DropColumn(
                name: "Cost2Jump",
                table: "TravellerVersions");

            migrationBuilder.DropColumn(
                name: "Cost3Jump",
                table: "TravellerVersions");

            migrationBuilder.DropColumn(
                name: "Cost4Jump",
                table: "TravellerVersions");

            migrationBuilder.DropColumn(
                name: "Cost5Jump",
                table: "TravellerVersions");

            migrationBuilder.DropColumn(
                name: "Cost6Jump",
                table: "TravellerVersions");

            migrationBuilder.DropColumn(
                name: "HighPassageCost",
                table: "TravellerVersions");

            migrationBuilder.DropColumn(
                name: "LowPassageCost",
                table: "TravellerVersions");

            migrationBuilder.DropColumn(
                name: "MidPassageCost",
                table: "TravellerVersions");
        }
    }
}
