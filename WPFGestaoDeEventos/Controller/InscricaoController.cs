using WPFGestaoDeEventos.Model;
using WPFGestaoDeEventos.Repository;

namespace WPFGestaoDeEventos.Controller
{
    // MVC: o Controller aplica as regras dos ingressos; o banco fica no repositório
    // SOLID (SRP): aqui só ficam as regras de inscrição
    public class InscricaoController
    {
        // SOLID (DIP): depende da interface; a tela usa o construtor vazio, que cria o repositório real
        private readonly IInscricaoRepository _repositorio;

        public InscricaoController() : this(new InscricaoRepository()) { }

        public InscricaoController(IInscricaoRepository repositorio)
        {
            _repositorio = repositorio;
        }

        // Home: botão "Garantir ingresso"
        public Inscricao Inscrever(int participanteId, int eventoId)
        {
            var evento = _repositorio.ObterEvento(eventoId)
                ?? throw new InvalidOperationException("Evento não encontrado.");

            if (evento.Status != StatusEvento.Disponivel)
                throw new InvalidOperationException("Este evento não está com inscrições abertas.");

            if (evento.VagasRestantes <= 0)
                throw new InvalidOperationException("Evento esgotado.");

            if (_repositorio.ExisteInscricaoAtiva(participanteId, eventoId))
                throw new InvalidOperationException("Você já tem ingresso para este evento.");

            var inscricao = new Inscricao
            {
                EventoId = eventoId,
                ParticipanteId = participanteId,
                Status = StatusInscricao.Confirmado
            };

            evento.VagasRestantes--;
            evento.QntInscricoes++;

            _repositorio.Adicionar(inscricao);
            _repositorio.Salvar();
            return inscricao;
        }

        // Meus Ingressos: ao abrir a tela e ao filtrar (nome e/ou status; null = todos)
        public List<Inscricao> ListarPorParticipante(int participanteId, string? nome = null, string? status = null)
        {
            return _repositorio.ListarPorParticipante(participanteId, nome, status);
        }

        // Meus Ingressos: palestras do evento selecionado
        public List<Palestra> ListarPalestras(int eventoId)
        {
            return _repositorio.ListarPalestras(eventoId);
        }

        // Meus Ingressos: botão "Cancelar ingresso" (a vaga volta)
        public void Cancelar(int inscricaoId)
        {
            var inscricao = _repositorio.ObterPorId(inscricaoId)
                ?? throw new InvalidOperationException("Ingresso não encontrado.");

            if (inscricao.Status != StatusInscricao.Confirmado)
                throw new InvalidOperationException("Só é possível cancelar um ingresso confirmado.");

            inscricao.Status = StatusInscricao.Cancelado;
            inscricao.Evento.VagasRestantes++;
            if (inscricao.Evento.QntInscricoes > 0)
                inscricao.Evento.QntInscricoes--;

            _repositorio.Salvar();
        }

        // Meus Ingressos: botão "Ver certificado" (só para ingresso utilizado)
        public Certificado ObterCertificado(int inscricaoId)
        {
            var inscricao = _repositorio.ObterPorId(inscricaoId)
                ?? throw new InvalidOperationException("Ingresso não encontrado.");

            if (inscricao.Status != StatusInscricao.Utilizado)
                throw new InvalidOperationException("O certificado só fica disponível depois do check-in.");

            if (inscricao.Certificado == null)
            {
                inscricao.Certificado = new Certificado
                {
                    InscricaoId = inscricao.InscricaoId,
                    DataEmissao = DateTime.Now
                };
                _repositorio.Salvar();
            }

            inscricao.Certificado.Inscricao = inscricao;
            return inscricao.Certificado;
        }
    }
}
