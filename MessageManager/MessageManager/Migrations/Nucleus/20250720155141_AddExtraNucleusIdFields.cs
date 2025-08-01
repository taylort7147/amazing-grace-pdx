using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MessageManager.Migrations.Nucleus
{
    /// <inheritdoc />
    public partial class AddExtraNucleusIdFields : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "NucleusChurchId",
                table: "Sermons",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "NucleusSermonEngineId",
                table: "Sermons",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "NucleusChurchId",
                table: "Playlists",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "NucleusSermonEngineId",
                table: "Playlists",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "NucleusChurchId",
                table: "Sermons");

            migrationBuilder.DropColumn(
                name: "NucleusSermonEngineId",
                table: "Sermons");

            migrationBuilder.DropColumn(
                name: "NucleusChurchId",
                table: "Playlists");

            migrationBuilder.DropColumn(
                name: "NucleusSermonEngineId",
                table: "Playlists");
        }
    }
}
