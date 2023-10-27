using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AuFood.Migrations
{
    /// <inheritdoc />
    public partial class NewTbAvaliationStore : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "IdProduct",
                table: "ProductPrice",
                newName: "ProductId");

            migrationBuilder.RenameIndex(
                name: "IX_ProductPrice_IdProduct",
                table: "ProductPrice",
                newName: "IX_ProductPrice_ProductId");

            migrationBuilder.RenameColumn(
                name: "IdProductCategory",
                table: "Product",
                newName: "ProductCategoryId");

            migrationBuilder.RenameColumn(
                name: "IdClient",
                table: "Product",
                newName: "ClientId");

            migrationBuilder.RenameIndex(
                name: "IX_Product_IdProductCategory",
                table: "Product",
                newName: "IX_Product_ProductCategoryId");

            migrationBuilder.RenameIndex(
                name: "IX_Product_IdClient",
                table: "Product",
                newName: "IX_Product_ClientId");

            migrationBuilder.CreateTable(
                name: "AvaliationStore",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    StoreId = table.Column<int>(type: "int", nullable: false),
                    Rating = table.Column<int>(type: "int(2)", nullable: false),
                    Comment = table.Column<string>(type: "varchar(255)", maxLength: 255, nullable: false, collation: "utf8_general_ci")
                        .Annotation("MySql:CharSet", "utf8")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PRIMARY", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Avaliation_Store",
                        column: x => x.StoreId,
                        principalTable: "Store",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8")
                .Annotation("Relational:Collation", "utf8_general_ci");

            migrationBuilder.CreateIndex(
                name: "IX_AvaliationStore_StoreId",
                table: "AvaliationStore",
                column: "StoreId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AvaliationStore");

            migrationBuilder.RenameColumn(
                name: "ProductId",
                table: "ProductPrice",
                newName: "IdProduct");

            migrationBuilder.RenameIndex(
                name: "IX_ProductPrice_ProductId",
                table: "ProductPrice",
                newName: "IX_ProductPrice_IdProduct");

            migrationBuilder.RenameColumn(
                name: "ProductCategoryId",
                table: "Product",
                newName: "IdProductCategory");

            migrationBuilder.RenameColumn(
                name: "ClientId",
                table: "Product",
                newName: "IdClient");

            migrationBuilder.RenameIndex(
                name: "IX_Product_ProductCategoryId",
                table: "Product",
                newName: "IX_Product_IdProductCategory");

            migrationBuilder.RenameIndex(
                name: "IX_Product_ClientId",
                table: "Product",
                newName: "IX_Product_IdClient");
        }
    }
}
