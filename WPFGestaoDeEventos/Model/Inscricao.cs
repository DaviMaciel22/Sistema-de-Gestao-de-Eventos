using System;

namespace WPFGestaoDeEventos.Model;

public class Inscricao
{
    public int IdInscricao { get; set; }
    public Evento Evento { get; set; }
    public Participante Participante { get; set; }

    public override string ToString()
    {
        return $"ID: {IdInscricao} / Evento: {Evento.Titulo} / Participante: {Participante.Nome}";
    }

}
