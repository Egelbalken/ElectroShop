using Microsoft.EntityFrameworkCore.Migrations;

namespace ElectroShop.Migrations
{
    public partial class add_rating_to_reviews : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "RatingProductRatingId",
                schema: "Identity",
                table: "ProductReviews",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_ProductReviews_RatingProductRatingId",
                schema: "Identity",
                table: "ProductReviews",
                column: "RatingProductRatingId");

            migrationBuilder.AddForeignKey(
                name: "FK_ProductReviews_ProductRatings_RatingProductRatingId",
                schema: "Identity",
                table: "ProductReviews",
                column: "RatingProductRatingId",
                principalSchema: "Identity",
                principalTable: "ProductRatings",
                principalColumn: "ProductRatingId",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProductReviews_ProductRatings_RatingProductRatingId",
                schema: "Identity",
                table: "ProductReviews");

            migrationBuilder.DropIndex(
                name: "IX_ProductReviews_RatingProductRatingId",
                schema: "Identity",
                table: "ProductReviews");

            migrationBuilder.DropColumn(
                name: "RatingProductRatingId",
                schema: "Identity",
                table: "ProductReviews");
        }
    }
}
