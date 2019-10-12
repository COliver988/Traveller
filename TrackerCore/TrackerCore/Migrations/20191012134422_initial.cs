using Microsoft.EntityFrameworkCore.Migrations;

namespace TrackerCore.Migrations
{
    public partial class initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ShipClasses",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    ClassName = table.Column<string>(nullable: true),
                    Tonnage = table.Column<int>(nullable: false),
                    Classification = table.Column<string>(nullable: true),
                    HighPassengers = table.Column<int>(nullable: false),
                    MidPassengers = table.Column<int>(nullable: false),
                    LowPassengers = table.Column<int>(nullable: false),
                    Fuel = table.Column<int>(nullable: false),
                    Cargo = table.Column<int>(nullable: false),
                    Jump = table.Column<int>(nullable: false),
                    Maneuver = table.Column<int>(nullable: false),
                    Power = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ShipClasses", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "Ships",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    ShipClassID = table.Column<int>(nullable: false),
                    Name = table.Column<string>(nullable: true),
                    currentCargo = table.Column<int>(nullable: false),
                    currentFuel = table.Column<int>(nullable: false),
                    currentHighPassengers = table.Column<int>(nullable: false),
                    currentMidPassengers = table.Column<int>(nullable: false),
                    currentLowPassengers = table.Column<int>(nullable: false),
                    currentCredits = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Ships", x => x.id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ShipClasses");

            migrationBuilder.DropTable(
                name: "Ships");
        }
    }
}
