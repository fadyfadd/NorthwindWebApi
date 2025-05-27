using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace NorthwindWebApi.Migrations
{
    /// <inheritdoc />
    public partial class UsingSequence : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "supplier_id",
                table: "suppliers",
                type: "integer",
                nullable: false,
                defaultValueSql: "nextval('\"product_sequence\"')",
                oldClrType: typeof(int),
                oldType: "integer",
                oldDefaultValueSql: "nextval('product_sequence')");

            migrationBuilder.AlterColumn<int>(
                name: "product_id",
                table: "products",
                type: "integer",
                nullable: false,
                defaultValueSql: "nextval('\"product_sequence\"')",
                oldClrType: typeof(int),
                oldType: "integer",
                oldDefaultValueSql: "nextval('product_sequence')");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "supplier_id",
                table: "suppliers",
                type: "integer",
                nullable: false,
                defaultValueSql: "nextval('product_sequence')",
                oldClrType: typeof(int),
                oldType: "integer",
                oldDefaultValueSql: "nextval('\"product_sequence\"')");

            migrationBuilder.AlterColumn<int>(
                name: "product_id",
                table: "products",
                type: "integer",
                nullable: false,
                defaultValueSql: "nextval('product_sequence')",
                oldClrType: typeof(int),
                oldType: "integer",
                oldDefaultValueSql: "nextval('\"product_sequence\"')");
        }
    }
}
