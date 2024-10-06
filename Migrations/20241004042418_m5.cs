using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace market_api.Migrations
{
    /// <inheritdoc />
    public partial class m5 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateOnly>(
                name: "date",
                table: "Invoices",
                type: "TEXT",
                nullable: false,
                defaultValueSql: "DATE('now')",
                oldClrType: typeof(DateOnly),
                oldType: "TEXT");

            migrationBuilder.AlterColumn<TimeOnly>(
                name: "Time",
                table: "Invoices",
                type: "TEXT",
                nullable: false,
                defaultValueSql: "Time('now')",
                oldClrType: typeof(TimeOnly),
                oldType: "TEXT");

            migrationBuilder.CreateTable(
                name: "InvoiceItems",
                columns: table => new
                {
                    InvoiceId = table.Column<int>(type: "INTEGER", nullable: false),
                    ItemId = table.Column<int>(type: "INTEGER", nullable: false),
                    Quantity = table.Column<int>(type: "INTEGER", nullable: false),
                    price = table.Column<double>(type: "REAL", nullable: false),
                    TotalCost = table.Column<double>(type: "REAL", nullable: false, computedColumnSql: "[Price] * [Quantity]")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InvoiceItems", x => new { x.InvoiceId, x.ItemId });
                    table.ForeignKey(
                        name: "FK_InvoiceItems_Invoices_InvoiceId",
                        column: x => x.InvoiceId,
                        principalTable: "Invoices",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_InvoiceItems_Items_ItemId",
                        column: x => x.ItemId,
                        principalTable: "Items",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_InvoiceItems_ItemId",
                table: "InvoiceItems",
                column: "ItemId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "InvoiceItems");

            migrationBuilder.AlterColumn<DateOnly>(
                name: "date",
                table: "Invoices",
                type: "TEXT",
                nullable: false,
                oldClrType: typeof(DateOnly),
                oldType: "TEXT",
                oldDefaultValueSql: "DATE('now')");

            migrationBuilder.AlterColumn<TimeOnly>(
                name: "Time",
                table: "Invoices",
                type: "TEXT",
                nullable: false,
                oldClrType: typeof(TimeOnly),
                oldType: "TEXT",
                oldDefaultValueSql: "Time('now')");
        }
    }
}
