using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace TravellerTracker.Migrations
{
    public partial class cargoDMs : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "PurchaseDMs",
                table: "Cargo",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ResaleDMs",
                table: "Cargo",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PurchaseDMs",
                table: "Cargo");

            migrationBuilder.DropColumn(
                name: "ResaleDMs",
                table: "Cargo");
        }
    }
}
