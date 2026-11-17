namespace WPFGestaoDeEventos.Model
{
    public abstract class Pagamento
    {
        public int IdPagamento { get; set; }
        public double Valor { get; set; }
        public bool Pago { get; set; }
        public DateTime? DataPagamento { get; set; }

        // 1x1 com Inscricao
        public int InscricaoId { get; set; }
        public Inscricao Inscricao { get; set; } = null!;

        public abstract string Metodo { get; }

        public abstract void Pagar();

        protected void Confirmar()
        {
            Pago = true;
            DataPagamento = DateTime.Now;
        }
    }
}
