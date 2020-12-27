using Microsoft.EntityFrameworkCore.Migrations;

namespace FilmReviews.API.Migrations
{
    public partial class ModifiedMovieEntity : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Reviews_Movies_MovieId",
                table: "Reviews");

            migrationBuilder.RenameColumn(
                name: "MovieId",
                table: "Reviews",
                newName: "ImdbId");

            migrationBuilder.RenameIndex(
                name: "IX_Reviews_MovieId",
                table: "Reviews",
                newName: "IX_Reviews_ImdbId");

            migrationBuilder.RenameColumn(
                name: "imdbVotes",
                table: "Movies",
                newName: "ImdbVotes");

            migrationBuilder.RenameColumn(
                name: "imdbRating",
                table: "Movies",
                newName: "ImdbRating");

            migrationBuilder.RenameColumn(
                name: "imdbID",
                table: "Movies",
                newName: "ImdbID");

            migrationBuilder.AddColumn<string>(
                name: "MovieTitle",
                table: "Reviews",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Reviews_Movies_ImdbId",
                table: "Reviews",
                column: "ImdbId",
                principalTable: "Movies",
                principalColumn: "ImdbID",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Reviews_Movies_ImdbId",
                table: "Reviews");

            migrationBuilder.DropColumn(
                name: "MovieTitle",
                table: "Reviews");

            migrationBuilder.RenameColumn(
                name: "ImdbId",
                table: "Reviews",
                newName: "MovieId");

            migrationBuilder.RenameIndex(
                name: "IX_Reviews_ImdbId",
                table: "Reviews",
                newName: "IX_Reviews_MovieId");

            migrationBuilder.RenameColumn(
                name: "ImdbVotes",
                table: "Movies",
                newName: "imdbVotes");

            migrationBuilder.RenameColumn(
                name: "ImdbRating",
                table: "Movies",
                newName: "imdbRating");

            migrationBuilder.RenameColumn(
                name: "ImdbID",
                table: "Movies",
                newName: "imdbID");

            migrationBuilder.AddForeignKey(
                name: "FK_Reviews_Movies_MovieId",
                table: "Reviews",
                column: "MovieId",
                principalTable: "Movies",
                principalColumn: "imdbID",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
