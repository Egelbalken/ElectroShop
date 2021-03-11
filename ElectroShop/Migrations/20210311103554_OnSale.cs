using Microsoft.EntityFrameworkCore.Migrations;

namespace ElectroShop.Migrations
{
    public partial class OnSale : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ShoppingCartItems_Products_ProductId",
                schema: "Identity",
                table: "ShoppingCartItems");

            migrationBuilder.AlterColumn<int>(
                name: "ProductId",
                schema: "Identity",
                table: "ShoppingCartItems",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<double>(
                name: "OnSalePrice",
                schema: "Identity",
                table: "Products",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<bool>(
                name: "OnSaleProduct",
                schema: "Identity",
                table: "Products",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddForeignKey(
                name: "FK_ShoppingCartItems_Products_ProductId",
                schema: "Identity",
                table: "ShoppingCartItems",
                column: "ProductId",
                principalSchema: "Identity",
                principalTable: "Products",
                principalColumn: "ProductId",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ShoppingCartItems_Products_ProductId",
                schema: "Identity",
                table: "ShoppingCartItems");

            migrationBuilder.DropColumn(
                name: "OnSalePrice",
                schema: "Identity",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "OnSaleProduct",
                schema: "Identity",
                table: "Products");

            migrationBuilder.AlterColumn<int>(
                name: "ProductId",
                schema: "Identity",
                table: "ShoppingCartItems",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_ShoppingCartItems_Products_ProductId",
                schema: "Identity",
                table: "ShoppingCartItems",
                column: "ProductId",
                principalSchema: "Identity",
                principalTable: "Products",
                principalColumn: "ProductId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
