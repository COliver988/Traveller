using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace TravellerTracker.Migrations
{
    public partial class WorldSectorAndCredits : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Credits",
                table: "Ships",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "WorldDataWorldID",
                table: "Ships",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "World",
                columns: table => new
                {
                    WorldID = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Alliance = table.Column<string>(nullable: true),
                    CulturalExt = table.Column<string>(nullable: true),
                    Ex = table.Column<string>(nullable: true),
                    Hex = table.Column<string>(nullable: true),
                    Importance = table.Column<string>(nullable: true),
                    Name = table.Column<string>(nullable: true),
                    PBG = table.Column<string>(nullable: true),
                    Remarks = table.Column<string>(nullable: true),
                    SectorID = table.Column<int>(nullable: false),
                    Stellar = table.Column<string>(nullable: true),
                    UWP = table.Column<string>(nullable: true),
                    WorldCount = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_World", x => x.WorldID);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Ships_WorldDataWorldID",
                table: "Ships",
                column: "WorldDataWorldID");

            /*  no FK added after the fact
            migrationBuilder.AddForeignKey(
                name: "FK_Ships_World_WorldDataWorldID",
                table: "Ships",
                column: "WorldDataWorldID",
                principalTable: "World",
                principalColumn: "WorldID",
                onDelete: ReferentialAction.Restrict);
             */
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            /*
            migrationBuilder.DropForeignKey(
                name: "FK_Ships_World_WorldDataWorldID",
                table: "Ships");
            */

            migrationBuilder.DropTable(
                name: "World");

            migrationBuilder.DropIndex(
                name: "IX_Ships_WorldDataWorldID",
                table: "Ships");

            migrationBuilder.DropColumn(
                name: "Credits",
                table: "Ships");

            migrationBuilder.DropColumn(
                name: "WorldDataWorldID",
                table: "Ships");
        }
    }
}
