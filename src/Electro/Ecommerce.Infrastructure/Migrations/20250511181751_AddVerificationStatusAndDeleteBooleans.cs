using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace grad.Ecommerce.Migrations
{
    /// <inheritdoc />
    public partial class AddVerificationStatusAndDeleteBooleans : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsBanned",
                table: "SupplierProfiles");

            migrationBuilder.DropColumn(
                name: "IsRejected",
                table: "SupplierProfiles");

            migrationBuilder.DropColumn(
                name: "IsVerified",
                table: "SupplierProfiles");

            migrationBuilder.AddColumn<string>(
                name: "VerificationStatus",
                table: "SupplierProfiles",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "VerificationStatus",
                table: "SupplierProfiles");

            migrationBuilder.AddColumn<bool>(
                name: "IsBanned",
                table: "SupplierProfiles",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsRejected",
                table: "SupplierProfiles",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsVerified",
                table: "SupplierProfiles",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }
    }
}
