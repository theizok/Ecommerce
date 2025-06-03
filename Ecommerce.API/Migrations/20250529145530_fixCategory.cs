using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Ecommerce.API.Migrations
{
    /// <inheritdoc />
    public partial class fixCategory : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Categories_ProductCategories_ProductCategoryId",
                table: "Categories");

            migrationBuilder.DropIndex(
                name: "IX_Categories_Name_ProductCategoryId",
                table: "Categories");

            migrationBuilder.AlterColumn<int>(
                name: "ProductCategoryId",
                table: "Categories",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.CreateIndex(
                name: "IX_Categories_Name_ProductCategoryId",
                table: "Categories",
                columns: new[] { "Name", "ProductCategoryId" },
                unique: true,
                filter: "[ProductCategoryId] IS NOT NULL");

            migrationBuilder.AddForeignKey(
                name: "FK_Categories_ProductCategories_ProductCategoryId",
                table: "Categories",
                column: "ProductCategoryId",
                principalTable: "ProductCategories",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Categories_ProductCategories_ProductCategoryId",
                table: "Categories");

            migrationBuilder.DropIndex(
                name: "IX_Categories_Name_ProductCategoryId",
                table: "Categories");

            migrationBuilder.AlterColumn<int>(
                name: "ProductCategoryId",
                table: "Categories",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Categories_Name_ProductCategoryId",
                table: "Categories",
                columns: new[] { "Name", "ProductCategoryId" },
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Categories_ProductCategories_ProductCategoryId",
                table: "Categories",
                column: "ProductCategoryId",
                principalTable: "ProductCategories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
