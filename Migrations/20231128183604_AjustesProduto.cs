using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AuFood.Migrations
{
    /// <inheritdoc />
    public partial class AjustesProduto : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "TimeDelivery",
                table: "Product",
                type: "int(3)",
                nullable: false,
                oldClrType: typeof(double),
                oldType: "double(2,2)");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<double>(
                name: "TimeDelivery",
                table: "Product",
                type: "double(2,2)",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int(3)");
        }
    }
}
