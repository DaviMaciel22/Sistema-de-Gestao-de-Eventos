namespace WPFGestaoDeEventos.Model
{
    public class Local
    {
        public int IdLocal { get; set; }
        public string Nome { get; set; } = string.Empty;
        public string Cidade { get; set; } = string.Empty;
        public string Estado { get; set; } = string.Empty;

        public override string ToString()
        {
            return $"{Nome}, {Cidade} ({Estado})";
        }
    }
}
