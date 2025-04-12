using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace grad.Ecommerce.Migrations
{
    /// <inheritdoc />
    public partial class AddMultiSupplierOrderTrack : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Status",
                table: "OrderItems",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<Guid>(
                name: "SupplierId",
                table: "OrderItems",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_OrderItems_SupplierId",
                table: "OrderItems",
                column: "SupplierId");

            migrationBuilder.AddForeignKey(
                name: "FK_OrderItems_SupplierProfiles_SupplierId",
                table: "OrderItems",
                column: "SupplierId",
                principalTable: "SupplierProfiles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_OrderItems_SupplierProfiles_SupplierId",
                table: "OrderItems");

            migrationBuilder.DropIndex(
                name: "IX_OrderItems_SupplierId",
                table: "OrderItems");

            migrationBuilder.DropColumn(
                name: "Status",
                table: "OrderItems");

            migrationBuilder.DropColumn(
                name: "SupplierId",
                table: "OrderItems");
        }
    }
}
