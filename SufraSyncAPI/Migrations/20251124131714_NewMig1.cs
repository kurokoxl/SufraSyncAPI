using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SufraSyncAPI.Migrations
{
    /// <inheritdoc />
    public partial class NewMig1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "bd5865a2-27c2-4a38-a203-392833302a59",
                column: "ConcurrencyStamp",
                value: "2");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "c7b013f0-5201-4317-abd8-c211f91b7330",
                column: "ConcurrencyStamp",
                value: "1");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "bd5865a2-27c2-4a38-a203-392833302a59",
                column: "ConcurrencyStamp",
                value: "7c983185-95fd-4a84-b6b4-fed3503cbe65");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "c7b013f0-5201-4317-abd8-c211f91b7330",
                column: "ConcurrencyStamp",
                value: "2916fe52-3f08-4450-9161-cef2a890cd30");
        }
    }
}
