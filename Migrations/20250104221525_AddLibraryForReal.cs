using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BibliotecaUtad.Migrations
{
    /// <inheritdoc />
    public partial class AddLibraryForReal : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Librabry",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LocAddress = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LocPostalCode = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LocCity = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LocCountry = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Phone = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    OpeningHours = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Librabry", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Librabry");
        }
    }
}
