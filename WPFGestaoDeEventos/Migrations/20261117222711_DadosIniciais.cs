using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace WPFGestaoDeEventos.Migrations
{
    /// <inheritdoc />
    public partial class DadosIniciais : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Locais",
                columns: new[] { "LocalId", "Bairro", "Capacidade", "Cidade", "Estado", "Nome", "Numero", "Pais", "Rua" },
                values: new object[,]
                {
                    { 1, "Centro", 100, "Vila Velha", "ES", "Auditório UVV", 123, "Brasil", "Rua da UVV" },
                    { 2, "Praia do Canto", 300, "Vitória", "ES", "Centro de Convenções", 456, "Brasil", "Av. Vitória" },
                    { 3, "Centro", 30, "Vila Velha", "ES", "Lab 3 UVV", 123, "Brasil", "Rua da UVV" }
                });

            migrationBuilder.InsertData(
                table: "Organizadores",
                columns: new[] { "OrganizadorId", "CNPJ", "Discriminator", "Email", "Numero", "RazaoSocial" },
                values: new object[] { 1, "12.345.678/0001-90", "Empresa", "contato@uvveventos.com.br", "27999998888", "UVV" });

            migrationBuilder.InsertData(
                table: "Organizadores",
                columns: new[] { "OrganizadorId", "CPF", "Discriminator", "Email", "Nome", "Numero" },
                values: new object[] { 2, "123.456.789-00", "PessoaFisica", "contato@uvveventos.com.br", "Júnior da Costa", "27999998888" });

            migrationBuilder.InsertData(
                table: "Palestrantes",
                columns: new[] { "PalestranteId", "CPF", "Email", "Nome" },
                values: new object[,]
                {
                    { 1, "123.456.789-00", "analima@email.com", "Ana Lima" },
                    { 2, "987.654.321-00", "brunoreis@email.com", "Bruno Reis" }
                });

            migrationBuilder.InsertData(
                table: "Participantes",
                columns: new[] { "ParticipanteId", "CPF", "Email", "Nome" },
                values: new object[] { 1, "123.456.789-00", "anasouza@email.com", "Ana Souza" });

            migrationBuilder.InsertData(
                table: "Eventos",
                columns: new[] { "EventoId", "DataEHora", "Descricao", "Discriminator", "Duracao", "LocalId", "OrganizadorId", "PalestraId", "QntInscricoes", "Status", "Titulo", "VagasRestantes", "Valor" },
                values: new object[] { 1, new DateTime(2026, 11, 20, 19, 0, 0, 0, DateTimeKind.Unspecified), "Desenvolvimento desktop prático com WPF e Entity Framework Core.", "Workshop", "3h", 1, 1, 1, 25, "Disponível", "Workshop de C#", 15, 50.0 });

            migrationBuilder.InsertData(
                table: "Eventos",
                columns: new[] { "EventoId", "DataEHora", "Descricao", "Discriminator", "Duracao", "LocalId", "OrganizadorId", "QntInscricoes", "Status", "Titulo", "VagasRestantes", "Valor" },
                values: new object[] { 2, new DateTime(2026, 11, 28, 9, 0, 0, 0, DateTimeKind.Unspecified), "Palestras sobre arquitetura de software, cloud e inteligência artificial.", "Conferencia", "8h", 2, 1, 112, "Disponível", "Conferência Tech", 188, 120.0 });

            migrationBuilder.InsertData(
                table: "Eventos",
                columns: new[] { "EventoId", "DataEHora", "Descricao", "Discriminator", "Duracao", "LocalId", "OrganizadorId", "PalestraId", "QntInscricoes", "Status", "Titulo", "VagasRestantes", "Valor" },
                values: new object[] { 3, new DateTime(2026, 12, 2, 14, 0, 0, 0, DateTimeKind.Unspecified), "Prototipação de interfaces e experiência do usuário.", "Workshop", "2h", 3, 1, 0, 30, "Disponível", "Workshop de UX", 0, 0.0 });

            migrationBuilder.InsertData(
                table: "Inscricoes",
                columns: new[] { "InscricaoId", "EventoId", "ParticipanteId" },
                values: new object[,]
                {
                    { 1, 1, 1 },
                    { 2, 2, 1 }
                });

            migrationBuilder.InsertData(
                table: "Palestras",
                columns: new[] { "PalestraId", "ConferenciaId", "Duracao", "Nome", "PalestranteId", "WorkshopId" },
                values: new object[,]
                {
                    { 1, 2, 3.0, "Fundamentos de C#", 1, null },
                    { 2, 2, 2.0, "EF Core na Prática", 2, null }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Eventos",
                keyColumn: "EventoId",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Inscricoes",
                keyColumn: "InscricaoId",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Inscricoes",
                keyColumn: "InscricaoId",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Organizadores",
                keyColumn: "OrganizadorId",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Palestras",
                keyColumn: "PalestraId",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Palestras",
                keyColumn: "PalestraId",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Eventos",
                keyColumn: "EventoId",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Eventos",
                keyColumn: "EventoId",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Locais",
                keyColumn: "LocalId",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Palestrantes",
                keyColumn: "PalestranteId",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Palestrantes",
                keyColumn: "PalestranteId",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Participantes",
                keyColumn: "ParticipanteId",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Locais",
                keyColumn: "LocalId",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Locais",
                keyColumn: "LocalId",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Organizadores",
                keyColumn: "OrganizadorId",
                keyValue: 1);
        }
    }
}
