using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Data.Migrations
{
    public partial class AddSiteSettingsTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "SiteSettings",
                columns: table => new
                {
                    Id = table.Column<decimal>(type: "decimal(20,0)", nullable: false),
                    CopyRightText = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SendSMSTimer = table.Column<int>(type: "int", nullable: false),
                    SiteDomain = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SiteCashDesk = table.Column<int>(type: "int", nullable: false),
                    CreateDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsDelete = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SiteSettings", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "SiteSettings");
        }
    }
}
