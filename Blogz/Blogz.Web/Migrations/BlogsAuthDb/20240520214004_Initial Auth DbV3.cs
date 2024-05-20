using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Blogz.Web.Migrations.BlogsAuthDb
{
    /// <inheritdoc />
    public partial class InitialAuthDbV3 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("dea533e1-bb48-46a9-94b5-b8e44c2bde4f"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "850a7937-34c6-4fd0-b1fc-fbef52eb41e9", "AQAAAAIAAYagAAAAEFfM86HNsm0v9DV5RLMonpTSmRMsfhWdVCO6ANvEZqvoFgAdD2kstvnWOtGHWu/8OA==", "dea533e1-bb48-46a9-94b5-b8e44c2bde4f" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("dea533e1-bb48-46a9-94b5-b8e44c2bde4f"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "3652c7c8-b9e9-426b-8356-267e8f38f0c6", "AQAAAAIAAYagAAAAEMCKclmHmCC7RFkrYYyv0dOq0uX36CLj42ASsmU6wHv9nrPV1pjgFAds1/3HxHRJrw==", null });
        }
    }
}
