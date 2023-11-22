using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AuFood.Migrations
{
    /// <inheritdoc />
    public partial class VincStoreClient : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ClientId",
                table: "Store",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Store_ClientId",
                table: "Store",
                column: "ClientId");

            migrationBuilder.AddForeignKey(
                name: "FK_Store_Client",
                table: "Store",
                column: "ClientId",
                principalTable: "Client",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Store_Client",
                table: "Store");

            migrationBuilder.DropIndex(
                name: "IX_Store_ClientId",
                table: "Store");

            migrationBuilder.DropColumn(
                name: "ClientId",
                table: "Store");
        }
    }
}
