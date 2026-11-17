namespace WPFGestaoDeEventos.Model
{
    public class Inscricao : ICertificado
    {
        public int IdInscricao { get; set; }
        public DateTime DataInscricao { get; set; } = DateTime.Now;
        public StatusInscricao Status { get; set; } = StatusInscricao.Confirmado;

        public int EventoId { get; set; }
        public Evento Evento { get; set; } = null!;

        public int ParticipanteId { get; set; }
        public Participante Participante { get; set; } = null!;

        // 1x1 (opcionais): evento gratuito não tem pagamento; certificado só após o check-in
        public Pagamento? Pagamento { get; set; }
        public Certificado? Certificado { get; set; }

        public Certificado Emitir()
        {
            if (Status != StatusInscricao.Utilizado)
            {
                throw new InvalidOperationException("O certificado só pode ser emitido após o check-in.");
            }

            Certificado ??= new Certificado
            {
                Codigo = $"CERT-{IdInscricao:D6}",
                DataEmissao = DateTime.Now,
                Inscricao = this
            };

            return Certificado;
        }

        public override string ToString()
        {
            return $"ID: {IdInscricao} / Evento: {Evento.Titulo} / Participante: {Participante.Nome}";
        }
    }
}
