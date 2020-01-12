using Microsoft.EntityFrameworkCore.Migrations;

namespace MyVet.Web.Migrations
{
    public partial class AddingField : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "OtherField",
                table: "Owners",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "OtherField",
                table: "Owners");
        }
    }
}
