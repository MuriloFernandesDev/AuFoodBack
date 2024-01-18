using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AuFood.Migrations
{
    /// <inheritdoc />
    public partial class RemovendoTb : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Store_category_store");

            migrationBuilder.DropTable(
                name: "Store_category");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Store_category",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Icon = table.Column<string>(type: "varchar(30)", maxLength: 30, nullable: false, collation: "utf8_general_ci")
                        .Annotation("MySql:CharSet", "utf8"),
                    Name = table.Column<string>(type: "varchar(30)", maxLength: 30, nullable: false, collation: "utf8_general_ci")
                        .Annotation("MySql:CharSet", "utf8")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PRIMARY", x => x.Id);
                })
                .Annotation("MySql:CharSet", "utf8")
                .Annotation("Relational:Collation", "utf8_general_ci");

            migrationBuilder.CreateTable(
                name: "Store_category_store",
                columns: table => new
                {
                    Store_id = table.Column<int>(type: "int", nullable: false),
                    Store_category_id = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Store_category_store", x => new { x.Store_id, x.Store_category_id });
                    table.ForeignKey(
                        name: "FK_Store_category_store_Store_Store_id",
                        column: x => x.Store_id,
                        principalTable: "Store",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Store_category_store_Store_category_Store_category_id",
                        column: x => x.Store_category_id,
                        principalTable: "Store_category",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8")
                .Annotation("Relational:Collation", "utf8_general_ci");

            migrationBuilder.CreateIndex(
                name: "IX_Store_category_store_Store_category_id",
                table: "Store_category_store",
                column: "Store_category_id");
        }
    }
}
