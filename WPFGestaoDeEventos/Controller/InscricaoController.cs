using Microsoft.EntityFrameworkCore;
using WPFGestaoDeEventos.Model;

namespace WPFGestaoDeEventos.Controller
{
    public class InscricaoController
    {
        private const string Confirmado = "Confirmado";
        private const string Cancelado = "Cancelado";
        private const string Utilizado = "Utilizado";

        // Home: botão "Garantir ingresso"
        public Inscricao Inscrever(int participanteId, int eventoId)
        {
            using var contexto = new WpfGestaoDeEventosDbContext();

            var evento = contexto.Eventos.FirstOrDefault(e => e.EventoId == eventoId)
                ?? throw new InvalidOperationException("Evento não encontrado.");

            if (evento.Status != "Disponível")
                throw new InvalidOperationException("Este evento não está com inscrições abertas.");

            if (evento.VagasRestantes <= 0)
                throw new InvalidOperationException("Evento esgotado.");

            bool jaInscrito = contexto.Inscricoes.Any(i =>
                i.EventoId == eventoId && i.ParticipanteId == participanteId && i.Status != Cancelado);
            if (jaInscrito)
                throw new InvalidOperationException("Você já tem ingresso para este evento.");

            var inscricao = new Inscricao
            {
                EventoId = eventoId,
                ParticipanteId = participanteId,
                Status = Confirmado
            };

            evento.VagasRestantes--;
            evento.QntInscricoes++;

            contexto.Inscricoes.Add(inscricao);
            contexto.SaveChanges();
            return inscricao;
        }

        // Meus Ingressos: ao abrir a tela e ao filtrar (nome e/ou status; null = todos)
        public List<Inscricao> ListarPorParticipante(int participanteId, string? nome = null, string? status = null)
        {
            using var contexto = new WpfGestaoDeEventosDbContext();

            var consulta = contexto.Inscricoes
                .Include(i => i.Evento).ThenInclude(e => e.Local)
                .Include(i => i.Certificado)
                .Where(i => i.ParticipanteId == participanteId);

            if (!string.IsNullOrWhiteSpace(nome))
                consulta = consulta.Where(i => i.Evento.Titulo.Contains(nome.Trim()));

            if (!string.IsNullOrWhiteSpace(status))
                consulta = consulta.Where(i => i.Status == status);

            return consulta.OrderBy(i => i.Evento.DataEHora).ToList();
        }

        // Meus Ingressos: palestras do evento selecionado.
        // Conferência tem várias (ConferenciaId); Workshop tem no máximo uma (WorkshopId).
        public List<Palestra> ListarPalestras(int eventoId)
        {
            using var contexto = new WpfGestaoDeEventosDbContext();

            return contexto.Palestras
                .Include(p => p.Palestrante)
                .Where(p => p.ConferenciaId == eventoId || p.WorkshopId == eventoId)
                .OrderBy(p => p.PalestraId)
                .ToList();
        }

        // Meus Ingressos: botão "Cancelar ingresso" (a vaga volta)
        public void Cancelar(int inscricaoId)
        {
            using var contexto = new WpfGestaoDeEventosDbContext();

            var inscricao = contexto.Inscricoes
                .Include(i => i.Evento)
                .FirstOrDefault(i => i.InscricaoId == inscricaoId)
                ?? throw new InvalidOperationException("Ingresso não encontrado.");

            if (inscricao.Status != Confirmado)
                throw new InvalidOperationException("Só é possível cancelar um ingresso confirmado.");

            inscricao.Status = Cancelado;
            inscricao.Evento.VagasRestantes++;
            if (inscricao.Evento.QntInscricoes > 0)
                inscricao.Evento.QntInscricoes--;

            contexto.SaveChanges();
        }

        // Meus Ingressos: botão "Ver certificado" (só para ingresso utilizado)
        public Certificado ObterCertificado(int inscricaoId)
        {
            using var contexto = new WpfGestaoDeEventosDbContext();

            var inscricao = contexto.Inscricoes
                .Include(i => i.Evento)
                .Include(i => i.Participante)
                .Include(i => i.Certificado)
                .FirstOrDefault(i => i.InscricaoId == inscricaoId)
                ?? throw new InvalidOperationException("Ingresso não encontrado.");

            if (inscricao.Status != Utilizado)
                throw new InvalidOperationException("O certificado só fica disponível depois do check-in.");

            if (inscricao.Certificado == null)
            {
                inscricao.Certificado = new Certificado
                {
                    InscricaoId = inscricao.InscricaoId,
                    DataEmissao = DateTime.Now
                };
                contexto.SaveChanges();
            }

            inscricao.Certificado.Inscricao = inscricao;
            return inscricao.Certificado;
        }
    }
}
