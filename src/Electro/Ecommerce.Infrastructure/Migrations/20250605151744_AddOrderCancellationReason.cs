﻿using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace grad.Ecommerce.Migrations
{
    /// <inheritdoc />
    public partial class AddOrderCancellationReason : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "CancellationReason",
                table: "Orders",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CancellationReason",
                table: "Orders");
        }
    }
}
