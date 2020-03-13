using Microsoft.EntityFrameworkCore.Migrations;

namespace ELibrary.Data.Migrations
{
    public partial class RenameCol : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PriceThoYears",
                table: "PaymentPlans");

            migrationBuilder.AddColumn<double>(
                name: "PriceTwoYears",
                table: "PaymentPlans",
                nullable: false,
                defaultValue: 0.0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PriceTwoYears",
                table: "PaymentPlans");

            migrationBuilder.AddColumn<double>(
                name: "PriceThoYears",
                table: "PaymentPlans",
                type: "float",
                nullable: false,
                defaultValue: 0.0);
        }
    }
}
