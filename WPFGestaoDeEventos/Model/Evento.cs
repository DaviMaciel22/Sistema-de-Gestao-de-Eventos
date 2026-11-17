namespace WPFGestaoDeEventos.Model
{
    public abstract class Evento
    {
        public int IdEvento { get; set; }
        public string Titulo { get; set; } = string.Empty;
        public string Descricao { get; set; } = string.Empty;
        public DateTime DataEHora { get; set; }
        public int Capacidade { get; set; }
        public double Valor { get; set; }
        public string Duracao { get; set; } = string.Empty;
        public string Status { get; set; } = "Aberto";

        // Agregação: o local existe independente do evento
        public int LocalId { get; set; }
        public Local Local { get; set; } = null!;

        // 1xN: Organizador -> Evento
        public int OrganizadorId { get; set; }
        public Organizador Organizador { get; set; } = null!;

        // 1xN: Evento -> Inscricao
        public ICollection<Inscricao> Inscricoes { get; set; } = new List<Inscricao>();

        // Composição: as palestras só existem dentro do evento
        public ICollection<Palestra> Palestras { get; set; } = new List<Palestra>();

        // Propriedades calculadas (somente leitura, não vão para o banco)
        public abstract string NomeTipo { get; }
        public int VagasRestantes => Capacidade - Inscricoes.Count(i => i.Status != StatusInscricao.Cancelado);
        public bool Esgotado => VagasRestantes <= 0;

        public void abrirInscricoes()
        {
            foreach (Inscricao i in Inscricoes)
            {
                i.ToString();
            }
        }
    }
}
