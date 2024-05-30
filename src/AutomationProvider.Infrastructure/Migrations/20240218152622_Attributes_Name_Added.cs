using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AutomationProvider.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class Attributes_Name_Added : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "Attributes",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Name",
                table: "Attributes");
        }
    }
}
