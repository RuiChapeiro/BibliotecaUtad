using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BibliotecaUtad.Migrations
{
    /// <inheritdoc />
    public partial class UpdateFKConstraints : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Book_Gender_GenderId",
                table: "Book");

            migrationBuilder.DropForeignKey(
                name: "FK_Book_Subgender_SubGenderId",
                table: "Book");

            migrationBuilder.AddForeignKey(
                name: "FK_Book_Gender_GenderId",
                table: "Book",
                column: "GenderId",
                principalTable: "Gender",
                principalColumn: "GenderId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Book_Subgender_SubGenderId",
                table: "Book",
                column: "SubGenderId",
                principalTable: "Subgender",
                principalColumn: "SubGenderId",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Book_Gender_GenderId",
                table: "Book");

            migrationBuilder.DropForeignKey(
                name: "FK_Book_Subgender_SubGenderId",
                table: "Book");

            migrationBuilder.AddForeignKey(
                name: "FK_Book_Gender_GenderId",
                table: "Book",
                column: "GenderId",
                principalTable: "Gender",
                principalColumn: "GenderId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Book_Subgender_SubGenderId",
                table: "Book",
                column: "SubGenderId",
                principalTable: "Subgender",
                principalColumn: "SubGenderId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
