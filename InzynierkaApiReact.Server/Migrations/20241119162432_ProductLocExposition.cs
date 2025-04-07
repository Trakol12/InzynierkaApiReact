using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace InzynierkaApiReact.Server.Migrations
{
    /// <inheritdoc />
    public partial class ProductLocExposition : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "NumberOnTheShelf",
                table: "ProductLocalization",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<int>(
                name: "NumerOnTheExposition",
                table: "ProductLocalization",
                type: "int",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "NumerOnTheExposition",
                table: "ProductLocalization");

            migrationBuilder.AlterColumn<int>(
                name: "NumberOnTheShelf",
                table: "ProductLocalization",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);
        }
    }
}
