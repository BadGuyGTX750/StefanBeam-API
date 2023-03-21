using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RestApplication.Data.Migrations
{
    /// <inheritdoc />
    public partial class initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "appUsers",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    chatName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    password = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    role = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_appUsers", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "middleCategories",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    parentCategoryId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    parentCategoryName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_middleCategories", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "products",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    shortDescr = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    longDescr = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    categoryName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    isInStock = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_products", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "subCategories",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    parentCategoryId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    parentCategoryName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_subCategories", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "topCategories",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    parentCategoryId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    parentCategoryName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_topCategories", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "flavorQuantities",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    flavor = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    quantity = table.Column<long>(type: "bigint", nullable: false),
                    ProductModelid = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_flavorQuantities", x => x.id);
                    table.ForeignKey(
                        name: "FK_flavorQuantities_products_ProductModelid",
                        column: x => x.ProductModelid,
                        principalTable: "products",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "weightPrices",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    weight = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    price = table.Column<int>(type: "int", nullable: false),
                    currency = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ProductModelid = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_weightPrices", x => x.id);
                    table.ForeignKey(
                        name: "FK_weightPrices_products_ProductModelid",
                        column: x => x.ProductModelid,
                        principalTable: "products",
                        principalColumn: "id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_flavorQuantities_ProductModelid",
                table: "flavorQuantities",
                column: "ProductModelid");

            migrationBuilder.CreateIndex(
                name: "IX_weightPrices_ProductModelid",
                table: "weightPrices",
                column: "ProductModelid");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "appUsers");

            migrationBuilder.DropTable(
                name: "flavorQuantities");

            migrationBuilder.DropTable(
                name: "middleCategories");

            migrationBuilder.DropTable(
                name: "subCategories");

            migrationBuilder.DropTable(
                name: "topCategories");

            migrationBuilder.DropTable(
                name: "weightPrices");

            migrationBuilder.DropTable(
                name: "products");
        }
    }
}
