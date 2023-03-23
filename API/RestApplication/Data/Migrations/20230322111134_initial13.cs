using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RestApplication.Data.Migrations
{
    /// <inheritdoc />
    public partial class initial13 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "tokenVerifiedAt",
                table: "appUsers",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "verificationToken",
                table: "appUsers",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<DateTime>(
                name: "verificationTokenCreationDate",
                table: "appUsers",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "tokenVerifiedAt",
                table: "appUsers");

            migrationBuilder.DropColumn(
                name: "verificationToken",
                table: "appUsers");

            migrationBuilder.DropColumn(
                name: "verificationTokenCreationDate",
                table: "appUsers");
        }
    }
}
