using Microsoft.EntityFrameworkCore.Migrations;

namespace MessageManager.Migrations
{
    public partial class Addplaylisttoseries : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Playlist_SeriesId",
                table: "Playlist");

            migrationBuilder.AddColumn<int>(
                name: "PlaylistId",
                table: "Series",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Playlist_SeriesId",
                table: "Playlist",
                column: "SeriesId",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Playlist_SeriesId",
                table: "Playlist");

            migrationBuilder.DropColumn(
                name: "PlaylistId",
                table: "Series");

            migrationBuilder.CreateIndex(
                name: "IX_Playlist_SeriesId",
                table: "Playlist",
                column: "SeriesId");
        }
    }
}
