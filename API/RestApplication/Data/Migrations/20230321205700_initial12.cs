using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RestApplication.Data.Migrations
{
    /// <inheritdoc />
    public partial class initial12 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "chatName",
                table: "appUsers",
                newName: "lastName");

            migrationBuilder.AddColumn<string>(
                name: "firstName",
                table: "appUsers",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<bool>(
                name: "isVerified",
                table: "appUsers",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "firstName",
                table: "appUsers");

            migrationBuilder.DropColumn(
                name: "isVerified",
                table: "appUsers");

            migrationBuilder.RenameColumn(
                name: "lastName",
                table: "appUsers",
                newName: "chatName");
        }
    }
}
