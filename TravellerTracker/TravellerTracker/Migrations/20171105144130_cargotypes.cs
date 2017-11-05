using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace TravellerTracker.Migrations
{
    public partial class cargotypes : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CargoTypeId",
                table: "Cargo",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "CargoTypes",
                columns: table => new
                {
                    CargoTypeId = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Description = table.Column<string>(nullable: true),
                    Type = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CargoTypes", x => x.CargoTypeId);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CargoTypes");

            migrationBuilder.DropColumn(
                name: "CargoTypeId",
                table: "Cargo");
        }
    }
}
