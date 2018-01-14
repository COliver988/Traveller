using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace TravellerTracker.Migrations
{
    public partial class imageList : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ImageLists",
                columns: table => new
                {
                    ImageListID = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    ShipID = table.Column<int>(nullable: false),
                    WorldID = table.Column<int>(nullable: false),
                    theImage = table.Column<byte[]>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ImageLists", x => x.ImageListID);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ImageLists");
        }
    }
}
