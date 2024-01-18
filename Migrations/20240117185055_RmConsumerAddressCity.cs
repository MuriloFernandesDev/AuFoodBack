using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AuFood.Migrations
{
    /// <inheritdoc />
    public partial class RmConsumerAddressCity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_consumer_city",
                table: "Consumer_address");

            migrationBuilder.DropIndex(
                name: "IX_Consumer_address_City_id",
                table: "Consumer_address");

            migrationBuilder.DropColumn(
                name: "City_id",
                table: "Consumer_address");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "City_id",
                table: "Consumer_address",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Consumer_address_City_id",
                table: "Consumer_address",
                column: "City_id");

            migrationBuilder.AddForeignKey(
                name: "FK_consumer_city",
                table: "Consumer_address",
                column: "City_id",
                principalTable: "City",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
