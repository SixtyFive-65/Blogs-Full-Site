using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Blogz.Web.Migrations.BlogsAuthDb
{
    /// <inheritdoc />
    public partial class InitialAuthDbV2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("dea533e1-bb48-46a9-94b5-b8e44c2bde4f"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "3652c7c8-b9e9-426b-8356-267e8f38f0c6", "AQAAAAIAAYagAAAAEMCKclmHmCC7RFkrYYyv0dOq0uX36CLj42ASsmU6wHv9nrPV1pjgFAds1/3HxHRJrw==" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("dea533e1-bb48-46a9-94b5-b8e44c2bde4f"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "03901ddc-df8c-4686-9262-7464bf4b9bbd", "AQAAAAIAAYagAAAAEISTDxaxe48Mu0XSHK0K3aym+oq68T85B39xuRd+H/IKM6ASYbaLlXBzmix9PJjcxw==" });
        }
    }
}
