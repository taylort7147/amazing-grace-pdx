using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MessageManager.Migrations.Nucleus
{
    /// <inheritdoc />
    public partial class MakeNucleusMessageManagerIdsUnique : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_Sermons_MessageId",
                table: "Sermons",
                column: "MessageId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Playlists_SeriesId",
                table: "Playlists",
                column: "SeriesId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Notes_NotesId",
                table: "Notes",
                column: "NotesId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Audio_AudioId",
                table: "Audio",
                column: "AudioId",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Sermons_MessageId",
                table: "Sermons");

            migrationBuilder.DropIndex(
                name: "IX_Playlists_SeriesId",
                table: "Playlists");

            migrationBuilder.DropIndex(
                name: "IX_Notes_NotesId",
                table: "Notes");

            migrationBuilder.DropIndex(
                name: "IX_Audio_AudioId",
                table: "Audio");
        }
    }
}
