using Microsoft.EntityFrameworkCore.Migrations;

namespace ElectroShop.Migrations
{
    public partial class Authorise : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ImageURL",
                schema: "Identity",
                table: "Products",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ImageURL",
                schema: "Identity",
                table: "Products");
        }
    }
}
