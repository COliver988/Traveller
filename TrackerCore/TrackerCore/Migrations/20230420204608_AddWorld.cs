using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TrackerCore.Migrations
{
    /// <inheritdoc />
    public partial class AddWorld : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "WorldID",
                table: "Ships",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "currentWorld",
                table: "Ships",
                type: "TEXT",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Worlds",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Worlds", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Worlds");

            migrationBuilder.DropColumn(
                name: "WorldID",
                table: "Ships");

            migrationBuilder.DropColumn(
                name: "currentWorld",
                table: "Ships");
        }
    }
}
