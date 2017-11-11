using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace TravellerTracker.Migrations
{
    public partial class cargoVersions : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "CargoCode",
                table: "ShipCargo",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "OriginWorldID",
                table: "ShipCargo",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "dTons",
                table: "ShipCargo",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "D1",
                table: "Cargo",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "D2",
                table: "Cargo",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "Desciption",
                table: "Cargo",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Multiplier",
                table: "Cargo",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "QtyDie",
                table: "Cargo",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "TravellerVersionId",
                table: "Cargo",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CargoCode",
                table: "ShipCargo");

            migrationBuilder.DropColumn(
                name: "OriginWorldID",
                table: "ShipCargo");

            migrationBuilder.DropColumn(
                name: "dTons",
                table: "ShipCargo");

            migrationBuilder.DropColumn(
                name: "D1",
                table: "Cargo");

            migrationBuilder.DropColumn(
                name: "D2",
                table: "Cargo");

            migrationBuilder.DropColumn(
                name: "Desciption",
                table: "Cargo");

            migrationBuilder.DropColumn(
                name: "Multiplier",
                table: "Cargo");

            migrationBuilder.DropColumn(
                name: "QtyDie",
                table: "Cargo");

            migrationBuilder.DropColumn(
                name: "TravellerVersionId",
                table: "Cargo");
        }
    }
}
