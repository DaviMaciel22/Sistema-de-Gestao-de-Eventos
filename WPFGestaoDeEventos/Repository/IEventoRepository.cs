using WPFGestaoDeEventos.Model;

namespace WPFGestaoDeEventos.Repository
{
    // SOLID (DIP): o controller usa esta interface, não a classe que acessa o banco
    public interface IEventoRepository
    {
        List<Evento> ListarDisponiveis();
        List<string> ListarCidades();
        List<Evento> Buscar(string? nome, DateTime? data, string? cidade, string? tipo);
    }
}
