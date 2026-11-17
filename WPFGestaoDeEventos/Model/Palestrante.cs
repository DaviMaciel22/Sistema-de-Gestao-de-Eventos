namespace WPFGestaoDeEventos.Model
{
    public class Palestrante
    {
        public int IdPalestrante { get; set; }
        public string Nome { get; set; } = string.Empty;

        // NxN
        public ICollection<Palestra> Palestras { get; set; } = new List<Palestra>();
    }
}
