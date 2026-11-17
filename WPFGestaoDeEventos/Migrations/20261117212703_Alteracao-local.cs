using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WPFGestaoDeEventos.Migrations
{
    /// <inheritdoc />
    public partial class Alteracaolocal : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Nome",
                table: "Locais",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Nome",
                table: "Locais");
        }
    }
}
