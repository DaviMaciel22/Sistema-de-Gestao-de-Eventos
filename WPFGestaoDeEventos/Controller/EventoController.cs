using WPFGestaoDeEventos.Model;
using WPFGestaoDeEventos.Repository;

namespace WPFGestaoDeEventos.Controller
{
    // MVC: o Controller fica entre a tela (View) e os dados (Model/Repository)
    public class EventoController
    {
        // SOLID (DIP): depende da interface; a tela usa o construtor vazio, que cria o repositório real
        private readonly IEventoRepository _repositorio;

        public EventoController() : this(new EventoRepository()) { }

        public EventoController(IEventoRepository repositorio)
        {
            _repositorio = repositorio;
        }

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

        // Home: botão Filtrar (consulta SQL parametrizada no repositório)
        public List<Evento> Buscar(string? nome, DateTime? data, string? cidade, string? tipo)
        {
            return _repositorio.Buscar(nome, data, cidade, tipo);
        }
    }
}
