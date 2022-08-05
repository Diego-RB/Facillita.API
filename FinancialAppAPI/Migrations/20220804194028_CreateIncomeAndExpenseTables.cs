using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FinancialAppAPI.Migrations
{
    public partial class CreateIncomeAndExpenseTables : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Expenses",
                columns: table => new
                {
                    expenseId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    expenseName = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    expenseAmount = table.Column<double>(type: "float", nullable: false),
                    expenseDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Expenses", x => x.expenseId);
                });

            migrationBuilder.CreateTable(
                name: "Incomes",
                columns: table => new
                {
                    incomeId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    incomeName = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    incomeAmount = table.Column<double>(type: "float", nullable: false),
                    incomeDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Incomes", x => x.incomeId);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Expenses");

            migrationBuilder.DropTable(
                name: "Incomes");
        }
    }
}
