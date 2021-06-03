using Microsoft.EntityFrameworkCore.Migrations;

namespace MessageManager.Migrations
{
    public partial class Movebiblereferencestoseparatetable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BibleReferenceRange_Message_MessageId",
                table: "BibleReferenceRange");

            migrationBuilder.DropPrimaryKey(
                name: "PK_BibleReferenceRange",
                table: "BibleReferenceRange");

            migrationBuilder.RenameTable(
                name: "BibleReferenceRange",
                newName: "BibleReferences");

            migrationBuilder.RenameIndex(
                name: "IX_BibleReferenceRange_MessageId",
                table: "BibleReferences",
                newName: "IX_BibleReferences_MessageId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_BibleReferences",
                table: "BibleReferences",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_BibleReferences_Message_MessageId",
                table: "BibleReferences",
                column: "MessageId",
                principalTable: "Message",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BibleReferences_Message_MessageId",
                table: "BibleReferences");

            migrationBuilder.DropPrimaryKey(
                name: "PK_BibleReferences",
                table: "BibleReferences");

            migrationBuilder.RenameTable(
                name: "BibleReferences",
                newName: "BibleReferenceRange");

            migrationBuilder.RenameIndex(
                name: "IX_BibleReferences_MessageId",
                table: "BibleReferenceRange",
                newName: "IX_BibleReferenceRange_MessageId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_BibleReferenceRange",
                table: "BibleReferenceRange",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_BibleReferenceRange_Message_MessageId",
                table: "BibleReferenceRange",
                column: "MessageId",
                principalTable: "Message",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
