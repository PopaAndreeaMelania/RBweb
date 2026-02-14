using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RBweb.Migrations
{
    /// <inheritdoc />
    public partial class MeniuCategory : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Meniu_Categorie_CategorieID",
                table: "Meniu");

            migrationBuilder.DropIndex(
                name: "IX_Meniu_CategorieID",
                table: "Meniu");

            migrationBuilder.DropColumn(
                name: "CategorieID",
                table: "Meniu");

            migrationBuilder.CreateTable(
                name: "MeniuCategorie",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MeniuID = table.Column<int>(type: "int", nullable: false),
                    CategorieID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MeniuCategorie", x => x.ID);
                    table.ForeignKey(
                        name: "FK_MeniuCategorie_Categorie_CategorieID",
                        column: x => x.CategorieID,
                        principalTable: "Categorie",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_MeniuCategorie_Meniu_MeniuID",
                        column: x => x.MeniuID,
                        principalTable: "Meniu",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_MeniuCategorie_CategorieID",
                table: "MeniuCategorie",
                column: "CategorieID");

            migrationBuilder.CreateIndex(
                name: "IX_MeniuCategorie_MeniuID",
                table: "MeniuCategorie",
                column: "MeniuID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "MeniuCategorie");

            migrationBuilder.AddColumn<int>(
                name: "CategorieID",
                table: "Meniu",
                type: "int",
                nullable: true);

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
    }
}
