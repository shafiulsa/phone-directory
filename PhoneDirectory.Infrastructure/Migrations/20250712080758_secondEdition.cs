using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PhoneDirectory.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class secondEdition : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1,
                column: "PasswordHash",
                value: "$2a$11$6SO9Nn0f.AbwTgUskE2eFuMVxFh0gVX.wNReWfITaBFsrNwGT89DS");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1,
                column: "PasswordHash",
                value: "$2a$11$z9FwUiCYYlki3EViF4oPcO4VMQAzuxor0OTLOc.362PFHNm7feAqi");
        }
    }
}
