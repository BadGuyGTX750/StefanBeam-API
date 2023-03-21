using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RestApplication.Data.Migrations
{
    /// <inheritdoc />
    public partial class initial3 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "parentCategoryName",
                table: "subCategories",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<Guid>(
                name: "parentCategoryId",
                table: "subCategories",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldNullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "parentCategoryId",
                table: "products",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AlterColumn<string>(
                name: "parentCategoryName",
                table: "middleCategories",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<Guid>(
                name: "parentCategoryId",
                table: "middleCategories",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_subCategories_parentCategoryId",
                table: "subCategories",
                column: "parentCategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_products_parentCategoryId",
                table: "products",
                column: "parentCategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_middleCategories_parentCategoryId",
                table: "middleCategories",
                column: "parentCategoryId");

            migrationBuilder.AddForeignKey(
                name: "FK_middleCategories_topCategories_parentCategoryId",
                table: "middleCategories",
                column: "parentCategoryId",
                principalTable: "topCategories",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_products_subCategories_parentCategoryId",
                table: "products",
                column: "parentCategoryId",
                principalTable: "subCategories",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_subCategories_middleCategories_parentCategoryId",
                table: "subCategories",
                column: "parentCategoryId",
                principalTable: "middleCategories",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_middleCategories_topCategories_parentCategoryId",
                table: "middleCategories");

            migrationBuilder.DropForeignKey(
                name: "FK_products_subCategories_parentCategoryId",
                table: "products");

            migrationBuilder.DropForeignKey(
                name: "FK_subCategories_middleCategories_parentCategoryId",
                table: "subCategories");

            migrationBuilder.DropIndex(
                name: "IX_subCategories_parentCategoryId",
                table: "subCategories");

            migrationBuilder.DropIndex(
                name: "IX_products_parentCategoryId",
                table: "products");

            migrationBuilder.DropIndex(
                name: "IX_middleCategories_parentCategoryId",
                table: "middleCategories");

            migrationBuilder.DropColumn(
                name: "parentCategoryId",
                table: "products");

            migrationBuilder.AlterColumn<string>(
                name: "parentCategoryName",
                table: "subCategories",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<Guid>(
                name: "parentCategoryId",
                table: "subCategories",
                type: "uniqueidentifier",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.AlterColumn<string>(
                name: "parentCategoryName",
                table: "middleCategories",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<Guid>(
                name: "parentCategoryId",
                table: "middleCategories",
                type: "uniqueidentifier",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");
        }
    }
}
