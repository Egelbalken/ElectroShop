using Microsoft.EntityFrameworkCore.Migrations;

namespace ElectroShop.Migrations
{
    public partial class AddedReceipt : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ReceiptId",
                schema: "Identity",
                table: "Orders",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Receipts",
                schema: "Identity",
                columns: table => new
                {
                    ReceiptId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ReceiptFirstName = table.Column<string>(maxLength: 125, nullable: false),
                    ReceiptLastName = table.Column<string>(maxLength: 125, nullable: false),
                    ReceiptEmailAddress = table.Column<string>(maxLength: 125, nullable: false),
                    ReceiptCity = table.Column<string>(nullable: false),
                    ReceiptCountry = table.Column<string>(maxLength: 125, nullable: false),
                    ReceiptStreetAddress = table.Column<string>(maxLength: 125, nullable: false),
                    ReceiptZipCode = table.Column<string>(maxLength: 10, nullable: false),
                    ReceiptState = table.Column<string>(maxLength: 125, nullable: false),
                    ReceiptPhoneNumber = table.Column<string>(maxLength: 12, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Receipts", x => x.ReceiptId);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Orders_ReceiptId",
                schema: "Identity",
                table: "Orders",
                column: "ReceiptId");

            migrationBuilder.AddForeignKey(
                name: "FK_Orders_Receipts_ReceiptId",
                schema: "Identity",
                table: "Orders",
                column: "ReceiptId",
                principalSchema: "Identity",
                principalTable: "Receipts",
                principalColumn: "ReceiptId",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Orders_Receipts_ReceiptId",
                schema: "Identity",
                table: "Orders");

            migrationBuilder.DropTable(
                name: "Receipts",
                schema: "Identity");

            migrationBuilder.DropIndex(
                name: "IX_Orders_ReceiptId",
                schema: "Identity",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "ReceiptId",
                schema: "Identity",
                table: "Orders");
        }
    }
}
