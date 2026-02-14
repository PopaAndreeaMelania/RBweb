using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RBweb.Migrations
{
    public partial class Comenzi : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Client",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FirstName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Address = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Phone = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Client", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Comanda",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ClientID = table.Column<int>(type: "int", nullable: true),
                    MeniuID = table.Column<int>(type: "int", nullable: true),
                    DataLivrare = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Comanda", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Comanda_Client_ClientID",
                        column: x => x.ClientID,
                        principalTable: "Client",
                        principalColumn: "ID");
                    table.ForeignKey(
                        name: "FK_Comanda_Meniu_MeniuID",
                        column: x => x.MeniuID,
                        principalTable: "Meniu",
                        principalColumn: "ID");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Comanda_ClientID",
                table: "Comanda",
                column: "ClientID");

            migrationBuilder.CreateIndex(
                name: "IX_Comanda_MeniuID",
                table: "Comanda",
                column: "MeniuID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(name: "Comanda");
            migrationBuilder.DropTable(name: "Client");
        }
    }
}
