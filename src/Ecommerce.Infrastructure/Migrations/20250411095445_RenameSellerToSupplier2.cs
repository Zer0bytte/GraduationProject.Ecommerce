using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace grad.Ecommerce.Migrations
{
    /// <inheritdoc />
    public partial class RenameSellerToSupplier2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Products_SupplierProfiles_SellerId",
                table: "Products");

            migrationBuilder.RenameColumn(
                name: "SellerId",
                table: "Products",
                newName: "SupplierId");

            migrationBuilder.RenameIndex(
                name: "IX_Products_SellerId",
                table: "Products",
                newName: "IX_Products_SupplierId");

            migrationBuilder.AddForeignKey(
                name: "FK_Products_SupplierProfiles_SupplierId",
                table: "Products",
                column: "SupplierId",
                principalTable: "SupplierProfiles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Products_SupplierProfiles_SupplierId",
                table: "Products");

            migrationBuilder.RenameColumn(
                name: "SupplierId",
                table: "Products",
                newName: "SellerId");

            migrationBuilder.RenameIndex(
                name: "IX_Products_SupplierId",
                table: "Products",
                newName: "IX_Products_SellerId");

            migrationBuilder.AddForeignKey(
                name: "FK_Products_SupplierProfiles_SellerId",
                table: "Products",
                column: "SellerId",
                principalTable: "SupplierProfiles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
