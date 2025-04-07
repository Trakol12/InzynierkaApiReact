using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace InzynierkaApiReact.Server.Migrations
{
    /// <inheritdoc />
    public partial class ChangedStoreCodeToCastoCode : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_ProductLocalization_ProductEanCode",
                table: "ProductLocalization");

            migrationBuilder.AlterColumn<string>(
                name: "CastoCode",
                table: "Product",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.CreateIndex(
                name: "IX_ProductLocalization_ProductEanCode",
                table: "ProductLocalization",
                column: "ProductEanCode",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_ProductLocalization_ProductEanCode",
                table: "ProductLocalization");

            migrationBuilder.AlterColumn<int>(
                name: "CastoCode",
                table: "Product",
                type: "int",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.CreateIndex(
                name: "IX_ProductLocalization_ProductEanCode",
                table: "ProductLocalization",
                column: "ProductEanCode");
        }
    }
}
