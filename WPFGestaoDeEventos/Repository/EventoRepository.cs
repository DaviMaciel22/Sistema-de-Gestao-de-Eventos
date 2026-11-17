using System.Text;
using Microsoft.EntityFrameworkCore;
using WPFGestaoDeEventos.Data;
using WPFGestaoDeEventos.Model;

namespace WPFGestaoDeEventos.Repository;

public class EventoRepository
{
    // Home: ao abrir a tela
    public List<Evento> ListarDisponiveis()
    {
        using var contexto = new AppDbContext();

        return contexto.Eventos
            .Include(e => e.Local)
            .Include(e => e.Inscricoes)
            .Where(e => e.Status == "Aberto" && e.DataEHora >= DateTime.Now)
            .OrderBy(e => e.DataEHora)
            .ToList();
    }

    // Home: preenche o filtro de cidade
    public List<string> ListarCidades()
    {
        using var contexto = new AppDbContext();

        return contexto.Locais
            .Select(l => l.Cidade)
            .Distinct()
            .OrderBy(c => c)
            .ToList();
    }

    // Home: botão Filtrar. Funcionalidade feita com SQL (FromSqlRaw), o restante usa EF/LINQ.
    // O parâmetro "tipo" espera "Workshop" ou "Conferencia" (valor da coluna Tipo).
    public List<Evento> Buscar(string? nome, DateTime? data, string? cidade, string? tipo)
    {
        var sql = new StringBuilder(
            "SELECT e.* FROM Eventos e INNER JOIN Locais l ON e.LocalId = l.IdLocal WHERE e.Status = {0}");
        var parametros = new List<object> { "Aberto" };

        // Registra o valor como parâmetro e devolve o marcador {n} correspondente
        string Parametro(object valor)
        {
            parametros.Add(valor);
            return "{" + (parametros.Count - 1) + "}";
        }

        if (!string.IsNullOrWhiteSpace(nome))
        {
            sql.Append(" AND e.Titulo LIKE " + Parametro($"%{nome.Trim()}%"));
        }

        if (data.HasValue)
        {
            DateTime inicio = data.Value.Date;
            DateTime fim = inicio.AddDays(1);
            sql.Append(" AND e.DataEHora >= " + Parametro(inicio));
            sql.Append(" AND e.DataEHora < " + Parametro(fim));
        }
        else
        {
            sql.Append(" AND e.DataEHora >= " + Parametro(DateTime.Now));
        }

        if (!string.IsNullOrWhiteSpace(cidade))
        {
            sql.Append(" AND l.Cidade = " + Parametro(cidade));
        }

        if (!string.IsNullOrWhiteSpace(tipo))
        {
            sql.Append(" AND e.Tipo = " + Parametro(tipo));
        }

        using var contexto = new AppDbContext();

        return contexto.Eventos
            .FromSqlRaw(sql.ToString(), parametros.ToArray())
            .Include(e => e.Local)
            .Include(e => e.Inscricoes)
            .OrderBy(e => e.DataEHora)
            .ToList();
    }
}
