using System;

namespace WPFGestaoDeEventos.Model;

// Relação N:N: Participante e Evento se ligam por meio da Inscricao
public class Inscricao
{
    public int InscricaoId { get; set; }

    // Confirmado, Cancelado ou Utilizado
    public string Status { get; set; } = StatusInscricao.Confirmado;

    public int EventoId { get; set; }
    public Evento Evento { get; set; } = null!;

    public int ParticipanteId { get; set; }
    public Participante Participante { get; set; } = null!;

    public Certificado Certificado { get; set; } = null!;

    public override string ToString()
    {
        return $"ID: {InscricaoId} / Evento: {Evento?.Titulo} / Participante: {Participante?.Nome}";
    }

}
