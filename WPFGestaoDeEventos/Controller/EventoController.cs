using WPFGestaoDeEventos.Model;
using WPFGestaoDeEventos.Repository;

namespace WPFGestaoDeEventos.Controller
{
    public class EventoController
    {
        private readonly EventoRepository _repositorio = new();

        // Home: ao abrir a tela
        public List<Evento> ListarDisponiveis()
        {
            return _repositorio.ListarDisponiveis();
        }

        // Home: preenche o filtro de cidade
        public List<string> ListarCidades()
        {
            return _repositorio.ListarCidades();
        }

        // Home: botão Filtrar (a consulta SQL fica no EventoRepository)
        public List<Evento> Buscar(string? nome, DateTime? data, string? cidade, string? tipo)
        {
            return _repositorio.Buscar(nome, data, cidade, tipo);
        }
    }
}
