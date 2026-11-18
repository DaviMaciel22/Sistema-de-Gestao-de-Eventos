using Microsoft.EntityFrameworkCore;
using WPFGestaoDeEventos.Model;

namespace WPFGestaoDeEventos.Repository
{
    // SOLID (SRP): esta classe só conversa com o banco sobre eventos
    // Aqui cada método abre e fecha o contexto, para a Home sempre ver as vagas atualizadas
    public class EventoRepository : IEventoRepository
    {

        public List<Evento> ListarDisponiveis()
        {
            using var contexto = new WpfGestaoDeEventosDbContext();

            return contexto.Eventos
                .Include(e => e.Local)
                .Where(e => e.Status == StatusEvento.Disponivel && e.DataEHora >= DateTime.Now)
                .OrderBy(e => e.DataEHora)
                .ToList();
        }

        public List<string> ListarCidades()
        {
            using var contexto = new WpfGestaoDeEventosDbContext();

            return contexto.Locais
                .Select(l => l.Cidade)
                .Distinct()
                .OrderBy(c => c)
                .ToList();
        }

        // Funcionalidade via SQL: busca da Home com parâmetros ({0}, {1}...), nunca juntando o texto do usuário no SQL
        // tipo: "Workshop", "Conferencia" ou null (todos). É o valor da coluna Discriminator.
        public List<Evento> Buscar(string? nome, DateTime? data, string? cidade, string? tipo)
        {
            using var contexto = new WpfGestaoDeEventosDbContext();

            var sql = new System.Text.StringBuilder(
                "SELECT e.* FROM Eventos e INNER JOIN Locais l ON e.LocalId = l.LocalId WHERE e.Status = {0}");
            var parametros = new List<object> { StatusEvento.Disponivel };

            if (!string.IsNullOrWhiteSpace(nome))
            {
                parametros.Add("%" + nome.Trim() + "%");
                sql.Append($" AND e.Titulo LIKE {{{parametros.Count - 1}}}");
            }

            if (data.HasValue)
            {
                parametros.Add(data.Value.Date);
                sql.Append($" AND e.DataEHora >= {{{parametros.Count - 1}}}");
                parametros.Add(data.Value.Date.AddDays(1));
                sql.Append($" AND e.DataEHora < {{{parametros.Count - 1}}}");
            }
            else
            {
                parametros.Add(DateTime.Now);
                sql.Append($" AND e.DataEHora >= {{{parametros.Count - 1}}}");
            }

            if (!string.IsNullOrWhiteSpace(cidade))
            {
                parametros.Add(cidade);
                sql.Append($" AND l.Cidade = {{{parametros.Count - 1}}}");
            }

            if (!string.IsNullOrWhiteSpace(tipo))
            {
                parametros.Add(tipo);
                sql.Append($" AND e.Discriminator = {{{parametros.Count - 1}}}");
            }

            return contexto.Eventos
                .FromSqlRaw(sql.ToString(), parametros.ToArray())
                .Include(e => e.Local)
                .OrderBy(e => e.DataEHora)
                .ToList();
        }
    }
}
