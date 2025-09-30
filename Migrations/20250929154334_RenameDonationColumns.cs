using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GiftOfTheGivers_ST10239864.Migrations
{
    /// <inheritdoc />
    public partial class RenameDonationColumns : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ResourceType",
                table: "Donations",
                newName: "Type");

            migrationBuilder.RenameColumn(
                name: "DonatedAt",
                table: "Donations",
                newName: "DateDonated");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Type",
                table: "Donations",
                newName: "ResourceType");

            migrationBuilder.RenameColumn(
                name: "DateDonated",
                table: "Donations",
                newName: "DonatedAt");
        }
    }
}
