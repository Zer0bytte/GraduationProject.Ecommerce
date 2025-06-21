using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace grad.Ecommerce.Migrations
{
    /// <inheritdoc />
    public partial class RenameDataToOriginal : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "TaxCardNameOnServer2",
                table: "SupplierProfiles",
                newName: "TaxCardNameOnServer");

            migrationBuilder.RenameColumn(
                name: "NationalIdFrontNameOnServer2",
                table: "SupplierProfiles",
                newName: "NationalIdFrontNameOnServer");

            migrationBuilder.RenameColumn(
                name: "NationalIdBackNameOnServer2",
                table: "SupplierProfiles",
                newName: "NationalIdBackNameOnServer");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "TaxCardNameOnServer",
                table: "SupplierProfiles",
                newName: "TaxCardNameOnServer2");

            migrationBuilder.RenameColumn(
                name: "NationalIdFrontNameOnServer",
                table: "SupplierProfiles",
                newName: "NationalIdFrontNameOnServer2");

            migrationBuilder.RenameColumn(
                name: "NationalIdBackNameOnServer",
                table: "SupplierProfiles",
                newName: "NationalIdBackNameOnServer2");
        }
    }
}
