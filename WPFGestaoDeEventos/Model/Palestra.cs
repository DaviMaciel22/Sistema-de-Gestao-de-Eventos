using System;

namespace WPFGestaoDeEventos.Model;

public class Palestra
{
    public int IdPalestra { get; set; } = 0;
    public Palestrante Palestrante { get; set; }
    public Evento Evento { get; set; }
    public double Duracao { get; set; }
    public string Nome { get; set; }


    public override string ToString()
    {
        return $"ID: {IdPalestra} / Palestrante: {Palestrante.Nome} / Evento: {Evento.Titulo} / Duração: {Duracao}";
    }
}
