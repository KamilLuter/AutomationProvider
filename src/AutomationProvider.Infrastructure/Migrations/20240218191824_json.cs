using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AutomationProvider.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class Json : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Values",
                table: "Attributes",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "jsonb");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Values",
                table: "Attributes",
                type: "jsonb",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");
        }
    }
}
