using Microsoft.EntityFrameworkCore.Migrations;

namespace MessageManager.Migrations
{
    public partial class CreateSeparateTableForBibleReferences : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "BibleReferences",
                table: "Message");

            migrationBuilder.CreateTable(
                name: "BibleReferences",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    StartBook = table.Column<int>(type: "int", nullable: false),
                    StartChapter = table.Column<int>(type: "int", nullable: false),
                    StartVerse = table.Column<int>(type: "int", nullable: false),
                    EndBook = table.Column<int>(type: "int", nullable: false),
                    EndChapter = table.Column<int>(type: "int", nullable: false),
                    EndVerse = table.Column<int>(type: "int", nullable: false),
                    MessageId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BibleReferences", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BibleReferences_Message_MessageId",
                        column: x => x.MessageId,
                        principalTable: "Message",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_BibleReferences_MessageId",
                table: "BibleReferences",
                column: "MessageId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BibleReferences");

            migrationBuilder.AddColumn<string>(
                name: "BibleReferences",
                table: "Message",
                type: "nvarchar(512)",
                maxLength: 512,
                nullable: true);
        }
    }
}
