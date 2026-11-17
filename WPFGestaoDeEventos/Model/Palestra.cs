using System;

namespace WPFGestaoDeEventos.Model;

public class Palestra
{
    public int PalestraId { get; set; }
    public double Duracao { get; set; }
    public string Nome { get; set; } = null!;

    public int PalestranteId { get; set; }
    public Palestrante Palestrante { get; set; } = null!;

    public int EventoId { get; set; }
    public Evento Evento { get; set; } = null!;



    public override string ToString()
    {
        return $"ID: {PalestraId} / Palestrante: {Palestrante.Nome} / Evento: {Evento.Titulo} / Duração: {Duracao}";
    }
}
