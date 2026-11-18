using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WPFGestaoDeEventos.Migrations
{
    /// <inheritdoc />
    public partial class AjustesTelas : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "SenhaHash",
                table: "Participantes",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Status",
                table: "Inscricoes",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.UpdateData(
                table: "Inscricoes",
                keyColumn: "InscricaoId",
                keyValue: 1,
                column: "Status",
                value: "Confirmado");

            migrationBuilder.UpdateData(
                table: "Inscricoes",
                keyColumn: "InscricaoId",
                keyValue: 2,
                column: "Status",
                value: "Confirmado");

            migrationBuilder.UpdateData(
                table: "Participantes",
                keyColumn: "ParticipanteId",
                keyValue: 1,
                column: "SenhaHash",
                value: "8D969EEF6ECAD3C29A3A629280E686CF0C3F5D5A86AFF3CA12020C923ADC6C92");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "SenhaHash",
                table: "Participantes");

            migrationBuilder.DropColumn(
                name: "Status",
                table: "Inscricoes");
        }
    }
}
