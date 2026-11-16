using WPFGestaoDeEventos.Model;

namespace WPFGestaoDeEventos.Controller
{
    public class EventoController
    {
        // Home: ao abrir a tela
        public List<Evento> ListarDisponiveis()
        {
            throw new NotImplementedException();
        }

        // Home: botão Filtrar (candidata à função via SQL)
        public List<Evento> Buscar(string? nome, DateTime? data, string? cidade, string? tipo)
        {
            throw new NotImplementedException();
        }
    }
}