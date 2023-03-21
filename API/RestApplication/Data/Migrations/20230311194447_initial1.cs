using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RestApplication.Data.Migrations
{
    /// <inheritdoc />
    public partial class initial1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_flavorQuantities_products_ProductModelid",
                table: "flavorQuantities");

            migrationBuilder.DropForeignKey(
                name: "FK_weightPrices_products_ProductModelid",
                table: "weightPrices");

            migrationBuilder.DropIndex(
                name: "IX_weightPrices_ProductModelid",
                table: "weightPrices");

            migrationBuilder.DropIndex(
                name: "IX_flavorQuantities_ProductModelid",
                table: "flavorQuantities");

            migrationBuilder.DropColumn(
                name: "ProductModelid",
                table: "weightPrices");

            migrationBuilder.DropColumn(
                name: "ProductModelid",
                table: "flavorQuantities");

            migrationBuilder.AddColumn<Guid>(
                name: "productId",
                table: "weightPrices",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "productId",
                table: "flavorQuantities",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_weightPrices_productId",
                table: "weightPrices",
                column: "productId");

            migrationBuilder.CreateIndex(
                name: "IX_flavorQuantities_productId",
                table: "flavorQuantities",
                column: "productId");

            migrationBuilder.AddForeignKey(
                name: "FK_flavorQuantities_products_productId",
                table: "flavorQuantities",
                column: "productId",
                principalTable: "products",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_weightPrices_products_productId",
                table: "weightPrices",
                column: "productId",
                principalTable: "products",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_flavorQuantities_products_productId",
                table: "flavorQuantities");

            migrationBuilder.DropForeignKey(
                name: "FK_weightPrices_products_productId",
                table: "weightPrices");

            migrationBuilder.DropIndex(
                name: "IX_weightPrices_productId",
                table: "weightPrices");

            migrationBuilder.DropIndex(
                name: "IX_flavorQuantities_productId",
                table: "flavorQuantities");

            migrationBuilder.DropColumn(
                name: "productId",
                table: "weightPrices");

            migrationBuilder.DropColumn(
                name: "productId",
                table: "flavorQuantities");

            migrationBuilder.AddColumn<Guid>(
                name: "ProductModelid",
                table: "weightPrices",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "ProductModelid",
                table: "flavorQuantities",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_weightPrices_ProductModelid",
                table: "weightPrices",
                column: "ProductModelid");

            migrationBuilder.CreateIndex(
                name: "IX_flavorQuantities_ProductModelid",
                table: "flavorQuantities",
                column: "ProductModelid");

            migrationBuilder.AddForeignKey(
                name: "FK_flavorQuantities_products_ProductModelid",
                table: "flavorQuantities",
                column: "ProductModelid",
                principalTable: "products",
                principalColumn: "id");

            migrationBuilder.AddForeignKey(
                name: "FK_weightPrices_products_ProductModelid",
                table: "weightPrices",
                column: "ProductModelid",
                principalTable: "products",
                principalColumn: "id");
        }
    }
}
