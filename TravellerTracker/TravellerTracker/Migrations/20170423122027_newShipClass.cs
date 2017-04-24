using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace TravellerTracker.Migrations
{
    public partial class newShipClass : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ShipClassID",
                table: "Ships",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "ShipClasses",
                columns: table => new
                {
                    ShipClassID = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Jump = table.Column<int>(nullable: false),
                    Man = table.Column<int>(nullable: false),
                    Name = table.Column<string>(nullable: true),
                    Power = table.Column<int>(nullable: false),
                    dTons = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ShipClasses", x => x.ShipClassID);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Ships_ShipClassID",
                table: "Ships",
                column: "ShipClassID");

            /* no foreign keys...
            migrationBuilder.AddForeignKey(
                name: "FK_Ships_ShipClasses_ShipClassID",
                table: "Ships",
                column: "ShipClassID",
                principalTable: "ShipClasses",
                principalColumn: "ShipClassID",
                onDelete: ReferentialAction.Cascade);
            */
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Ships_ShipClasses_ShipClassID",
                table: "Ships");

            migrationBuilder.DropTable(
                name: "ShipClasses");

            migrationBuilder.DropIndex(
                name: "IX_Ships_ShipClassID",
                table: "Ships");

            migrationBuilder.DropColumn(
                name: "ShipClassID",
                table: "Ships");
        }
    }
}
