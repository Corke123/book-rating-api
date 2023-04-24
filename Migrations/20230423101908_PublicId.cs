using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace book_rating_api.Migrations
{
    /// <inheritdoc />
    public partial class PublicId : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Actors_Books_BookId",
                table: "Actors");

            migrationBuilder.AddColumn<string>(
                name: "CoverImagePublicId",
                table: "Books",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_Books_CategoryId",
                table: "Books",
                column: "CategoryId");

            migrationBuilder.AddForeignKey(
                name: "FK_Actors_Books_BookId",
                table: "Actors",
                column: "BookId",
                principalTable: "Books",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Books_Categories_CategoryId",
                table: "Books",
                column: "CategoryId",
                principalTable: "Categories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Actors_Books_BookId",
                table: "Actors");

            migrationBuilder.DropForeignKey(
                name: "FK_Books_Categories_CategoryId",
                table: "Books");

            migrationBuilder.DropIndex(
                name: "IX_Books_CategoryId",
                table: "Books");

            migrationBuilder.DropColumn(
                name: "CoverImagePublicId",
                table: "Books");

            migrationBuilder.AddForeignKey(
                name: "FK_Actors_Books_BookId",
                table: "Actors",
                column: "BookId",
                principalTable: "Books",
                principalColumn: "Id");
        }
    }
}
