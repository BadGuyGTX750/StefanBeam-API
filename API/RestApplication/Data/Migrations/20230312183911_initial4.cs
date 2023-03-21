using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RestApplication.Data.Migrations
{
    /// <inheritdoc />
    public partial class initial4 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
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

            migrationBuilder.AddForeignKey(
                name: "FK_middleCategories_topCategories_parentCategoryId",
                table: "middleCategories",
                column: "parentCategoryId",
                principalTable: "topCategories",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_products_subCategories_parentCategoryId",
                table: "products",
                column: "parentCategoryId",
                principalTable: "subCategories",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_subCategories_middleCategories_parentCategoryId",
                table: "subCategories",
                column: "parentCategoryId",
                principalTable: "middleCategories",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);
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
    }
}
