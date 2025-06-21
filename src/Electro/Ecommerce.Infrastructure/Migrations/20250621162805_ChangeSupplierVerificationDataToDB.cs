using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace grad.Ecommerce.Migrations
{
    /// <inheritdoc />
    public partial class ChangeSupplierVerificationDataToDB : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<byte[]>(
                name: "NationalIdBackNameOnServer2",
                table: "SupplierProfiles",
                type: "varbinary(max)",
                nullable: true);

            migrationBuilder.AddColumn<byte[]>(
                name: "NationalIdFrontNameOnServer2",
                table: "SupplierProfiles",
                type: "varbinary(max)",
                nullable: true);

            migrationBuilder.AddColumn<byte[]>(
                name: "TaxCardNameOnServer2",
                table: "SupplierProfiles",
                type: "varbinary(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "NationalIdBackNameOnServer2",
                table: "SupplierProfiles");

            migrationBuilder.DropColumn(
                name: "NationalIdFrontNameOnServer2",
                table: "SupplierProfiles");

            migrationBuilder.DropColumn(
                name: "TaxCardNameOnServer2",
                table: "SupplierProfiles");
        }
    }
}
