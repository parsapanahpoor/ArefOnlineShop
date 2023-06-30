using Microsoft.EntityFrameworkCore.Migrations;

namespace Data.Migrations
{
    public partial class AddReturendPRoductsTables : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsReturend",
                table: "OrderDetails",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.CreateTable(
                name: "returnedProductTypes",
                columns: table => new
                {
                    ReturnedProductTypeID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_returnedProductTypes", x => x.ReturnedProductTypeID);
                });

            migrationBuilder.CreateTable(
                name: "ReturnedProducts",
                columns: table => new
                {
                    ReturnedProductID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    OrderDetailID = table.Column<int>(type: "int", nullable: false),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    ReturnProductTypeID = table.Column<int>(type: "int", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ReturnedProducts", x => x.ReturnedProductID);
                    table.ForeignKey(
                        name: "FK_ReturnedProducts_OrderDetails_OrderDetailID",
                        column: x => x.OrderDetailID,
                        principalTable: "OrderDetails",
                        principalColumn: "OrderDetailID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ReturnedProducts_returnedProductTypes_ReturnProductTypeID",
                        column: x => x.ReturnProductTypeID,
                        principalTable: "returnedProductTypes",
                        principalColumn: "ReturnedProductTypeID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ReturnedProducts_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ReturnedProducts_OrderDetailID",
                table: "ReturnedProducts",
                column: "OrderDetailID");

            migrationBuilder.CreateIndex(
                name: "IX_ReturnedProducts_ReturnProductTypeID",
                table: "ReturnedProducts",
                column: "ReturnProductTypeID");

            migrationBuilder.CreateIndex(
                name: "IX_ReturnedProducts_UserId",
                table: "ReturnedProducts",
                column: "UserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ReturnedProducts");

            migrationBuilder.DropTable(
                name: "returnedProductTypes");

            migrationBuilder.DropColumn(
                name: "IsReturend",
                table: "OrderDetails");
        }
    }
}
