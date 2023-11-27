using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AuFood.Migrations
{
    /// <inheritdoc />
    public partial class RemovendoTbl : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Store_City_CityId",
                table: "Store");

            migrationBuilder.DropForeignKey(
                name: "FK_Store_ZipCode",
                table: "Store");

            migrationBuilder.DropTable(
                name: "StoreAddress");

            migrationBuilder.DropIndex(
                name: "IX_Store_StoreAddressId",
                table: "Store");

            migrationBuilder.DropColumn(
                name: "StoreAddressId",
                table: "Store");

            migrationBuilder.AlterColumn<string>(
                name: "Phone",
                table: "Store",
                type: "varchar(30)",
                maxLength: 30,
                nullable: true,
                collation: "utf8_general_ci",
                oldClrType: typeof(string),
                oldType: "varchar(30)",
                oldMaxLength: 30)
                .Annotation("MySql:CharSet", "utf8")
                .OldAnnotation("MySql:CharSet", "utf8")
                .OldAnnotation("Relational:Collation", "utf8_general_ci");

            migrationBuilder.AlterColumn<string>(
                name: "InstagramUrl",
                table: "Store",
                type: "varchar(100)",
                maxLength: 100,
                nullable: true,
                collation: "utf8_general_ci",
                oldClrType: typeof(string),
                oldType: "varchar(100)",
                oldMaxLength: 100)
                .Annotation("MySql:CharSet", "utf8")
                .OldAnnotation("MySql:CharSet", "utf8")
                .OldAnnotation("Relational:Collation", "utf8_general_ci");

            migrationBuilder.AlterColumn<int>(
                name: "CityId",
                table: "Store",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Neighborhood",
                table: "Store",
                type: "longtext",
                nullable: false,
                collation: "utf8_general_ci")
                .Annotation("MySql:CharSet", "utf8");

            migrationBuilder.AddColumn<string>(
                name: "Street",
                table: "Store",
                type: "longtext",
                nullable: false,
                collation: "utf8_general_ci")
                .Annotation("MySql:CharSet", "utf8");

            migrationBuilder.AddColumn<string>(
                name: "Zip",
                table: "Store",
                type: "longtext",
                nullable: false,
                collation: "utf8_general_ci")
                .Annotation("MySql:CharSet", "utf8");

            migrationBuilder.AddForeignKey(
                name: "FK_Store_City",
                table: "Store",
                column: "CityId",
                principalTable: "City",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Store_City",
                table: "Store");

            migrationBuilder.DropColumn(
                name: "Neighborhood",
                table: "Store");

            migrationBuilder.DropColumn(
                name: "Street",
                table: "Store");

            migrationBuilder.DropColumn(
                name: "Zip",
                table: "Store");

            migrationBuilder.UpdateData(
                table: "Store",
                keyColumn: "Phone",
                keyValue: null,
                column: "Phone",
                value: "");

            migrationBuilder.AlterColumn<string>(
                name: "Phone",
                table: "Store",
                type: "varchar(30)",
                maxLength: 30,
                nullable: false,
                collation: "utf8_general_ci",
                oldClrType: typeof(string),
                oldType: "varchar(30)",
                oldMaxLength: 30,
                oldNullable: true)
                .Annotation("MySql:CharSet", "utf8")
                .OldAnnotation("MySql:CharSet", "utf8")
                .OldAnnotation("Relational:Collation", "utf8_general_ci");

            migrationBuilder.UpdateData(
                table: "Store",
                keyColumn: "InstagramUrl",
                keyValue: null,
                column: "InstagramUrl",
                value: "");

            migrationBuilder.AlterColumn<string>(
                name: "InstagramUrl",
                table: "Store",
                type: "varchar(100)",
                maxLength: 100,
                nullable: false,
                collation: "utf8_general_ci",
                oldClrType: typeof(string),
                oldType: "varchar(100)",
                oldMaxLength: 100,
                oldNullable: true)
                .Annotation("MySql:CharSet", "utf8")
                .OldAnnotation("MySql:CharSet", "utf8")
                .OldAnnotation("Relational:Collation", "utf8_general_ci");

            migrationBuilder.AlterColumn<int>(
                name: "CityId",
                table: "Store",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<int>(
                name: "StoreAddressId",
                table: "Store",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "StoreAddress",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    CityId = table.Column<int>(type: "int", nullable: false),
                    Neighborhood = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false, collation: "utf8_general_ci")
                        .Annotation("MySql:CharSet", "utf8"),
                    Street = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false, collation: "utf8_general_ci")
                        .Annotation("MySql:CharSet", "utf8"),
                    Zip = table.Column<string>(type: "varchar(8)", maxLength: 8, nullable: false, collation: "utf8_general_ci")
                        .Annotation("MySql:CharSet", "utf8")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PRIMARY", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ZipCode_City",
                        column: x => x.CityId,
                        principalTable: "City",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8")
                .Annotation("Relational:Collation", "utf8_general_ci");

            migrationBuilder.CreateIndex(
                name: "IX_Store_StoreAddressId",
                table: "Store",
                column: "StoreAddressId");

            migrationBuilder.CreateIndex(
                name: "IX_StoreAddress_CityId",
                table: "StoreAddress",
                column: "CityId");

            migrationBuilder.AddForeignKey(
                name: "FK_Store_City_CityId",
                table: "Store",
                column: "CityId",
                principalTable: "City",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Store_ZipCode",
                table: "Store",
                column: "StoreAddressId",
                principalTable: "StoreAddress",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
