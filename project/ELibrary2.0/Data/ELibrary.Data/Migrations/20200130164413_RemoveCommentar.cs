using Microsoft.EntityFrameworkCore.Migrations;

namespace ELibrary.Data.Migrations
{
    public partial class RemoveCommentar : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Commentar",
                table: "Books");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Commentar",
                table: "Books",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
