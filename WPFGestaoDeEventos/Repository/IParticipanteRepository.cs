using WPFGestaoDeEventos.Model;

namespace WPFGestaoDeEventos.Repository
{
    // SOLID (DIP): o controller usa esta interface, não a classe que acessa o banco
    public interface IParticipanteRepository
    {
        Participante? ObterPorId(int participanteId);
        bool EmailEmUso(string email, int participanteIdIgnorado);
        bool TemInscricaoConfirmada(int participanteId);
        void Remover(Participante participante);
        void Salvar();
    }
}
