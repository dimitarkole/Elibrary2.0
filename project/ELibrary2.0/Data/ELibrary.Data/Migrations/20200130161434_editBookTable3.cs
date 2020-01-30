using Microsoft.EntityFrameworkCore.Migrations;

namespace ELibrary.Data.Migrations
{
    public partial class editBookTable3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Currencys",
                table: "Books");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Currencys",
                table: "Books",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
