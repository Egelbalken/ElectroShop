using Microsoft.EntityFrameworkCore.Migrations;

namespace ElectroShop.Migrations
{
    public partial class UpdateOnSale : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "OnSaleProduct",
                schema: "Identity",
                table: "Products");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "OnSaleProduct",
                schema: "Identity",
                table: "Products",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }
    }
}
