using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AuFood.Migrations
{
    /// <inheritdoc />
    public partial class RemoveAtt : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TimeDelivery",
                table: "Store");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<double>(
                name: "TimeDelivery",
                table: "Store",
                type: "double(2,2)",
                nullable: false,
                defaultValue: 0.0);
        }
    }
}
