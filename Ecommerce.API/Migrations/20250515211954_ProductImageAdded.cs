using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Ecommerce.API.Migrations
{
    /// <inheritdoc />
    public partial class ProductImageAdded : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Products_ProductImage_ProductImageId",
                table: "Products");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ProductImage",
                table: "ProductImage");

            migrationBuilder.RenameTable(
                name: "ProductImage",
                newName: "ProductsImages");

            migrationBuilder.AddColumn<int>(
                name: "ProductCategoryId",
                table: "ProductsImages",
                type: "int",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_ProductsImages",
                table: "ProductsImages",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_ProductsImages_ProductCategoryId",
                table: "ProductsImages",
                column: "ProductCategoryId");

            migrationBuilder.AddForeignKey(
                name: "FK_Products_ProductsImages_ProductImageId",
                table: "Products",
                column: "ProductImageId",
                principalTable: "ProductsImages",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ProductsImages_ProductCategories_ProductCategoryId",
                table: "ProductsImages",
                column: "ProductCategoryId",
                principalTable: "ProductCategories",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Products_ProductsImages_ProductImageId",
                table: "Products");

            migrationBuilder.DropForeignKey(
                name: "FK_ProductsImages_ProductCategories_ProductCategoryId",
                table: "ProductsImages");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ProductsImages",
                table: "ProductsImages");

            migrationBuilder.DropIndex(
                name: "IX_ProductsImages_ProductCategoryId",
                table: "ProductsImages");

            migrationBuilder.DropColumn(
                name: "ProductCategoryId",
                table: "ProductsImages");

            migrationBuilder.RenameTable(
                name: "ProductsImages",
                newName: "ProductImage");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ProductImage",
                table: "ProductImage",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Products_ProductImage_ProductImageId",
                table: "Products",
                column: "ProductImageId",
                principalTable: "ProductImage",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
