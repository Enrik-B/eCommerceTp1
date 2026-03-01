using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace eCommerceTP1.Migrations
{
    /// <inheritdoc />
    public partial class ajoutAttributFacture : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<decimal>(
                name: "Prix",
                table: "Produits",
                type: "decimal(65,30)",
                nullable: false,
                oldClrType: typeof(float),
                oldType: "float");

            migrationBuilder.AddColumn<string>(
                name: "FactureTime",
                table: "Factures",
                type: "longtext",
                nullable: false)
                .Annotation("MySql:CharSet", "utf8mb4");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FactureTime",
                table: "Factures");

            migrationBuilder.AlterColumn<float>(
                name: "Prix",
                table: "Produits",
                type: "float",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(65,30)");
        }
    }
}
