using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TrackerCore.Migrations
{
    /// <inheritdoc />
    public partial class AddWorldDatav1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Hex",
                table: "Worlds",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "Worlds",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UWP",
                table: "Worlds",
                type: "TEXT",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Hex",
                table: "Worlds");

            migrationBuilder.DropColumn(
                name: "Name",
                table: "Worlds");

            migrationBuilder.DropColumn(
                name: "UWP",
                table: "Worlds");
        }
    }
}
