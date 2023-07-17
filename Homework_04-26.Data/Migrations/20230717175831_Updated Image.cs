using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Homework_04_26.Data.Migrations
{
    public partial class UpdatedImage : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Title",
                table: "Images",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Title",
                table: "Images");
        }
    }
}
