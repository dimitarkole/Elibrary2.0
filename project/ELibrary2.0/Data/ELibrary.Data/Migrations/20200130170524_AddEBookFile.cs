using Microsoft.EntityFrameworkCore.Migrations;

namespace ELibrary.Data.Migrations
{
    public partial class AddEBookFile : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "EBookFile",
                table: "Books",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "EBookFile",
                table: "Books");
        }
    }
}
