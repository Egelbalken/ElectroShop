using Microsoft.EntityFrameworkCore.Migrations;

namespace ElectroShop.Migrations
{
    public partial class addreviewandratin : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ProductRatings",
                schema: "Identity",
                columns: table => new
                {
                    ProductRatingId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Rating = table.Column<byte>(type: "TINYINT", nullable: false),
                    ProductId = table.Column<int>(nullable: false),
                    CustomerId = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductRatings", x => x.ProductRatingId);
                    table.ForeignKey(
                        name: "FK_ProductRatings_AspNetUsers_CustomerId",
                        column: x => x.CustomerId,
                        principalSchema: "Identity",
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ProductRatings_Products_ProductId",
                        column: x => x.ProductId,
                        principalSchema: "Identity",
                        principalTable: "Products",
                        principalColumn: "ProductId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ProductReviews",
                schema: "Identity",
                columns: table => new
                {
                    ProductReviewId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(maxLength: 50, nullable: true),
                    Review = table.Column<string>(maxLength: 2000, nullable: true),
                    ProductId = table.Column<int>(nullable: false),
                    CustomerId = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductReviews", x => x.ProductReviewId);
                    table.ForeignKey(
                        name: "FK_ProductReviews_AspNetUsers_CustomerId",
                        column: x => x.CustomerId,
                        principalSchema: "Identity",
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ProductReviews_Products_ProductId",
                        column: x => x.ProductId,
                        principalSchema: "Identity",
                        principalTable: "Products",
                        principalColumn: "ProductId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ProductRatings_CustomerId",
                schema: "Identity",
                table: "ProductRatings",
                column: "CustomerId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductRatings_ProductId",
                schema: "Identity",
                table: "ProductRatings",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductReviews_CustomerId",
                schema: "Identity",
                table: "ProductReviews",
                column: "CustomerId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductReviews_ProductId",
                schema: "Identity",
                table: "ProductReviews",
                column: "ProductId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ProductRatings",
                schema: "Identity");

            migrationBuilder.DropTable(
                name: "ProductReviews",
                schema: "Identity");
        }
    }
}
