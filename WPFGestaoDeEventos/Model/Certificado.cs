namespace WPFGestaoDeEventos.Model
{
    public class Certificado
    {
        public int IdCertificado { get; set; }
        public string Codigo { get; set; } = string.Empty;
        public DateTime DataEmissao { get; set; }

        // 1x1 com Inscricao
        public int InscricaoId { get; set; }
        public Inscricao Inscricao { get; set; } = null!;
    }
}
