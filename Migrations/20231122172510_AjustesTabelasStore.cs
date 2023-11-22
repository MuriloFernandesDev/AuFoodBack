using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AuFood.Migrations
{
    /// <inheritdoc />
    public partial class AjustesTabelasStore : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "StoreCategoryMapping");

            migrationBuilder.CreateTable(
                name: "StoreCategoryStore",
                columns: table => new
                {
                    StoreId = table.Column<int>(type: "int", nullable: false),
                    StoreCategoryId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StoreCategoryStore", x => new { x.StoreId, x.StoreCategoryId });
                    table.ForeignKey(
                        name: "FK_StoreCategoryStore_StoreCategory_StoreCategoryId",
                        column: x => x.StoreCategoryId,
                        principalTable: "StoreCategory",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_StoreCategoryStore_Store_StoreId",
                        column: x => x.StoreId,
                        principalTable: "Store",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8")
                .Annotation("Relational:Collation", "utf8_general_ci");

            migrationBuilder.CreateIndex(
                name: "IX_StoreCategoryStore_StoreCategoryId",
                table: "StoreCategoryStore",
                column: "StoreCategoryId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "StoreCategoryStore");

            migrationBuilder.CreateTable(
                name: "StoreCategoryMapping",
                columns: table => new
                {
                    StoreId = table.Column<int>(type: "int", nullable: false),
                    StoreCategoryId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StoreCategoryMapping", x => new { x.StoreId, x.StoreCategoryId });
                    table.ForeignKey(
                        name: "FK_StoreCategoryMapping_StoreCategory_StoreCategoryId",
                        column: x => x.StoreCategoryId,
                        principalTable: "StoreCategory",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_StoreCategoryMapping_Store_StoreId",
                        column: x => x.StoreId,
                        principalTable: "Store",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8")
                .Annotation("Relational:Collation", "utf8_general_ci");

            migrationBuilder.CreateIndex(
                name: "IX_StoreCategoryMapping_StoreCategoryId",
                table: "StoreCategoryMapping",
                column: "StoreCategoryId");
        }
    }
}
