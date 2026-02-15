using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RBweb.Migrations
{
    /// <inheritdoc />
    public partial class Add_Mentiuni_To_Comanda : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Mentiuni",
                table: "Comanda",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "ComandaItem",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ComandaID = table.Column<int>(type: "int", nullable: false),
                    MeniuID = table.Column<int>(type: "int", nullable: false),
                    Cantitate = table.Column<int>(type: "int", nullable: false),
                    Pret = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ComandaItem", x => x.ID);
                    table.ForeignKey(
                        name: "FK_ComandaItem_Comanda_ComandaID",
                        column: x => x.ComandaID,
                        principalTable: "Comanda",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ComandaItem_Meniu_MeniuID",
                        column: x => x.MeniuID,
                        principalTable: "Meniu",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ComandaItem_ComandaID",
                table: "ComandaItem",
                column: "ComandaID");

            migrationBuilder.CreateIndex(
                name: "IX_ComandaItem_MeniuID",
                table: "ComandaItem",
                column: "MeniuID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ComandaItem");

            migrationBuilder.DropColumn(
                name: "Mentiuni",
                table: "Comanda");
        }
    }
}
