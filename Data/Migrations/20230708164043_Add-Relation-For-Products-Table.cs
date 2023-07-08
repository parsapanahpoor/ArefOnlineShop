using Microsoft.EntityFrameworkCore.Migrations;

namespace Data.Migrations
{
    public partial class AddRelationForProductsTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ProductsSizeId",
                table: "ProductSelectedSizes",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ProductColorId",
                table: "ProductSelectedColors",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_ProductSelectedSizes_ProductId",
                table: "ProductSelectedSizes",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductSelectedSizes_ProductsSizeId",
                table: "ProductSelectedSizes",
                column: "ProductsSizeId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductSelectedColors_ProductColorId",
                table: "ProductSelectedColors",
                column: "ProductColorId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductSelectedColors_ProductId",
                table: "ProductSelectedColors",
                column: "ProductId");

            migrationBuilder.AddForeignKey(
                name: "FK_ProductSelectedColors_product_ProductId",
                table: "ProductSelectedColors",
                column: "ProductId",
                principalTable: "product",
                principalColumn: "ProductID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ProductSelectedColors_ProductColors_ProductColorId",
                table: "ProductSelectedColors",
                column: "ProductColorId",
                principalTable: "ProductColors",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ProductSelectedSizes_product_ProductId",
                table: "ProductSelectedSizes",
                column: "ProductId",
                principalTable: "product",
                principalColumn: "ProductID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ProductSelectedSizes_ProductsSizes_ProductsSizeId",
                table: "ProductSelectedSizes",
                column: "ProductsSizeId",
                principalTable: "ProductsSizes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProductSelectedColors_product_ProductId",
                table: "ProductSelectedColors");

            migrationBuilder.DropForeignKey(
                name: "FK_ProductSelectedColors_ProductColors_ProductColorId",
                table: "ProductSelectedColors");

            migrationBuilder.DropForeignKey(
                name: "FK_ProductSelectedSizes_product_ProductId",
                table: "ProductSelectedSizes");

            migrationBuilder.DropForeignKey(
                name: "FK_ProductSelectedSizes_ProductsSizes_ProductsSizeId",
                table: "ProductSelectedSizes");

            migrationBuilder.DropIndex(
                name: "IX_ProductSelectedSizes_ProductId",
                table: "ProductSelectedSizes");

            migrationBuilder.DropIndex(
                name: "IX_ProductSelectedSizes_ProductsSizeId",
                table: "ProductSelectedSizes");

            migrationBuilder.DropIndex(
                name: "IX_ProductSelectedColors_ProductColorId",
                table: "ProductSelectedColors");

            migrationBuilder.DropIndex(
                name: "IX_ProductSelectedColors_ProductId",
                table: "ProductSelectedColors");

            migrationBuilder.DropColumn(
                name: "ProductsSizeId",
                table: "ProductSelectedSizes");

            migrationBuilder.DropColumn(
                name: "ProductColorId",
                table: "ProductSelectedColors");
        }
    }
}
