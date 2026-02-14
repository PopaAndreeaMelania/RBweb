using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RBweb.Migrations
{
    /// <inheritdoc />
    public partial class AddCategorie : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Categorie",
                table: "Meniu");

            migrationBuilder.AddColumn<int>(
                name: "CategorieID",
                table: "Meniu",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Categorie",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CategoryName = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Categorie", x => x.ID);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Meniu_CategorieID",
                table: "Meniu",
                column: "CategorieID");

            migrationBuilder.AddForeignKey(
                name: "FK_Meniu_Categorie_CategorieID",
                table: "Meniu",
                column: "CategorieID",
                principalTable: "Categorie",
                principalColumn: "ID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Meniu_Categorie_CategorieID",
                table: "Meniu");

            migrationBuilder.DropTable(
                name: "Categorie");

            migrationBuilder.DropIndex(
                name: "IX_Meniu_CategorieID",
                table: "Meniu");

            migrationBuilder.DropColumn(
                name: "CategorieID",
                table: "Meniu");

            migrationBuilder.AddColumn<string>(
                name: "Categorie",
                table: "Meniu",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }
    }
}
