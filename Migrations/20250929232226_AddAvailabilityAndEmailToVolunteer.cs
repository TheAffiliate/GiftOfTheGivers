using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GiftOfTheGivers_ST10239864.Migrations
{
    /// <inheritdoc />
    public partial class AddAvailabilityAndEmailToVolunteer : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Volunteers_AspNetUsers_UserId",
                table: "Volunteers");

            migrationBuilder.DropIndex(
                name: "IX_Volunteers_UserId",
                table: "Volunteers");

            migrationBuilder.DropColumn(
                name: "Date",
                table: "Volunteers");

            migrationBuilder.DropColumn(
                name: "Notes",
                table: "Volunteers");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Volunteers");

            migrationBuilder.RenameColumn(
                name: "Location",
                table: "Volunteers",
                newName: "Email");

            migrationBuilder.AlterColumn<string>(
                name: "Skills",
                table: "Volunteers",
                type: "nvarchar(200)",
                maxLength: 200,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(500)",
                oldMaxLength: 500,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "FullName",
                table: "Volunteers",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddColumn<string>(
                name: "Availability",
                table: "Volunteers",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Availability",
                table: "Volunteers");

            migrationBuilder.RenameColumn(
                name: "Email",
                table: "Volunteers",
                newName: "Location");

            migrationBuilder.AlterColumn<string>(
                name: "Skills",
                table: "Volunteers",
                type: "nvarchar(500)",
                maxLength: 500,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(200)",
                oldMaxLength: 200);

            migrationBuilder.AlterColumn<string>(
                name: "FullName",
                table: "Volunteers",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(100)",
                oldMaxLength: 100);

            migrationBuilder.AddColumn<DateTime>(
                name: "Date",
                table: "Volunteers",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "Notes",
                table: "Volunteers",
                type: "nvarchar(500)",
                maxLength: 500,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UserId",
                table: "Volunteers",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Volunteers_UserId",
                table: "Volunteers",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Volunteers_AspNetUsers_UserId",
                table: "Volunteers",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }
    }
}
