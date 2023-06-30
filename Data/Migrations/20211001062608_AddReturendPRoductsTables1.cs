using Microsoft.EntityFrameworkCore.Migrations;

namespace Data.Migrations
{
    public partial class AddReturendPRoductsTables1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ReturnedProducts_returnedProductTypes_ReturnProductTypeID",
                table: "ReturnedProducts");

            migrationBuilder.DropPrimaryKey(
                name: "PK_returnedProductTypes",
                table: "returnedProductTypes");

            migrationBuilder.RenameTable(
                name: "returnedProductTypes",
                newName: "ReturnedProductTypes");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ReturnedProductTypes",
                table: "ReturnedProductTypes",
                column: "ReturnedProductTypeID");

            migrationBuilder.AddForeignKey(
                name: "FK_ReturnedProducts_ReturnedProductTypes_ReturnProductTypeID",
                table: "ReturnedProducts",
                column: "ReturnProductTypeID",
                principalTable: "ReturnedProductTypes",
                principalColumn: "ReturnedProductTypeID",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ReturnedProducts_ReturnedProductTypes_ReturnProductTypeID",
                table: "ReturnedProducts");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ReturnedProductTypes",
                table: "ReturnedProductTypes");

            migrationBuilder.RenameTable(
                name: "ReturnedProductTypes",
                newName: "returnedProductTypes");

            migrationBuilder.AddPrimaryKey(
                name: "PK_returnedProductTypes",
                table: "returnedProductTypes",
                column: "ReturnedProductTypeID");

            migrationBuilder.AddForeignKey(
                name: "FK_ReturnedProducts_returnedProductTypes_ReturnProductTypeID",
                table: "ReturnedProducts",
                column: "ReturnProductTypeID",
                principalTable: "returnedProductTypes",
                principalColumn: "ReturnedProductTypeID",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
