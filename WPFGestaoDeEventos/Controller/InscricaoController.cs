using Microsoft.EntityFrameworkCore;
using WPFGestaoDeEventos.Data;
using WPFGestaoDeEventos.Model;

namespace WPFGestaoDeEventos.Controller
{
    public class InscricaoController
    {
        // Home: botão "Garantir ingresso"
        public Inscricao Inscrever(int participanteId, int eventoId)
        {
            using var contexto = new AppDbContext();

            var evento = contexto.Eventos
                .Include(e => e.Inscricoes)
                .FirstOrDefault(e => e.IdEvento == eventoId)
                ?? throw new InvalidOperationException("Evento não encontrado.");

            var participante = contexto.Participantes.Find(participanteId)
                ?? throw new InvalidOperationException("Participante não encontrado.");

            if (evento.Status != "Aberto")
            {
                throw new InvalidOperationException("As inscrições deste evento estão encerradas.");
            }

            if (evento.Esgotado)
            {
                throw new InvalidOperationException("Este evento está esgotado.");
            }

            bool jaInscrito = evento.Inscricoes.Any(i =>
                i.ParticipanteId == participanteId && i.Status != StatusInscricao.Cancelado);

            if (jaInscrito)
            {
                throw new InvalidOperationException("Você já possui ingresso para este evento.");
            }

            var inscricao = new Inscricao
            {
                Evento = evento,
                Participante = participante
            };

            contexto.Inscricoes.Add(inscricao);
            contexto.SaveChanges();

            return inscricao;
        }

        // Meus Ingressos: ao abrir a tela
        public List<Inscricao> ListarPorParticipante(int participanteId)
        {
            using var contexto = new AppDbContext();

            return contexto.Inscricoes
                .Include(i => i.Evento).ThenInclude(e => e.Local)
                .Include(i => i.Evento).ThenInclude(e => e.Organizador)
                .Include(i => i.Evento).ThenInclude(e => e.Palestras).ThenInclude(p => p.Palestrante)
                .Include(i => i.Certificado)
                .Where(i => i.ParticipanteId == participanteId)
                .OrderBy(i => i.Evento.DataEHora)
                .ToList();
        }

        // Meus Ingressos: botão "Cancelar ingresso" (a vaga volta, pois o status deixa de contar)
        public void Cancelar(int inscricaoId)
        {
            using var contexto = new AppDbContext();

            var inscricao = contexto.Inscricoes.Find(inscricaoId)
                ?? throw new InvalidOperationException("Ingresso não encontrado.");

            if (inscricao.Status != StatusInscricao.Confirmado)
            {
                throw new InvalidOperationException("Só é possível cancelar ingressos confirmados.");
            }

            inscricao.Status = StatusInscricao.Cancelado;
            contexto.SaveChanges();
        }

        // Meus Ingressos: botão "Ver certificado" (só com status Utilizado)
        public Certificado EmitirCertificado(int inscricaoId)
        {
            using var contexto = new AppDbContext();

            var inscricao = contexto.Inscricoes
                .Include(i => i.Certificado)
                .FirstOrDefault(i => i.IdInscricao == inscricaoId)
                ?? throw new InvalidOperationException("Ingresso não encontrado.");

            Certificado certificado = inscricao.Emitir();
            contexto.SaveChanges();

            return certificado;
        }
    }
}
