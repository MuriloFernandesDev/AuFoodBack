using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AuFood.Migrations
{
    /// <inheritdoc />
    public partial class AjusteCampoPrice : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<double>(
                name: "Price",
                table: "ProductPrice",
                type: "double",
                nullable: false,
                oldClrType: typeof(double),
                oldType: "double(2,2)");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<double>(
                name: "Price",
                table: "ProductPrice",
                type: "double(2,2)",
                nullable: false,
                oldClrType: typeof(double),
                oldType: "double");
        }
    }
}
