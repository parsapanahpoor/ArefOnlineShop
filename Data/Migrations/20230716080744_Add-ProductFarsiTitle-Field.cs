using Microsoft.EntityFrameworkCore.Migrations;

namespace Data.Migrations
{
    public partial class AddProductFarsiTitleField : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ColorFarsiTitle",
                table: "ProductColors",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.InsertData(
                table: "ProductType",
                columns: new[] { "ProductTypeId", "ProductTypeTitle" },
                values: new object[] { 1, "product" });

            migrationBuilder.InsertData(
                table: "ProductType",
                columns: new[] { "ProductTypeId", "ProductTypeTitle" },
                values: new object[] { 2, "blog" });

            migrationBuilder.InsertData(
                table: "ProductType",
                columns: new[] { "ProductTypeId", "ProductTypeTitle" },
                values: new object[] { 3, "Video" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "ProductType",
                keyColumn: "ProductTypeId",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "ProductType",
                keyColumn: "ProductTypeId",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "ProductType",
                keyColumn: "ProductTypeId",
                keyValue: 3);

            migrationBuilder.DropColumn(
                name: "ColorFarsiTitle",
                table: "ProductColors");
        }
    }
}
