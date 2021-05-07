using Microsoft.EntityFrameworkCore.Migrations;

namespace MessageManager.Migrations
{
    public partial class AddSeriesDescription : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "Series",
                type: "nvarchar(max)",
                maxLength: 4095,
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Description",
                table: "Series");
        }
    }
}
