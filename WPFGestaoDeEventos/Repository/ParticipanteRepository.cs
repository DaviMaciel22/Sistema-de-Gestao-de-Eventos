using WPFGestaoDeEventos.Model;

namespace WPFGestaoDeEventos.Repository
{
    // SOLID (SRP): esta classe só conversa com o banco sobre participantes
    public class ParticipanteRepository : IParticipanteRepository
    {
        // Um contexto por tela: o EF guarda as alterações e grava tudo junto no Salvar()
        private readonly WpfGestaoDeEventosDbContext _contexto = new();

        public Participante? ObterPorId(int participanteId)
        {
            return _contexto.Participantes.FirstOrDefault(p => p.ParticipanteId == participanteId);
        }

        public bool EmailEmUso(string email, int participanteIdIgnorado)
        {
            return _contexto.Participantes.Any(p => p.Email == email && p.ParticipanteId != participanteIdIgnorado);
        }

        public bool TemInscricaoConfirmada(int participanteId)
        {
            return _contexto.Inscricoes.Any(i =>
                i.ParticipanteId == participanteId && i.Status == StatusInscricao.Confirmado);
        }

        public void Remover(Participante participante)
        {
            _contexto.Participantes.Remove(participante);
        }

        public void Salvar()
        {
            _contexto.SaveChanges();
        }
    }
}
