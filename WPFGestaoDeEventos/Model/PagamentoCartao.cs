namespace WPFGestaoDeEventos.Model
{
    public class PagamentoCartao : Pagamento
    {
        public override string Metodo => "Cartão";

        public override void Pagar()
        {
            Confirmar();
        }
    }
}
