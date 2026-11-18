using WPFGestaoDeEventos.Model;

namespace WPFGestaoDeEventos.Repository
{
    // SOLID (DIP): o controller usa esta interface, não a classe que acessa o banco
    public interface IInscricaoRepository
    {
        Evento? ObterEvento(int eventoId);
        bool ExisteInscricaoAtiva(int participanteId, int eventoId);
        void Adicionar(Inscricao inscricao);
        Inscricao? ObterPorId(int inscricaoId);
        List<Inscricao> ListarPorParticipante(int participanteId, string? nome, string? status);
        List<Palestra> ListarPalestras(int eventoId);
        void Salvar();
    }
}
