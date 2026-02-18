using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RBweb.Migrations
{
    /// <inheritdoc />
    public partial class AddStatusComanda : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Status",
                table: "Comanda",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Status",
                table: "Comanda");
        }
    }
}
