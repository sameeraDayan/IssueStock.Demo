using Microsoft.EntityFrameworkCore.Migrations;

namespace IssueStock.Demo.API.Migrations
{
    public partial class SeedDataStockItems : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "StockItem",
                columns: new[] { "Id", "Code", "Description", "Name" },
                values: new object[] { 1, "C001", "abc Pvt Ltd description", "Company abc Pvt Ltd" });

            migrationBuilder.InsertData(
                table: "StockItem",
                columns: new[] { "Id", "Code", "Description", "Name" },
                values: new object[] { 2, "C002", "pqr Pvt Ltd description", "Company pqr Pvt Ltd" });

            migrationBuilder.InsertData(
                table: "StockItem",
                columns: new[] { "Id", "Code", "Description", "Name" },
                values: new object[] { 3, "C003", "abc xyz Ltd description", "Company xyz Pvt Ltd" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "StockItem",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "StockItem",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "StockItem",
                keyColumn: "Id",
                keyValue: 3);
        }
    }
}
