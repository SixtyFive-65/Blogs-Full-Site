using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Blogz.Web.Migrations.BlogsAuthDb
{
    /// <inheritdoc />
    public partial class first : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("dea533e1-bb48-46a9-94b5-b8e44c2bde4f"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "ebcba15d-fcf7-4d9d-94d3-d174f89810f3", "AQAAAAIAAYagAAAAEKoRdguuvud7yvAu2kr1tvfI5Aw1/YeM8BHRJQn6X2PYmseBXQ0W/NDf1mRj/g1m+g==" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("dea533e1-bb48-46a9-94b5-b8e44c2bde4f"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "850a7937-34c6-4fd0-b1fc-fbef52eb41e9", "AQAAAAIAAYagAAAAEFfM86HNsm0v9DV5RLMonpTSmRMsfhWdVCO6ANvEZqvoFgAdD2kstvnWOtGHWu/8OA==" });
        }
    }
}
