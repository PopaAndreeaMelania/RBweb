using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RBweb.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Meniu",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Denumire = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Categorie = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Pret = table.Column<decimal>(type: "decimal(6,2)", nullable: false),
                    DataAdaugare = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Meniu", x => x.ID);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Meniu");
        }
    }
}
