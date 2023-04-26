using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace StoEtDash.Web.Migrations
{
    /// <inheritdoc />
    public partial class AddTransactionTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Transactions",
                columns: table => new
                {
                    Id = table.Column<string>(type: "VARCHAR (36)", nullable: false),
                    Action = table.Column<int>(type: "VARCHAR (4)", nullable: false),
                    Time = table.Column<DateTime>(type: "DATETIME", nullable: false),
                    Ticker = table.Column<string>(type: "VARCHAR (10)", nullable: false),
                    Name = table.Column<string>(type: "VARCHAR (75)", nullable: false),
                    NumberOfShares = table.Column<double>(type: "DOUBLE", nullable: false),
                    PricePerShare = table.Column<double>(type: "DOUBLE", nullable: false),
                    Currency = table.Column<int>(type: "VARCHAR (3)", nullable: false),
                    ExchangeRate = table.Column<double>(type: "DOUBLE", nullable: false),
                    TotalInEur = table.Column<double>(type: "DOUBLE", nullable: false),
                    FeesInEur = table.Column<double>(type: "DOUBLE", nullable: false),
                    Username = table.Column<string>(type: "VARCHAR (60)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Transactions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Transactions_Users_Username",
                        column: x => x.Username,
                        principalTable: "Users",
                        principalColumn: "Username",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Transactions_Username",
                table: "Transactions",
                column: "Username");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Transactions");
        }
    }
}
