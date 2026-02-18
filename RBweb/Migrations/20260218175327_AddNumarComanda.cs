using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RBweb.Migrations
{
    
    public partial class AddNumarComanda : Migration
    {
      
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "NumarComanda",
                table: "Comanda",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

       
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "NumarComanda",
                table: "Comanda");
        }
    }
}
