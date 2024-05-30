using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AutomationProvider.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class product_category_relation : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Products_CatalogId",
                table: "Products");

            migrationBuilder.CreateIndex(
                name: "IX_Products_CatalogId",
                table: "Products",
                column: "CatalogId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Products_CatalogId",
                table: "Products");

            migrationBuilder.CreateIndex(
                name: "IX_Products_CatalogId",
                table: "Products",
                column: "CatalogId",
                unique: true);
        }
    }
}
