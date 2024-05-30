using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AutomationProvider.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class moneyOrderorderLine : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_OrderLines_Orders_OrderId",
                table: "OrderLines");

            migrationBuilder.DropIndex(
                name: "IX_OrderLines_OrderId",
                table: "OrderLines");

            migrationBuilder.AddColumn<string>(
                name: "PriceCurrency",
                table: "Orders",
                type: "nvarchar(3)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<decimal>(
                name: "PriceValue",
                table: "Orders",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AlterColumn<Guid>(
                name: "OrderId",
                table: "OrderLines",
                type: "uniqueidentifier",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.AddColumn<Guid>(
                name: "OrderLine",
                table: "OrderLines",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_OrderLines_OrderLine",
                table: "OrderLines",
                column: "OrderLine");

            migrationBuilder.AddForeignKey(
                name: "FK_OrderLines_Orders_OrderLine",
                table: "OrderLines",
                column: "OrderLine",
                principalTable: "Orders",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_OrderLines_Orders_OrderLine",
                table: "OrderLines");

            migrationBuilder.DropIndex(
                name: "IX_OrderLines_OrderLine",
                table: "OrderLines");

            migrationBuilder.DropColumn(
                name: "PriceCurrency",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "PriceValue",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "OrderLine",
                table: "OrderLines");

            migrationBuilder.AlterColumn<Guid>(
                name: "OrderId",
                table: "OrderLines",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_OrderLines_OrderId",
                table: "OrderLines",
                column: "OrderId");

            migrationBuilder.AddForeignKey(
                name: "FK_OrderLines_Orders_OrderId",
                table: "OrderLines",
                column: "OrderId",
                principalTable: "Orders",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
