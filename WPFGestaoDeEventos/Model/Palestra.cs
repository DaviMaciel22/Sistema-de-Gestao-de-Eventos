namespace WPFGestaoDeEventos.Model;

public class Palestra
{
    public int IdPalestra { get; set; }
    public string Titulo { get; set; } = string.Empty;
    public DateTime DataEHora { get; set; }
    public double Duracao { get; set; }

    // Composição: a palestra pertence a um evento
    public int EventoId { get; set; }
    public Evento Evento { get; set; } = null!;

    // 1xN: um palestrante pode dar várias palestras
    public int PalestranteId { get; set; }
    public Palestrante Palestrante { get; set; } = null!;

    public override string ToString()
    {
        return $"ID: {IdPalestra} / Título: {Titulo} / Palestrante: {Palestrante?.Nome} / Evento: {Evento?.Titulo} / Duração: {Duracao}";
    }
}
