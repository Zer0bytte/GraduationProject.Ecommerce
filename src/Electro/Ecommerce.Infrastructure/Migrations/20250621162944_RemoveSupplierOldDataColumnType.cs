using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace grad.Ecommerce.Migrations
{
    /// <inheritdoc />
    public partial class RemoveSupplierOldDataColumnType : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "NationalIdBackNameOnServer",
                table: "SupplierProfiles");

            migrationBuilder.DropColumn(
                name: "NationalIdFrontNameOnServer",
                table: "SupplierProfiles");

            migrationBuilder.DropColumn(
                name: "TaxCardNameOnServer",
                table: "SupplierProfiles");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "NationalIdBackNameOnServer",
                table: "SupplierProfiles",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "NationalIdFrontNameOnServer",
                table: "SupplierProfiles",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "TaxCardNameOnServer",
                table: "SupplierProfiles",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }
    }
}
