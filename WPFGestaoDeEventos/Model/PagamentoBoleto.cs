namespace WPFGestaoDeEventos.Model
{
    public class PagamentoBoleto : Pagamento
    {
        public override string Metodo => "Boleto";

        public override void Pagar()
        {
            Confirmar();
        }
    }
}
