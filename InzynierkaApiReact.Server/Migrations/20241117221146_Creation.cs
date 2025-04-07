using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace InzynierkaApiReact.Server.Migrations
{
    /// <inheritdoc />
    public partial class Creation : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Planogram",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Picture = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Date = table.Column<DateOnly>(type: "date", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Planogram", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "User",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsAdmin = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_User", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Product",
                columns: table => new
                {
                    EanCode = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CastoCode = table.Column<int>(type: "int", nullable: false),
                    Category = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PlanogramId = table.Column<int>(type: "int", nullable: true),
                    ProductPageLink = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Product", x => x.EanCode);
                    table.ForeignKey(
                        name: "FK_Product_Planogram_PlanogramId",
                        column: x => x.PlanogramId,
                        principalTable: "Planogram",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "ProductLocalization",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProductEanCode = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Alley = table.Column<int>(type: "int", nullable: false),
                    NumberOnTheShelf = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductLocalization", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ProductLocalization_Product_ProductEanCode",
                        column: x => x.ProductEanCode,
                        principalTable: "Product",
                        principalColumn: "EanCode",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Product_PlanogramId",
                table: "Product",
                column: "PlanogramId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductLocalization_ProductEanCode",
                table: "ProductLocalization",
                column: "ProductEanCode");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ProductLocalization");

            migrationBuilder.DropTable(
                name: "User");

            migrationBuilder.DropTable(
                name: "Product");

            migrationBuilder.DropTable(
                name: "Planogram");
        }
    }
}
