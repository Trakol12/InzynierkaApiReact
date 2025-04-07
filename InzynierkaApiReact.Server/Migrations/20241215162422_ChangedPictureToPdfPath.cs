using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace InzynierkaApiReact.Server.Migrations
{
    /// <inheritdoc />
    public partial class ChangedPictureToPdfPath : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Picture",
                table: "Planogram",
                newName: "PdfPath");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "PdfPath",
                table: "Planogram",
                newName: "Picture");
        }
    }
}
