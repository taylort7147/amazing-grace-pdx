using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Editor.Migrations
{
    public partial class VideoChangeDateTimeToInt : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "MessageStartTime",
                table: "Video",
                nullable: false,
                oldClrType: typeof(DateTime));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "MessageStartTime",
                table: "Video",
                nullable: false,
                oldClrType: typeof(int));
        }
    }
}
