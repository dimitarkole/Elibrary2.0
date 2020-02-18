using Microsoft.EntityFrameworkCore.Migrations;

namespace ELibrary.Data.Migrations
{
    public partial class AddUserColAtReviewBook : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "UserId",
                table: "BookReviews",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_BookReviews_UserId",
                table: "BookReviews",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_BookReviews_AspNetUsers_UserId",
                table: "BookReviews",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BookReviews_AspNetUsers_UserId",
                table: "BookReviews");

            migrationBuilder.DropIndex(
                name: "IX_BookReviews_UserId",
                table: "BookReviews");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "BookReviews");
        }
    }
}
