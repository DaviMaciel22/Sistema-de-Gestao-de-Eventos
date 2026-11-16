using System;

public class Inscricao
{
    public int IdInscricao { get; set; }
    public Evento Evento { get; set; }
    public Participante Participante { get; set; }

    public override string ToString()
    {
        return $"ID: {IdInscricao} / Evento: {Evento.Titulo} / Participante: {Participante.Nome}";
    }

    public void confirmar()
    {
        Console.WriteLine("Inscrição confirmada.");
        //adicionar insert no banco
    }

    public void cancelar()
    {
        Console.WriteLine("Inscrição cancelada.");
    }

}
