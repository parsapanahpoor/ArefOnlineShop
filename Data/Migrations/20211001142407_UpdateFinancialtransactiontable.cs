using Microsoft.EntityFrameworkCore.Migrations;

namespace Data.Migrations
{
    public partial class UpdateFinancialtransactiontable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "FinancialTransactionTypeID",
                table: "FinancialTransactions",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "FinancialTransactionType",
                columns: table => new
                {
                    FinancialTransactionTypeID = table.Column<int>(type: "int", nullable: false),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FinancialTransactionType", x => x.FinancialTransactionTypeID);
                });

            migrationBuilder.CreateIndex(
                name: "IX_FinancialTransactions_FinancialTransactionTypeID",
                table: "FinancialTransactions",
                column: "FinancialTransactionTypeID");

            migrationBuilder.AddForeignKey(
                name: "FK_FinancialTransactions_FinancialTransactionType_FinancialTransactionTypeID",
                table: "FinancialTransactions",
                column: "FinancialTransactionTypeID",
                principalTable: "FinancialTransactionType",
                principalColumn: "FinancialTransactionTypeID",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_FinancialTransactions_FinancialTransactionType_FinancialTransactionTypeID",
                table: "FinancialTransactions");

            migrationBuilder.DropTable(
                name: "FinancialTransactionType");

            migrationBuilder.DropIndex(
                name: "IX_FinancialTransactions_FinancialTransactionTypeID",
                table: "FinancialTransactions");

            migrationBuilder.DropColumn(
                name: "FinancialTransactionTypeID",
                table: "FinancialTransactions");
        }
    }
}
