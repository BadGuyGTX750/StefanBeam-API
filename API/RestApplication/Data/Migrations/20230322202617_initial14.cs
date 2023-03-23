using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RestApplication.Data.Migrations
{
    /// <inheritdoc />
    public partial class initial14 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "changePasswordRequestDate",
                table: "appUsers",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "changePaswwordToken",
                table: "appUsers",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "changedPassword",
                table: "appUsers",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<DateTime>(
                name: "newPasswordCreationDate",
                table: "appUsers",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "changePasswordRequestDate",
                table: "appUsers");

            migrationBuilder.DropColumn(
                name: "changePaswwordToken",
                table: "appUsers");

            migrationBuilder.DropColumn(
                name: "changedPassword",
                table: "appUsers");

            migrationBuilder.DropColumn(
                name: "newPasswordCreationDate",
                table: "appUsers");
        }
    }
}
