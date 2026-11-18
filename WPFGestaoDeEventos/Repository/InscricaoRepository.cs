using Microsoft.EntityFrameworkCore;
using WPFGestaoDeEventos.Model;

namespace WPFGestaoDeEventos.Repository
{
    // SOLID (SRP): esta classe só conversa com o banco sobre inscrições
    public class InscricaoRepository : IInscricaoRepository
    {
        // Um contexto por tela: o EF guarda as alterações e grava tudo junto no Salvar()
        private readonly WpfGestaoDeEventosDbContext _contexto = new();

        public Evento? ObterEvento(int eventoId)
        {
            return _contexto.Eventos.FirstOrDefault(e => e.EventoId == eventoId);
        }

        public bool ExisteInscricaoAtiva(int participanteId, int eventoId)
        {
            return _contexto.Inscricoes.Any(i =>
                i.EventoId == eventoId && i.ParticipanteId == participanteId && i.Status != StatusInscricao.Cancelado);
        }

        public void Adicionar(Inscricao inscricao)
        {
            _contexto.Inscricoes.Add(inscricao);
        }

        public Inscricao? ObterPorId(int inscricaoId)
        {
            return _contexto.Inscricoes
                .Include(i => i.Evento)
                .Include(i => i.Participante)
                .Include(i => i.Certificado)
                .FirstOrDefault(i => i.InscricaoId == inscricaoId);
        }

        public List<Inscricao> ListarPorParticipante(int participanteId, string? nome, string? status)
        {
            var consulta = _contexto.Inscricoes
                .Include(i => i.Evento).ThenInclude(e => e.Local)
                .Include(i => i.Certificado)
                .Where(i => i.ParticipanteId == participanteId);

            if (!string.IsNullOrWhiteSpace(nome))
                consulta = consulta.Where(i => i.Evento.Titulo.Contains(nome.Trim()));

            if (!string.IsNullOrWhiteSpace(status))
                consulta = consulta.Where(i => i.Status == status);

            return consulta.OrderBy(i => i.Evento.DataEHora).ToList();
        }

        // Conferência tem várias palestras (ConferenciaId); Workshop tem no máximo uma (WorkshopId)
        public List<Palestra> ListarPalestras(int eventoId)
        {
            return _contexto.Palestras
                .Include(p => p.Palestrante)
                .Where(p => p.ConferenciaId == eventoId || p.WorkshopId == eventoId)
                .OrderBy(p => p.PalestraId)
                .ToList();
        }

        public void Salvar()
        {
            _contexto.SaveChanges();
        }
    }
}
