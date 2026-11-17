using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WPFGestaoDeEventos.Migrations
{
    /// <inheritdoc />
    public partial class Correcaotabelas : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "País",
                table: "Locais",
                newName: "Pais");

            migrationBuilder.RenameColumn(
                name: "idPalestra",
                table: "Eventos",
                newName: "PalestraId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Pais",
                table: "Locais",
                newName: "País");

            migrationBuilder.RenameColumn(
                name: "PalestraId",
                table: "Eventos",
                newName: "idPalestra");
        }
    }
}
