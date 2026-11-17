namespace WPFGestaoDeEventos.Model
{
    public class Palestra
    {
        public int IdPalestra { get; set; }
        public string Titulo { get; set; } = string.Empty;
        public DateTime Horario { get; set; }

        // Composição: pertence a um evento
        public int EventoId { get; set; }
        public Evento Evento { get; set; } = null!;

        // NxN: uma palestra tem vários palestrantes e um palestrante dá várias palestras
        public ICollection<Palestrante> Palestrantes { get; set; } = new List<Palestrante>();
    }
}
