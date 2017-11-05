using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace TravellerTracker.Migrations
{
    public partial class travellerVersion : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "TravellerVersionID",
                table: "Ships",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "TravellerVersions",
                columns: table => new
                {
                    TravellerVersionId = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TravellerVersions", x => x.TravellerVersionId);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TravellerVersions");

            migrationBuilder.DropColumn(
                name: "TravellerVersionID",
                table: "Ships");
        }
    }
}
