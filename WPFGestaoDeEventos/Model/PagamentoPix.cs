namespace WPFGestaoDeEventos.Model
{
    public class PagamentoPix : Pagamento
    {
        public override string Metodo => "Pix";

        public override void Pagar()
        {
            Confirmar();
        }
    }
}
