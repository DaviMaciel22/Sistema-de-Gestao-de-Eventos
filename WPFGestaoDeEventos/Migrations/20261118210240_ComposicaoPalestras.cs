using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WPFGestaoDeEventos.Migrations
{
    /// <inheritdoc />
    public partial class ComposicaoPalestras : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Palestras_Eventos_ConferenciaId",
                table: "Palestras");

            migrationBuilder.AddForeignKey(
                name: "FK_Palestras_Eventos_ConferenciaId",
                table: "Palestras",
                column: "ConferenciaId",
                principalTable: "Eventos",
                principalColumn: "EventoId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Palestras_Eventos_ConferenciaId",
                table: "Palestras");

            migrationBuilder.AddForeignKey(
                name: "FK_Palestras_Eventos_ConferenciaId",
                table: "Palestras",
                column: "ConferenciaId",
                principalTable: "Eventos",
                principalColumn: "EventoId");
        }
    }
}
