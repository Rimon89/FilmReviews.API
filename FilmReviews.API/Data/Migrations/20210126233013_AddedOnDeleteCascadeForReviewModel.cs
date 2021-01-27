using Microsoft.EntityFrameworkCore.Migrations;

namespace FilmReviews.API.Migrations
{
    public partial class AddedOnDeleteCascadeForReviewModel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Reviews_Movies_ImdbId",
                table: "Reviews");

            migrationBuilder.AlterColumn<string>(
                name: "MovieTitle",
                table: "Reviews",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "ImdbId",
                table: "Reviews",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Reviews_Movies_ImdbId",
                table: "Reviews",
                column: "ImdbId",
                principalTable: "Movies",
                principalColumn: "ImdbID",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Reviews_Movies_ImdbId",
                table: "Reviews");

            migrationBuilder.AlterColumn<string>(
                name: "MovieTitle",
                table: "Reviews",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "ImdbId",
                table: "Reviews",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AddForeignKey(
                name: "FK_Reviews_Movies_ImdbId",
                table: "Reviews",
                column: "ImdbId",
                principalTable: "Movies",
                principalColumn: "ImdbID",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
