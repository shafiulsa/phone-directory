using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PhoneDirectory.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class init : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1,
                column: "PasswordHash",
                value: "$2a$11$z9FwUiCYYlki3EViF4oPcO4VMQAzuxor0OTLOc.362PFHNm7feAqi");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1,
                column: "PasswordHash",
                value: "$2a$11$vIGPOYV8N3pN.78.SnpW7OBD3n6p3xVHfT83a/v6DTUM.agwWw/Ma");
        }
    }
}
