using Microsoft.EntityFrameworkCore.Migrations;

namespace ElectroShop.Migrations
{
    public partial class Update_db_scheme : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_Orders_OrderId",
                schema: "Identity",
                table: "AspNetUsers");

            migrationBuilder.DropForeignKey(
                name: "FK_Categories_Categories_ParentCategoryCategoryId",
                schema: "Identity",
                table: "Categories");

            migrationBuilder.DropForeignKey(
                name: "FK_Products_Orders_OrderModelOrderId",
                schema: "Identity",
                table: "Products");

            migrationBuilder.DropIndex(
                name: "IX_Products_OrderModelOrderId",
                schema: "Identity",
                table: "Products");

            migrationBuilder.DropIndex(
                name: "IX_Categories_ParentCategoryCategoryId",
                schema: "Identity",
                table: "Categories");

            migrationBuilder.DropIndex(
                name: "IX_AspNetUsers_OrderId",
                schema: "Identity",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "OrderModelOrderId",
                schema: "Identity",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "ParentCategoryCategoryId",
                schema: "Identity",
                table: "Categories");

            migrationBuilder.DropColumn(
                name: "OrderId",
                schema: "Identity",
                table: "AspNetUsers");

            migrationBuilder.AddColumn<string>(
                name: "CustomerId",
                schema: "Identity",
                table: "Orders",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ParentCategoryId",
                schema: "Identity",
                table: "Categories",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "OrderDetailModel",
                schema: "Identity",
                columns: table => new
                {
                    OrderDetailId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    OrderId = table.Column<int>(nullable: false),
                    ProductId = table.Column<int>(nullable: false),
                    Quantity = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrderDetailModel", x => x.OrderDetailId);
                    table.ForeignKey(
                        name: "FK_OrderDetailModel_Orders_OrderId",
                        column: x => x.OrderId,
                        principalSchema: "Identity",
                        principalTable: "Orders",
                        principalColumn: "OrderId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_OrderDetailModel_Products_ProductId",
                        column: x => x.ProductId,
                        principalSchema: "Identity",
                        principalTable: "Products",
                        principalColumn: "ProductId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Orders_CustomerId",
                schema: "Identity",
                table: "Orders",
                column: "CustomerId");

            migrationBuilder.CreateIndex(
                name: "IX_Categories_ParentCategoryId",
                schema: "Identity",
                table: "Categories",
                column: "ParentCategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_OrderDetailModel_OrderId",
                schema: "Identity",
                table: "OrderDetailModel",
                column: "OrderId");

            migrationBuilder.CreateIndex(
                name: "IX_OrderDetailModel_ProductId",
                schema: "Identity",
                table: "OrderDetailModel",
                column: "ProductId");

            migrationBuilder.AddForeignKey(
                name: "FK_Categories_Categories_ParentCategoryId",
                schema: "Identity",
                table: "Categories",
                column: "ParentCategoryId",
                principalSchema: "Identity",
                principalTable: "Categories",
                principalColumn: "CategoryId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Orders_AspNetUsers_CustomerId",
                schema: "Identity",
                table: "Orders",
                column: "CustomerId",
                principalSchema: "Identity",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Categories_Categories_ParentCategoryId",
                schema: "Identity",
                table: "Categories");

            migrationBuilder.DropForeignKey(
                name: "FK_Orders_AspNetUsers_CustomerId",
                schema: "Identity",
                table: "Orders");

            migrationBuilder.DropTable(
                name: "OrderDetailModel",
                schema: "Identity");

            migrationBuilder.DropIndex(
                name: "IX_Orders_CustomerId",
                schema: "Identity",
                table: "Orders");

            migrationBuilder.DropIndex(
                name: "IX_Categories_ParentCategoryId",
                schema: "Identity",
                table: "Categories");

            migrationBuilder.DropColumn(
                name: "CustomerId",
                schema: "Identity",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "ParentCategoryId",
                schema: "Identity",
                table: "Categories");

            migrationBuilder.AddColumn<int>(
                name: "OrderModelOrderId",
                schema: "Identity",
                table: "Products",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ParentCategoryCategoryId",
                schema: "Identity",
                table: "Categories",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "OrderId",
                schema: "Identity",
                table: "AspNetUsers",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Products_OrderModelOrderId",
                schema: "Identity",
                table: "Products",
                column: "OrderModelOrderId");

            migrationBuilder.CreateIndex(
                name: "IX_Categories_ParentCategoryCategoryId",
                schema: "Identity",
                table: "Categories",
                column: "ParentCategoryCategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_OrderId",
                schema: "Identity",
                table: "AspNetUsers",
                column: "OrderId");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_Orders_OrderId",
                schema: "Identity",
                table: "AspNetUsers",
                column: "OrderId",
                principalSchema: "Identity",
                principalTable: "Orders",
                principalColumn: "OrderId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Categories_Categories_ParentCategoryCategoryId",
                schema: "Identity",
                table: "Categories",
                column: "ParentCategoryCategoryId",
                principalSchema: "Identity",
                principalTable: "Categories",
                principalColumn: "CategoryId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Products_Orders_OrderModelOrderId",
                schema: "Identity",
                table: "Products",
                column: "OrderModelOrderId",
                principalSchema: "Identity",
                principalTable: "Orders",
                principalColumn: "OrderId",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
