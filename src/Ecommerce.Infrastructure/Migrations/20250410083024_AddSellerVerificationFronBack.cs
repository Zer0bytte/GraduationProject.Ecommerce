using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace grad.Ecommerce.Migrations
{
    /// <inheritdoc />
    public partial class AddSellerVerificationFronBack : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "NationalIdImageNameOnServer",
                table: "SellerProfiles",
                newName: "NationalIdFrontNameOnServer");

            migrationBuilder.AddColumn<string>(
                name: "NationalIdBackNameOnServer",
                table: "SellerProfiles",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "NationalIdBackNameOnServer",
                table: "SellerProfiles");

            migrationBuilder.RenameColumn(
                name: "NationalIdFrontNameOnServer",
                table: "SellerProfiles",
                newName: "NationalIdImageNameOnServer");
        }
    }
}
