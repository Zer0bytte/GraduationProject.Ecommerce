using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace grad.Ecommerce.Migrations
{
    /// <inheritdoc />
    public partial class RenameAuctionBids : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AuctionBid_AspNetUsers_UserId",
                table: "AuctionBid");

            migrationBuilder.DropForeignKey(
                name: "FK_AuctionBid_Products_ProductId",
                table: "AuctionBid");

            migrationBuilder.DropPrimaryKey(
                name: "PK_AuctionBid",
                table: "AuctionBid");

            migrationBuilder.RenameTable(
                name: "AuctionBid",
                newName: "AuctionBids");

            migrationBuilder.RenameIndex(
                name: "IX_AuctionBid_UserId",
                table: "AuctionBids",
                newName: "IX_AuctionBids_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_AuctionBid_ProductId",
                table: "AuctionBids",
                newName: "IX_AuctionBids_ProductId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_AuctionBids",
                table: "AuctionBids",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_AuctionBids_AspNetUsers_UserId",
                table: "AuctionBids",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_AuctionBids_Products_ProductId",
                table: "AuctionBids",
                column: "ProductId",
                principalTable: "Products",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AuctionBids_AspNetUsers_UserId",
                table: "AuctionBids");

            migrationBuilder.DropForeignKey(
                name: "FK_AuctionBids_Products_ProductId",
                table: "AuctionBids");

            migrationBuilder.DropPrimaryKey(
                name: "PK_AuctionBids",
                table: "AuctionBids");

            migrationBuilder.RenameTable(
                name: "AuctionBids",
                newName: "AuctionBid");

            migrationBuilder.RenameIndex(
                name: "IX_AuctionBids_UserId",
                table: "AuctionBid",
                newName: "IX_AuctionBid_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_AuctionBids_ProductId",
                table: "AuctionBid",
                newName: "IX_AuctionBid_ProductId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_AuctionBid",
                table: "AuctionBid",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_AuctionBid_AspNetUsers_UserId",
                table: "AuctionBid",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_AuctionBid_Products_ProductId",
                table: "AuctionBid",
                column: "ProductId",
                principalTable: "Products",
                principalColumn: "Id");
        }
    }
}
